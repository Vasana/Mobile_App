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
        private List<CustPolicyLife> _lifePolicyList;
        public int lifePolCount = 0;

        private List<Notification> _notifList;
        public int notifCount = 0;

        private CustPolicyLife _lifePolicy;



        public async Task<List<CustPolicyLife>> GetLifePoliciesAsync(string accessToken, int pageIndex, int pageSize)
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
                    response = await client.PostAsync("http://203.115.11.236:10455/MobileAuthWS/api/Life/Getpolicies", requestContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        _lifePolicyList = JsonConvert.DeserializeObject<List<CustPolicyLife>>(responseContent);
                    }
                    else if (response.StatusCode.ToString() == "Unauthorized")
                    {
                        Application.Current.MainPage = new LoginPage();
                    }
                    
                    //_lifePolicyList = null;
                    //if (SearchCriteriaLife.Instance.lapsed_pol)
                    //{
                    //    _lifePolicyList = GetCustPolicyLifeList2();
                    //}
                    //else
                    //{
                    //    _lifePolicyList = GetCustPolicyLifeList();
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

        //public List<CustPolicyLife> GetCustPolicyLifeList()
        //{
        //    List<CustPolicyLife> polList = new List<CustPolicyLife>
        //        {
        //            new CustPolicyLife
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

        //             new CustPolicyLife
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

        //             new CustPolicyLife
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

        //public List<CustPolicyLife> GetCustPolicyLifeList2()
        //{
        //    List<CustPolicyLife> polList = new List<CustPolicyLife>
        //        {
        //            new CustPolicyLife
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

        //             new CustPolicyLife
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
                response = await client.PostAsync("http://203.115.11.236:10455/MobileAuthWS/api/Life/AddComment", requestContent);

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
                response = await client.PostAsync("http://203.115.11.236:10455/MobileAuthWS/api/Life/DeleteComment", requestContent);

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


        public async Task<LifePolicy> GetLifePolicyAsync(string accessToken, string policyNumber)
        {
            LifePolicy _lifPolicy = new LifePolicy();
            try
            {
                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                var json = await client.GetStringAsync("http://203.115.11.236:10155/MobileAuthWS/api/Life/Get_GET_POLICY_DETAILS?polNo=" + policyNumber.Trim());

                /*
                _lifPolicy.PolicyNumber = "0456544";
                _lifPolicy.InsuredName = "Saman Perera";
                _lifPolicy.Address = new List<string> { "No 01", "Hevelock Road", "Colombo 06" };
                _lifPolicy.ComDate = "2017/02/03";
                _lifPolicy.PolTable = "19";
                _lifPolicy.PolTerm = "20";
                _lifPolicy.PolDesc = "Divi Thilina";
                _lifPolicy.SumInsured = "5000000";
                _lifPolicy.PayTypeDesc = "Monthly";
                _lifPolicy.PolStatus = "Inforce";
                _lifPolicy.Premium = "20000";
                _lifPolicy.MobileNumber = "0774567643";

                */

                _lifPolicy = JsonConvert.DeserializeObject<LifePolicy>(json);

            }
            catch (Exception e)
            {

            }
            return _lifPolicy;

            //-----------------------------------------------------------------------------------

        }

        public async Task<List<LifeMember>> GetMemberDetailsAsync(string accessToken, string polNumber)
        {
            List<LifeMember> memberList = new List<LifeMember> { };
            try
            {
                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                var json = await client.GetStringAsync("http://203.115.11.236:10155/MobileAuthWS/api/Life/Get_GET_MEM_DETAILS?polNo=" + polNumber.Trim());

                memberList = JsonConvert.DeserializeObject<List<LifeMember>>(json);

                /*
                List<LifeMember> memberList = new List<LifeMember>
                {
                    new LifeMember
                    {
                        MemberType = "1",
                        Relationship = "Main Life",
                        FullName = "Saman Perera",
                        DateOfBirth = "1987/03/02",
                        Age = "31",
                        NicNumber = "874567345V",
                    },
                    new LifeMember
                    {
                        MemberType = "2",
                        Relationship = "Spouse",
                        FullName = "Himali Perera",
                        DateOfBirth = "1988/03/02",
                        Age = "30",
                        NicNumber = "884567345V",
                    }
                };
                */
            }
            catch (Exception e)
            {

            }

            return memberList;

            //-----------------------------------------------------------------------------------

            //return custPolicies; --- Original code
        }


        public async Task<List<LifeCover>> GetCoverDetailsAsync(string accessToken, string polNumber)
        {
            List<LifeCover> coverList = new List<LifeCover> { };
            try
            {
                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                var json = await client.GetStringAsync("http://203.115.11.236:10155/MobileAuthWS/api/Life/Get_GET_COVER_DETAILS?polNo=" + polNumber.Trim());

                coverList = JsonConvert.DeserializeObject<List<LifeCover>>(json);

            }
            catch (Exception e)
            {

            }

            return coverList;

            //-----------------------------------------------------------------------------------

            //return custPolicies; --- Original code
        }

        public async Task<List<LifePremiumDue>> GetPremiumDuesAsync(string accessToken, string polNumber)
        {
            List<LifePremiumDue> premiumList = new List<LifePremiumDue> { };
            try
            {
                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                var json = await client.GetStringAsync("http://203.115.11.236:10155/MobileAuthWS/api/Life/Get_GET_PREMIUM_DUES?polNo=" + polNumber.Trim());

                premiumList = JsonConvert.DeserializeObject<List<LifePremiumDue>>(json);

            }
            catch (Exception e)
            {
                //premiumList = null;
            }

            return premiumList;

            //-----------------------------------------------------------------------------------

            //return custPolicies; --- Original code
        }


    }



}
