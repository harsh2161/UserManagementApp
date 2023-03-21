using System.Configuration;
using UserManagementApp.Models;
using UserManagementApp.Helper;
using System.Net.Http;
using UserManagementApp.ExceptionHandler;
using UserManagementApp.ExceptionHandler.CustomExceptions;
using UserManagementApp.Validator.UserValidation;

namespace UserManagementApp.BussinessLayer
{
    public class UpdateService
    {
        private readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(UpdateService));
        string BaseUrl = "";
        string Token;
        public UpdateService(string token)
        {
            if (ConfigurationManager.AppSettings["BaseUrl"] == null)
            {
                Log.Error(" Base Url Not Present In Web.config  While Updating User");
                throw new BaseUrlInvalid("Unable To Connect To Server", Exceptions.BaseUrlInvalidException);
            }
            BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            token = token.Replace("AuthCookie=", "");
            Token = token.Replace("\\", "");
        }

        public string Update(User user)
        {
            UserFieldValidation.IsUserFieldValid(user);

            DatabaseConnection DatabaseConnection = new DatabaseConnection(Token);
            HttpResponseMessage Response = DatabaseConnection.PutAsyncRequest<User>(BaseUrl + "update", user);

            var ResponseResult = Response.Content.ReadAsStringAsync().Result;
            ResponseResult = ResponseResult.Replace("\"", "");
            Log.Info(" Getting Reponse for updating User for Email : " + user.UserEmail + " With Response "+ ResponseResult);
            return ResponseResult;
        }
    }
}