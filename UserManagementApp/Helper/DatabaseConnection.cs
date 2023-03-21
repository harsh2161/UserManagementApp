using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace UserManagementApp.Helper
{
    public class DatabaseConnection
    {
        private readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(DatabaseConnection));
        private string Token;
        public DatabaseConnection(string token=null)
        {
            Token = token;
        }
        public HttpResponseMessage PutAsyncRequest<T>(string baseUrl, T parameter)
        {
            Log.Info(" Put Request Started for "+baseUrl);
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Clear();
            if(Token != null)
            {
                Client.DefaultRequestHeaders.Add("Token", Token);
            }
            var Parameter = JsonConvert.SerializeObject(parameter, new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat });
            var Content = new StringContent(Parameter, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage Response = Client.PutAsync(baseUrl, Content).Result;
            Log.Info(" Put Request Completed for " + baseUrl);
            return Response;
        }

        public List<T> GetAsyncRequest<T>(string baseUrl)
        {
            Log.Info(" Get Request Started for " + baseUrl);
            List<T> StoreResponse = new List<T>();
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Clear();
            if (Token != null)
            {
                Client.DefaultRequestHeaders.Add("Token", Token);
            }
            HttpResponseMessage Response = Client.GetAsync(baseUrl).Result;
            if (Response.IsSuccessStatusCode)
            {
                Log.Info(" Get Request Completed for " + baseUrl);
                var ReadResponse = Response.Content.ReadAsStringAsync().Result;
                StoreResponse = JsonConvert.DeserializeObject<List<T>>(ReadResponse);
                return StoreResponse;
            }
            Log.Info(" Get Request Failed for " + baseUrl);
            return null;
        }

        public HttpResponseMessage DeleteAsyncRequest(string baseUrl)
        {
            Log.Info(" Delete Request Started for " + baseUrl);
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Clear();
            if (Token != null)
            {
                Client.DefaultRequestHeaders.Add("Token", Token);
            }
            HttpResponseMessage Response = Client.DeleteAsync(baseUrl).Result;
            Log.Info(" Delete Request Completed for " + baseUrl);
            return Response;
        }

        public HttpResponseMessage PostAsyncRequest<T>(string baseUrl, T parameter)
        {
            Log.Info(" Post Request Started for " + baseUrl);
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Clear();
            if (Token != null) 
            {
                Client.DefaultRequestHeaders.Add("Token", Token);
            }
            var Parameter = JsonConvert.SerializeObject(parameter, new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat });
            var Content = new StringContent(Parameter, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage Response = Client.PostAsync(baseUrl, Content).Result;
            Log.Info(" Post Request Completed for " + baseUrl);
            return Response;            
        }
    }
}