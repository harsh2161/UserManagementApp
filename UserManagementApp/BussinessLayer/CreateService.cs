using System.Configuration;
using System.Net.Http;
using UserManagementApp.ExceptionHandler;
using UserManagementApp.ExceptionHandler.CustomExceptions;
using UserManagementApp.Helper;
using UserManagementApp.Models;
using UserManagementApp.Validator.UserValidation;

namespace UserManagementApp.BussinessLayer
{
    public class CreateService
    {
        private readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(CreateService));
        string BaseUrl = "";
        string Token;
        public CreateService(string token)
        {
            if (ConfigurationManager.AppSettings["BaseUrl"] == null)
            {
                Log.Error(" Base Url Not Present In Web.config  While Creating User");
                throw new BaseUrlInvalid("Unable To Connect To Server", Exceptions.BaseUrlInvalidException);
            }
            BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            token = token.Replace("AuthCookie=", "");
            Token = token.Replace("\\", "");
        }

        public string Create(User user)
        {
            UserFieldValidation.IsUserFieldValid(user);

            DatabaseConnection DatabaseConnection = new DatabaseConnection(Token);
            HttpResponseMessage Response = DatabaseConnection.PostAsyncRequest<User>(BaseUrl + "create", user);

            var ResponseResult = Response.Content.ReadAsStringAsync().Result;
            ResponseResult = ResponseResult.Replace("\"", "");
            Log.Info(" Getting Response for " + user.UserEmail + " is " + ResponseResult);
            return ResponseResult;
        }
    }
}