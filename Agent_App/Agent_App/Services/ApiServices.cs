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
using System.Linq;
using System.Collections.ObjectModel;
using Agent_App.Views;
using Xamarin.Forms;
using System.Net;

namespace Agent_App.Services
{
    public class ApiServices
    {
        private List<CustPolicy> _policyList;
        public int policyCount = 0;

        private List<Notification> _notifList;
        public int notifCount = 0;        

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

        public async Task<string> changePassword(ChangePasswordBindingModel model, string accessToken)
        {
            string result = "";
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json);
                        
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

            HttpResponseMessage response = new HttpResponseMessage();
            response = await client.PostAsync("http://203.115.11.236:10455/MobileAuthWS/api/Account/ChangePassword", content);

            if (response.IsSuccessStatusCode)
            {
                result = "Successful";
            }
            else
            {
                result = response.ReasonPhrase.ToString();
            }


            return result;
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
            catch (Exception e)
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

        public async Task<List<CustPolicy>> GetPoliciesAsync(string accessToken, int pageIndex, int pageSize)
        {
            try
            {
                if (SearchCriteria.Instance.NewSearch)
                {
                    _policyList = null;
                    var json = JsonConvert.SerializeObject(SearchCriteria.Instance);
                    HttpContent requestContent = new StringContent(json);
                    requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.PostAsync("http://203.115.11.236:10455/MobileAuthWS/api/agent/getpolicies", requestContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        _policyList = JsonConvert.DeserializeObject<List<CustPolicy>>(responseContent);
                    }
                    else if (response.StatusCode.ToString() == "Unauthorized")
                    {                        
                        Application.Current.MainPage = new LoginPage();                        
                    }

                    //GeneratePolicies();
                    if (_policyList != null)
                    {
                        policyCount = _policyList.Count;
                    }
                    else
                    {
                        policyCount = 0;
                    }
                }                

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
            finally
            {
                SearchCriteria.Instance.NewSearch = false;
                SearchCriteria.Instance.BusinessType = "A";
                SearchCriteria.Instance.PremiumsPending = false;
                SearchCriteria.Instance.ClaimPending = false;
                SearchCriteria.Instance.Flagged = false;
                SearchCriteria.Instance.BadClaims = false;
                SearchCriteria.Instance.DebitOutstanding = false;
                SearchCriteria.Instance.AllPolicies = false;
                SearchCriteria.Instance.PolicyNumber = "";
                SearchCriteria.Instance.VehicleNumber = "";
                SearchCriteria.Instance.StartFromDt = "";
                SearchCriteria.Instance.StartToDt = "";
                SearchCriteria.Instance.TopTen = false;
                SearchCriteria.Instance.TodayReminders = false;
                SearchCriteria.Instance.MobileNumber = "";
                SearchCriteria.Instance.NicNumber = "";
                SearchCriteria.Instance.BusiRegNo = "";

            }
            if (policyCount >= pageSize)
            {
                return _policyList.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }
            else
            {
                return _policyList;
            }
            //-----------------------------------------------------------------------------------

            //return custPolicies; --- Original code
        }
        
        public async Task<bool> FlagPolicyAsync(string accessToken)
        {
            bool ret = false;
            try
            {               
                var json = JsonConvert.SerializeObject(PolicyFlag.Instance);
                HttpContent requestContent = new StringContent(json);
                requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage response = new HttpResponseMessage();
                response = await client.PostAsync("http://203.115.11.236:10455/MobileAuthWS/api/Agent/AddComment", requestContent);

                if (response.IsSuccessStatusCode)
                {
                    ret = true;
                }
                else if (response.StatusCode.ToString() == "Unauthorized")
                {
                    Application.Current.MainPage = new LoginPage();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            PolicyFlag.Instance.PolicyNumber = "";
            PolicyFlag.Instance.AgentCode = "";
            PolicyFlag.Instance.Flagged = false;
            PolicyFlag.Instance.Comment = "";
            PolicyFlag.Instance.RemindOnDate = "";
            PolicyFlag.Instance.CommentCreatedDate = "";

            return ret;         

        }

        public async Task<bool> UnFlagPolicyAsync(string accessToken)
        {
            bool ret = false;
            try
            {
                var json = JsonConvert.SerializeObject(PolicyFlag.Instance);
                HttpContent requestContent = new StringContent(json);
                requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage response = new HttpResponseMessage();
                response = await client.PostAsync("http://203.115.11.236:10455/MobileAuthWS/api/Agent/DeleteComment", requestContent);
                
                if (response.IsSuccessStatusCode)
                {
                    ret = true;
                }
                else if (response.StatusCode.ToString() == "Unauthorized")
                {
                    Application.Current.MainPage = new LoginPage();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            PolicyFlag.Instance.PolicyNumber = "";
            PolicyFlag.Instance.AgentCode = "";
            PolicyFlag.Instance.Flagged = false;
            PolicyFlag.Instance.Comment = "";
            PolicyFlag.Instance.RemindOnDate = "";
            PolicyFlag.Instance.CommentCreatedDate = "";

            return ret;
        }

        public async Task<GeneralPolicy> GetGenPolicyAsync(string accessToken, string dept, string policyNumber)
        {
            GeneralPolicy _genPolicy = new GeneralPolicy();
            try
            {
                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                var json = await client.GetStringAsync("http://203.115.11.236:10455/MobileAuthWS/api/Agent/getGeneralPolicyInfo?policyNo=" + policyNumber.Trim());

                _genPolicy = JsonConvert.DeserializeObject<GeneralPolicy>(json);

            }
            catch (Exception e)
            {

            }
            return _genPolicy;

            //-----------------------------------------------------------------------------------

        }

        public async Task<AppVersions> GetAppVersionAsync(string buildNumber)
        {
            AppVersions _appVersion = new AppVersions();
            try
            {
                var client = new HttpClient();

                var json = await client.GetStringAsync("http://203.115.11.236:10455/MobileAuthWS/api/Agent/GetVersionInfo?BuildNo=" + buildNumber.Trim());

                _appVersion = JsonConvert.DeserializeObject<AppVersions>(json);

            }
            catch (Exception e)  
            {
                _appVersion = null;
            }
            return _appVersion;

            //-----------------------------------------------------------------------------------

        }

        public async Task<List<Products>> GetProducts(string stream, string accessToken)
        {
            List<Products> _ProductList;
           // _ProductList = new IList<Products>();
            try
            {
                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                var json = await client.GetStringAsync("http://203.115.11.236:10455/MobileAuthWS/api/Agent/GetProductList?businessStream=" + stream.Trim());

                _ProductList = JsonConvert.DeserializeObject<List<Products>>(json);

            }
            catch (Exception e)
            {
                _ProductList = null;
            }
            return _ProductList;

            //-----------------------------------------------------------------------------------

        }   
         
        public async Task<List<BranchContact>> GetBranchContactsAsync(string accessToken)
        {
             var client = new HttpClient();

             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

             var json = await client.GetStringAsync("http://203.115.11.236:10455/MobileAuthWS/api/Agent/GetBranches");

             var branchList = JsonConvert.DeserializeObject<List<BranchContact>>(json);
            
            return branchList;

            //-----------------------------------------------------------------------------------

            //return custPolicies; --- Original code
        }

        public List<AgentPerformance> GetTeamPerformance(string accessToken, string OrgCode, string fromDate, string toDate)
        {
            List<AgentPerformance> claimsList = null;
            try
            {
                //    var client = new HttpClient();
                //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                //    var json = await client.GetStringAsync("http://203.115.11.236:10455/MobileAuthWS/api/Agent/getAgentPerfomance_byDate?agentCode=" + agentCode.Trim()+ "&year="+year);
                //    claimsList = JsonConvert.DeserializeObject<List<MonthlyPerformance>>(json);

                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add("Content-Type", "text");
                    wc.Headers[HttpRequestHeader.Authorization] = "Bearer " + accessToken;
                    var json = wc.DownloadString("http://203.115.11.236:10455/MobileAuthWS/api/Agent/getTeamPerfomance_byDate?organizerCode=" + OrgCode.Trim() + "&fromDate=" + fromDate + "&toDate=" + toDate);
                    claimsList = JsonConvert.DeserializeObject<List<AgentPerformance>>(json);
                }
            }
            catch (Exception e)
            {
                claimsList = null;
            }
            return claimsList;
            
        }


        public async Task<List<AgtPerfmStat>> GetTeamPerformanceAsync(string accessToken, string OrgCode, string fromDate, string toDate)
        {
            List<AgentPerformance> claimsList = null;
            List<AgtPerfmStat> agent_performanceList = null;
            try
            {
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                    var json = await client.GetStringAsync("http://203.115.11.236:10455/MobileAuthWS/api/Agent/getTeamPerfomance_byDate?organizerCode=" + OrgCode.Trim() + "&fromDate=" + fromDate + "&toDate=" + toDate);
                 claimsList = JsonConvert.DeserializeObject<List<AgentPerformance>>(json);

                


                //using (WebClient wc = new WebClient())
                //{
                //    wc.Headers.Add("Content-Type", "text");
                //    wc.Headers[HttpRequestHeader.Authorization] = "Bearer " + accessToken;
                //    var json = wc.DownloadString(""http://203.115.11.236:10455/MobileAuthWS/api/Agent/getTeamPerfomance_byDate?organizerCode=" + OrgCode.Trim() + "&fromDate=" + fromDate + "&toDate=" + toDate);
                //    claimsList = JsonConvert.DeserializeObject<List<AgentPerformance>>(json);
                //}
            }
            catch (Exception e)
            {
                claimsList = null;
            }
            return agent_performanceList;

        }


        public async Task<List<Notification>> GetNotificationsAsync(string accessToken, int pageIndex, int pageSize)
        {
             this._notifList = null;

              var client = new HttpClient();
              client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
             var json = await client.GetStringAsync("http://203.115.11.236:10455/MobileAuthWS/api/Agent/GetNotifications");

             _notifList = JsonConvert.DeserializeObject<List<Notification>>(json); 
            
            if (_notifList != null)
            {
                notifCount = _notifList.Count;               
            }
            else
            {
                notifCount = 0;
            }

            if (notifCount >= pageSize)
            {
                return _notifList.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }
            else
            {
                return _notifList;
            }            
        }

        public async Task<List<Notification>> ClearNotifAsync(string accessToken, List<Notification> notifList, int pageIndex, int pageSize)
        {
            if (notifList != null)
            {
                try
                {
                    _notifList = null;
                    var json = JsonConvert.SerializeObject(notifList);
                    HttpContent requestContent = new StringContent(json);
                    requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.PostAsync("http://203.115.11.236:10455/MobileAuthWS/api/agent/DeleteNotification", requestContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        _notifList = JsonConvert.DeserializeObject<List<Notification>>(responseContent);
                    }
                    else if (response.StatusCode.ToString() == "Unauthorized")
                    {
                        Application.Current.MainPage = new LoginPage();
                    }

                    //GeneratePolicies();
                    if (_notifList != null)
                    {
                        notifCount = _notifList.Count;
                    }
                    else
                    {
                        notifCount = 0;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    //throw;
                }
            }
            if (notifCount >= pageSize)
            {
                return _notifList.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }
            else
            {
                return _notifList;
            }
            //-----------------------------------------------------------------------------------

            //return custPolicies; --- Original code
        }

        public async Task<bool> ClearNotifAsync_1(string accessToken, Notification notif)
        {
            bool ret = false;
            try
            {
                var json = JsonConvert.SerializeObject(notif);
                HttpContent requestContent = new StringContent(json);
                requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage response = new HttpResponseMessage();
                response = await client.PostAsync("http://203.115.11.236:10455/MobileAuthWS/api/Agent/DeleteNotification", requestContent);

                if (response.IsSuccessStatusCode)
                {
                    ret = true;
                }
                else if (response.StatusCode.ToString() == "Unauthorized")
                {
                    Application.Current.MainPage = new LoginPage();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
            
            if (ret)
            {
                notifCount -= notifCount;
            }
            return ret;
        }

        public async Task<bool> NotificsExistAsync(string accessToken)
        {
            bool ret = false;
            try
            {
                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                HttpResponseMessage response = new HttpResponseMessage();
                response = await client.GetAsync("http://203.115.11.236:10455/MobileAuthWS/api/Agent/CheckNotifications");

                if (response.IsSuccessStatusCode)
                {
                    ret = true;
                }
                else if (response.StatusCode.ToString() == "Unauthorized")
                {
                    Application.Current.MainPage = new LoginPage();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }         
            return ret;
        }

        public async Task<List<ClaimHistory>> GetClaimHistoryAsync(string accessToken, string polNumber)
        {
            List<ClaimHistory> claimsList = null;
            try
            {
                var client = new HttpClient();   
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                var json = await client.GetStringAsync("http://203.115.11.236:10455/MobileAuthWS/api/Agent/GetClaimHistory?policyNo=" + polNumber.Trim());
                claimsList = JsonConvert.DeserializeObject<List<ClaimHistory>>(json);               
            }
            catch(Exception e)
            {
                claimsList = null; 
            }
            return claimsList;

            //-----------------------------------------------------------------------------------

            //return custPolicies; --- Original code
        }

        public async Task<List<PremiumHistory>> GetPremiumHistoryAsync(string accessToken, string polNumber)
        {
            List<PremiumHistory> premiumsList = null;
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                var json = await client.GetStringAsync("http://203.115.11.236:10455/MobileAuthWS/api/Agent/GetPremiumHistory?policyNo=" + polNumber.Trim());
                premiumsList = JsonConvert.DeserializeObject<List<PremiumHistory>>(json);
            }
            catch (Exception e)
            {
                premiumsList = null;
            }
            return premiumsList;

            //-----------------------------------------------------------------------------------

            //return custPolicies; --- Original code
        }

        public List<MonthlyPerformance> GetMonthlyPerformance(string accessToken, string agentCode, string year)
        {
            List<MonthlyPerformance> claimsList = null;
            try
            {
                //    var client = new HttpClient();
                //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                //    var json = await client.GetStringAsync("http://203.115.11.236:10455/MobileAuthWS/api/Agent/getMonthlyPreformance?agentCode=" + agentCode.Trim()+ "&year="+year);
                //    claimsList = JsonConvert.DeserializeObject<List<MonthlyPerformance>>(json);

                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add("Content-Type", "text");
                    wc.Headers[HttpRequestHeader.Authorization] = "Bearer " + accessToken;
                    var json = wc.DownloadString("http://203.115.11.236:10455/MobileAuthWS/api/Agent/getMonthlyPreformance?agentCode=" + agentCode.Trim() + "&year=" + year);
                    claimsList = JsonConvert.DeserializeObject<List<MonthlyPerformance>>(json); 
                }
            }
            catch (Exception e)
            {
                claimsList = null;
            }
            return claimsList;

            //-----------------------------------------------------------------------------------

            //return custPolicies; --- Original code
        }

        public List<AgentPerformance> GetAgentPerformance(string accessToken, string agentCode, string fromDate, string toDate)
        {
            List < AgentPerformance> claimsList = null;
            try
            {
                //    var client = new HttpClient();
                //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                //    var json = await client.GetStringAsync("http://203.115.11.236:10455/MobileAuthWS/api/Agent/getAgentPerfomance_byDate?agentCode=" + agentCode.Trim()+ "&year="+year);
                //    claimsList = JsonConvert.DeserializeObject<List<MonthlyPerformance>>(json);

                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add("Content-Type", "text");
                    wc.Headers[HttpRequestHeader.Authorization] = "Bearer " + accessToken;
                    var json = wc.DownloadString("http://203.115.11.236:10455/MobileAuthWS/api/Agent/getAgentPerfomance_byDate?agentCode=" + agentCode.Trim() + "&fromDate=" + fromDate+"&toDate="+toDate);
                    claimsList = JsonConvert.DeserializeObject< List<AgentPerformance>>(json);
                }
            }
            catch (Exception e)
            {
                claimsList = null;
            }
            return claimsList;

            //-----------------------------------------------------------------------------------

            //return custPolicies; --- Original code
        }            

        public async Task<AgentProfile> GetAgentProfile(string accessToken)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync("http://203.115.11.236:10455/MobileAuthWS/api/Agent/getAgentProfile");

            AgentProfile agentProf = JsonConvert.DeserializeObject<AgentProfile>(json);

            return agentProf;

            //-----------------------------------------------------------------------------------

            //return custPolicies; --- Original code
        }


        public async Task<bool> WritetoAuditLog(Audit_trail au, string accessToken)
        {
            bool result = false;
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(au);
            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

            HttpResponseMessage response = new HttpResponseMessage();
            response = await client.PostAsync("http://203.115.11.236:10455/MobileAuthWS/api/Agent/WriteToAuditTrail", content);

            if (response.IsSuccessStatusCode)
            {
                result = true;
            }
            else
            {
                result = false;
            }


            return result;
        }
    }
}
