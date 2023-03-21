using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using UserManagementApp.Helper;
using UserManagementApp.Models;
using UserManagementApp.Validator.SearchSortValidation;

namespace UserManagementApp.BussinessLayer
{
    public class GetService
    {
        private readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(GetService));
        private List<string> UserCredentials = new List<string>();
        string BaseUrl;
        string Token;
        public GetService(string token)
        {
            token = token.Replace("AuthCookie=", "");
            Token = token;
            UserCredentials = TokenService.Decrypt(token).Split(',').ToList();
            BaseUrl = ConfigurationManager.AppSettings["BaseUrl"] + "find/";
        }
        public List<T> Get<T>(SearchFilter searchFilter, string id)
        {
            if (UserCredentials[1] == AvailableRoles.Admin.ToString() && id == "all")
            {
                PageSizeIndexValidation.IsPageSizeIndexValid(searchFilter.PageIndex, searchFilter.PageSize);
                BaseUrl += "all";
                return (List<T>)Convert.ChangeType(ReadUsersWithParamaterFunction<User>(searchFilter), typeof(List<T>));
            }
            else if (UserCredentials[1] == AvailableRoles.Admin.ToString())
            {
                BaseUrl += id;
                return (List<T>)Convert.ChangeType(ReadUsersWithParamaterFunction<User>(searchFilter), typeof(List<T>));
            }
            else if (UserCredentials[1] == AvailableRoles.User.ToString())
            {
                BaseUrl += "user";
                return (List<T>)Convert.ChangeType(ReadUsersWithParamaterFunction<User>(searchFilter), typeof(List<T>));
            }
            return new List<T>();
        }
        private List<T> ReadUsersWithParamaterFunction<T>(SearchFilter searchFilter)
        {
            DatabaseConnection DatabaseConnection = new DatabaseConnection(Token);
            HttpResponseMessage Response = DatabaseConnection.PostAsyncRequest<SearchFilter>(BaseUrl, searchFilter);

            if (Response.IsSuccessStatusCode)
            {
                List<T> StoreResponse = new List<T>();
                var ReadResponse = Response.Content.ReadAsStringAsync().Result;
                StoreResponse = JsonConvert.DeserializeObject<List<T>>(ReadResponse);
                return StoreResponse;
            }
            return new List<T>();
        }
    }
}
