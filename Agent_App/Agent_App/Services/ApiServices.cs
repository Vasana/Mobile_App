using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Agent_App.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Microsoft.CSharp.RuntimeBinder;

namespace Agent_App.Services
{
    public class ApiServices
    {
        internal async Task<bool> RegisterAsync(string email, string password, string confirmPassword)
        {
            bool ret = false;
            try
            {

                var client = new HttpClient();

                var model = new RegisterBindingModel
                {
                    Email = email,
                    Password = password,
                    ConfirmPassword = confirmPassword
                };

                var json = JsonConvert.SerializeObject(model);
                HttpContent content = new StringContent(json);

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = new HttpResponseMessage();
                response = await client.PostAsync("http://203.115.11.236:10455/MobileAuthWS/api/Account/Register", content);

                ret = response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
            
            return ret;
        }

        public async Task<string> LoginAsync(string userName, string password)
        {
            string accessToken = null;
            try
            {
                var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            };

                var request = new HttpRequestMessage(HttpMethod.Post, "http://203.115.11.236:10455/MobileAuthWS/Token");

                request.Content = new FormUrlEncodedContent(keyValues);

                var client = new HttpClient();
                var response = await client.SendAsync(request);

                var jwtResponse = await response.Content.ReadAsStringAsync(); // contains access token

                JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(jwtResponse);

                accessToken = jwtDynamic.Value<string>("access_token");

                Debug.WriteLine(jwtResponse);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            return accessToken;

        }

        public async Task<List<Idea>> GetIdeasAsync(string accessToken)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync("http://localhost:50762/api/Ideas");

            var ideas = JsonConvert.DeserializeObject<List<Idea>>(json);

            return ideas;

        }

        public async Task<List<Cust_Policy>> GetPoliciesAsync(string accessToken)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync("http://203.115.11.236:10455/MobileAuthWS/api/CustPolicies");

            var custPolicies = JsonConvert.DeserializeObject<List<Cust_Policy>>(json);

            return custPolicies;

        }
    }
}
