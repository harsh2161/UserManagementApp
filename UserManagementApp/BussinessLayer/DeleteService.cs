using System.Configuration;
using System.Net.Http;
using UserManagementApp.ExceptionHandler;
using UserManagementApp.ExceptionHandler.CustomExceptions;
using UserManagementApp.Helper;

namespace UserManagementApp.BussinessLayer
{
    public class DeleteService
    {
        private readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(DeleteService));
        string BaseUrl = "";
        string Token;
        public DeleteService(string token)
        {
            if (ConfigurationManager.AppSettings["BaseUrl"] == null)
            {
                Log.Error(" Base Url Not Present In Web.config  While Deleting User");
                throw new BaseUrlInvalid("Unable To Connect To Server", Exceptions.BaseUrlInvalidException);
            }
            BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            token = token.Replace("AuthCookie=", "");
            Token = token.Replace("\\", "");
        }

        public string Delete(string id)
        {
            DatabaseConnection DatabaseConnection = new DatabaseConnection(Token);
            HttpResponseMessage Response = DatabaseConnection.DeleteAsyncRequest(BaseUrl + "delete/"+id);
            var ResponseResult = Response.Content.ReadAsStringAsync().Result;
            if (Response.IsSuccessStatusCode)
            {
                Log.Info(" User Deleted for Id : " + id);                
                return ResponseResult;                
            }
            return ResponseResult;
        }
    }
}