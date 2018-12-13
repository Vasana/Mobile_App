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
    public class ApiServicesLife
    {
        private List<LifePolicy> _lifePolicyList;
        public int lifePolCount = 0;

        private List<Notification> _notifList;
        public int notifCount = 0;

        string IP = "http://203.115.11.236";
        string Port = "10455"; //Live 10455     Test 10155
        string Path = "";

        private LifePolicy _lifePolicy;

        public ApiServicesLife()
        {
            Path = IP+":"+Port;
        }

        public async Task<List<LifePolicy>> GetLifePoliciesAsync(string accessToken, int pageIndex, int pageSize)
        {
            try
            {
                if (SearchCriteriaLife.Instance.NewSearch)
                {
                    _lifePolicyList = null;
                    var json = JsonConvert.SerializeObject(SearchCriteriaLife.Instance);
                    HttpContent requestContent = new StringContent(json);
                    requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.PostAsync(Path+"/MobileAuthWS/api/Life/Getpolicies", requestContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        _lifePolicyList = JsonConvert.DeserializeObject<List<LifePolicy>>(responseContent);
                    }
                    else if (response.StatusCode.ToString() == "Unauthorized")
                    {
                        Application.Current.MainPage = new LoginPage();
                    }
                    
                    //_lifePolicyList = null;
                    //if (SearchCriteriaLife.Instance.lapsed_pol)
                    //{
                    //    _lifePolicyList = GetLifePolicyList2();
                    //}
                    //else
                    //{
                    //    _lifePolicyList = GetLifePolicyList();
                    //}

                    //GeneratePolicies();
                    if (_lifePolicyList != null)
                    {
                        lifePolCount = _lifePolicyList.Count;
                    }
                    else
                    {
                        lifePolCount = 0;
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
                SearchCriteriaLife.Instance.NewSearch = false;
                SearchCriteria.Instance.AllPolicies = false;
                SearchCriteriaLife.Instance.PolicyTable = "A";
                SearchCriteriaLife.Instance.inforce_pol = false;
                SearchCriteriaLife.Instance.templapse_pol = false;
                SearchCriteriaLife.Instance.lapsed_pol = false;
                SearchCriteriaLife.Instance.Flagged = false;
                SearchCriteriaLife.Instance.PolicyNumber = "";
                SearchCriteriaLife.Instance.TopTen = false;
                SearchCriteriaLife.Instance.TodayReminders = false;
                SearchCriteriaLife.Instance.NicNumber = "";
                SearchCriteriaLife.Instance.TableId = "";
    }
            if (lifePolCount >= pageSize)
            {
                return _lifePolicyList.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }
            else
            {
                return _lifePolicyList;
            }
            //-----------------------------------------------------------------------------------

            //return custPolicies; --- Original code

        }

        //public List<LifePolicy> GetLifePolicyList()
        //{
        //    List<LifePolicy> polList = new List<LifePolicy>
        //        {
        //            new LifePolicy
        //            {
        //                PolicyNumber = "123456",
        //                AgentCode = "923454",
        //                InsuredName = "Mrs. J.M.N. Weerathunga",
        //                ComDate = "2015-02-01",
        //                Premium = "2500.00",
        //                LastPaidDueDate = "2018-03-11",
        //                NoOfOutstandings = "3",
        //                PolTableNo = "15",
        //                PolTerm = "21",
        //                PolTableTerm = "15/21",
        //                PolicyStatus = "Active",
        //                IsSelected = false,
        //                PolTypeImage = "",
        //                PolStatusImage = "",
        //                MobileNumber = "0774556734",
        //                Flagged = false,
        //                FlagImage = "",
        //                RemindOnDate = "",
        //                CommentCreatedDate = "",
        //            },

        //             new LifePolicy
        //             {
        //                PolicyNumber = "123456",
        //                AgentCode = "923454",
        //                InsuredName = "Mrs. J.M.N. Weerathunga",
        //                ComDate = "2015-02-01",
        //                Premium = "2500.00",
        //                LastPaidDueDate = "2018-03-11",
        //                NoOfOutstandings = "3",
        //                PolTableNo = "15",
        //                PolTerm = "21",
        //                PolTableTerm = "15/21",
        //                PolicyStatus = "Active",
        //                IsSelected = false,
        //                PolTypeImage = "",
        //                PolStatusImage = "",
        //                MobileNumber = "0774556734",
        //                Flagged = false,
        //                FlagImage = "",
        //                RemindOnDate = "",
        //                CommentCreatedDate = "",
        //             },

        //             new LifePolicy
        //             {
        //                PolicyNumber = "123456",
        //                AgentCode = "923454",
        //                InsuredName = "Mrs. J.M.N. Weerathunga",
        //                ComDate = "2015-02-01",
        //                Premium = "2500.00",
        //                LastPaidDueDate = "2018-03-11",
        //                NoOfOutstandings = "3",
        //                PolTableNo = "15",
        //                PolTerm = "21",
        //                PolTableTerm = "15/21",
        //                PolicyStatus = "Active",
        //                IsSelected = false,
        //                PolTypeImage = "",
        //                PolStatusImage = "",
        //                MobileNumber = "0774556734",
        //                Flagged = false,
        //                FlagImage = "",
        //                RemindOnDate = "",
        //                CommentCreatedDate = "",

        //             },
        //    };
        //    return polList;
        //}

        //public List<LifePolicy> GetLifePolicyList2()
        //{
        //    List<LifePolicy> polList = new List<LifePolicy>
        //        {
        //            new LifePolicy
        //            {
        //                PolicyNumber = "123456",
        //                AgentCode = "923454",
        //                InsuredName = "Mrs. J.M.N. Weerathunga",
        //                ComDate = "2015-02-01",
        //                Premium = "2500.00",
        //                LastPaidDueDate = "2018-03-11",
        //                NoOfOutstandings = "3",
        //                PolTableNo = "15",
        //                PolTerm = "21",
        //                PolTableTerm = "15/21",
        //                PolicyStatus = "Active",
        //                IsSelected = false,
        //                PolTypeImage = "",
        //                PolStatusImage = "",
        //                MobileNumber = "0774556734",
        //                Flagged = false,
        //                FlagImage = "",
        //                RemindOnDate = "",
        //                CommentCreatedDate = "",
        //            },

        //             new LifePolicy
        //             {
        //                PolicyNumber = "123456",
        //                AgentCode = "923454",
        //                InsuredName = "Mrs. J.M.N. Weerathunga",
        //                ComDate = "2015-02-01",
        //                Premium = "2500.00",
        //                LastPaidDueDate = "2018-03-11",
        //                NoOfOutstandings = "3",
        //                PolTableNo = "15",
        //                PolTerm = "21",
        //                PolTableTerm = "15/21",
        //                PolicyStatus = "Active",
        //                IsSelected = false,
        //                PolTypeImage = "",
        //                PolStatusImage = "",
        //                MobileNumber = "0774556734",
        //                Flagged = false,
        //                FlagImage = "",
        //                RemindOnDate = "",
        //                CommentCreatedDate = "",
        //             },
                     
        //    };
        //    return polList;
        //}

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
                response = await client.PostAsync(Path+"/MobileAuthWS/api/Life/AddComment", requestContent);

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
                response = await client.PostAsync(Path+"/MobileAuthWS/api/Life/DeleteComment", requestContent);

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

        public async Task<List<Notification>> GetNotificationsAsync(string accessToken, int pageIndex, int pageSize)
        {
            this._notifList = null;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            var json = await client.GetStringAsync(Path+"/MobileAuthWS/api/Agent/GetNotifications");

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

        public async Task<bool> NotificsExistAsync(string accessToken)
        {
            bool ret = false;
            try
            {
                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                HttpResponseMessage response = new HttpResponseMessage();
                response = await client.GetAsync(Path+"/MobileAuthWS/api/Agent/CheckNotifications");

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

    }

}
