using System.Configuration;
using System.Net.Http;
using UserManagementApp.ExceptionHandler;
using UserManagementApp.ExceptionHandler.CustomExceptions;
using UserManagementApp.Helper;
using UserManagementApp.Models;
using UserManagementApp.Validator.UserValidation;

namespace UserManagementApp.BussinessLayer
{
    public class AuthenticationService
    {
        private readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(AuthenticationService));
        string BaseUrl = "";
        public AuthenticationService()
        {
            if(ConfigurationManager.AppSettings["BaseUrl"] == null)
            {
                Log.Error(" Base Url Not Present In Web.config ");
                throw new BaseUrlInvalid("Token Is Null", Exceptions.BaseUrlInvalidException);
            }
            BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];
        }

        public string Authenticate(UserCredentials userCredentials)
        {
            EmailPasswordValidation.IsEmailPasswordValid(userCredentials);
            
            DatabaseConnection DatabaseConnection = new DatabaseConnection();
            HttpResponseMessage Response = DatabaseConnection.PostAsyncRequest<UserCredentials>(BaseUrl + "authenticate", userCredentials);
            
            var UserResponse = Response.Content.ReadAsStringAsync().Result;
            UserResponse = UserResponse.Replace("\"", "");

            Log.Info(" Getting Response for " + userCredentials.UserEmail + " is " + UserResponse);
            if (Response.IsSuccessStatusCode)
            {
                Log.Info(" User Authenticated for Email : " + userCredentials.UserEmail);

                if (UserResponse == "NotAuthenticated")
                    return UserResponse;
                               

                return ("AuthenticatedUser,"+UserResponse).Replace("\\","");
            }
            return UserResponse;
        }
    }
}
