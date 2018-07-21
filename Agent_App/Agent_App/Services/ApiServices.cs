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

namespace Agent_App.Services
{
    public class ApiServices
    {
        private List<CustPolicy> _policyList;
        public int policyCount = 0;

        private List<Notification> _notifList;
        public int notifCount = 0;

        private LifePolicy _lifePolicy;

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
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync("http://203.115.11.236:10455/MobileAuthWS/api/Agent/getGeneralPolicyInfo?policyNo=" + policyNumber.Trim());

            var _genPolicy = JsonConvert.DeserializeObject<GeneralPolicy>(json);
            
            return _genPolicy;
             
            //-----------------------------------------------------------------------------------

            
        }

        public async Task<LifePolicy> GetLifePolicyAsync(string accessToken, string dept, string policyNumber)
        {
            /* var client = new HttpClient();

             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

             var json = await client.GetStringAsync("http://203.115.11.236:10455/MobileAuthWS/api/CustPolicies");

             var custPolicies = JsonConvert.DeserializeObject<List<CustPolicy>>(json);*/// Original code

            //---------------------only for testing---------------------------------------
            _lifePolicy = new LifePolicy
            {
                PolicyNumber = "VM1115003410000519",
                InsuredName = "H.K.K.T.DUMINDA",
                Address = new List<string> { "No 2", "Temple Road", "Colombo 03" },
               // VehicleNumber = "DH 1234",
                StartDate = "22-JUN-17",
                EndDate = "22-JUN-18",
                SumInsured = "G",
                AdditionalCovers = new List<string> { "Cover 1", "Cover 2", "Cover 3" }
            };

            await Task.Delay(2000);
            return _lifePolicy;

            //-----------------------------------------------------------------------------------

            //return custPolicies; --- Original code

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

        private List<BranchContact> GetBranchesList()
        {
            List<BranchContact> BranchList = new List<BranchContact>
                {
                new BranchContact
                    {
                        Name = "Head Office",
                        District = "Colombo",
                        Address = "No. 21, Vauxall Strt, Colombo 02",
                        ContactNumber1 =  "0112343344",
                        ContactNumber2 = "0774544334",
                        ContactNumber3 = "0112334555",
                        ContactNumber4 = "0774544334",
                        SMContactNumber = "0772334554",
                        RSMContactNumber = "0774567956",
                        BrnAdminContactNo = "0778455644",
                    },
                new BranchContact
                    {
                        Name = "Kotahena",
                        District = "Colombo",
                        Address = "No. 178, Gold Tower, Colombo 13",
                        ContactNumber1 =  "011256775",
                        ContactNumber2 = "0774544334",
                        ContactNumber3 = "0112334555",
                        ContactNumber4 = "0774544334",
                        SMContactNumber = "0772334554",
                        RSMContactNumber = "0774567956",
                        BrnAdminContactNo = "0778455644",
                    },
                    new BranchContact
                    {
                        Name = "Anuradhapura Branch",
                        District = "Anuradhapura",
                        Address = "81, Thalawa Rd",
                        ContactNumber1 =  "0112334564",
                        ContactNumber2 = "0774544334",
                        ContactNumber3 = "0112334555",
                        ContactNumber4 = "0774544334",
                        SMContactNumber = "0772334554",
                        BrnAdminContactNo = "0778455644",
                    },
                    new BranchContact
                    {
                        Name = "Kekirawa Branch",
                        District = "Anuradhapura",
                        Address = "31, Main St",
                        ContactNumber1 =  "011233345",
                        ContactNumber2 = "0774544334",
                        ContactNumber3 = "0112334555",
                        ContactNumber4 = "0774544334",
                    },
                    new BranchContact
                    {
                        Name = "Badulla Branch",
                        District = "Badulla",
                        Address = "14, Badulla Rd",
                        ContactNumber1 =  "0112343344",
                        ContactNumber2 = "0774544334",
                        ContactNumber3 = "0112334555",
                        SMContactNumber = "0772334554",
                        BrnAdminContactNo = "0778455644",

                    },
                    new BranchContact
                    {
                        Name = "Ampara Branch",
                        District = "Badulla",
                        Address = "01, Iginiyagala Rd",
                        ContactNumber1 =  "0112343344",
                        ContactNumber2 = "0774544334",
                        ContactNumber3 = "0112334555",
                        SMContactNumber = "0772334554",
                        RSMContactNumber = "0774567956",
                        BrnAdminContactNo = "0778455644",

                    },
                    new BranchContact
                    {
                        Name = "Hambantota Branch",
                        District = "Hambantota",
                        Address = "59, Main St",
                        ContactNumber1 =  "0112343344",
                        ContactNumber3 = "0112334555",
                        ContactNumber4 = "0774544334",
                        SMContactNumber = "0772334554",
                        RSMContactNumber = "0774567956",
                        BrnAdminContactNo = "0778455644",
                    },
                    new BranchContact
                    {
                        Name = "Beliatta Branch",
                        District = "Hambantota",
                        Address = "74, Tangalla Rd",
                        ContactNumber1 =  "0112343344",
                        ContactNumber3 = "0112334555",
                        ContactNumber4 = "0774544334",
                        SMContactNumber = "0772334554",
                        RSMContactNumber = "0774567956",
                        BrnAdminContactNo = "0778455644",
                    },
                    new BranchContact
                    {
                        Name = "Jaffna Branch",
                        District = "Jaffna",
                        Address = "571, Hospital Rd",
                        ContactNumber1 =  "0112343344",
                        ContactNumber2 = "0774544334",
                        ContactNumber3 = "0112334555",
                        ContactNumber4 = "0774544334",
                    },
                    new BranchContact
                    {
                        Name = "Mannar Branch",
                        District = "Jaffna",
                        Address = "Station Rd (Opposite Pakiya Studio)",
                        ContactNumber1 =  "0112343344",
                        ContactNumber2 = "0774544334",
                        ContactNumber3 = "0112334555",
                        ContactNumber4 = "0774544334",
                        SMContactNumber = "0772334554",
                        RSMContactNumber = "0774567956",
                        BrnAdminContactNo = "0778455644",
                    },
                    new BranchContact
                    {
                        Name = "Kalutara Branch",
                        District = "Kalutara",
                        Address = "55 1/1, Paranagama Bldg, Galle Rd",
                        ContactNumber1 =  "0112343344",
                        ContactNumber2 = "0774544334",
                        ContactNumber3 = "0112334555",
                        ContactNumber4 = "0774544334",
                    },
                    new BranchContact
                    {
                        Name = "Beruwala Branch",
                        District = "Kalutara",
                        Address = "167/1, Galle Rd",
                        ContactNumber1 =  "0112343344",
                        ContactNumber2 = "0774544334",
                        ContactNumber3 = "0112334555",
                        ContactNumber4 = "0774544334",
                        SMContactNumber = "0772334554",
                        RSMContactNumber = "0774567956",
                        BrnAdminContactNo = "0778455644",
                    },
                    new BranchContact
                    {
                        Name = "Kurunegala Branch",
                        District = "Kurunegala",
                        Address = "16/2, Dambulla Rd",
                        ContactNumber1 =  "0112343344",
                        ContactNumber2 = "0774544334",
                       
                    },
                    new BranchContact
                    {
                        Name = "Giriulla Branch",
                        District = "Kurunegala",
                        Address = "101, Negombo Rd",
                        ContactNumber1 =  "0112343344",
                        ContactNumber2 = "0774544334",
                        SMContactNumber = "0772334554",
                        RSMContactNumber = "0774567956",
                        BrnAdminContactNo = "0778455644",
                    },
                    new BranchContact
                    {
                        Name = "Galle Branch",
                        District = "Galle",
                        Address = "20 A, Havelock Rd",
                        ContactNumber1 =  "0112343344",
                        ContactNumber2 = "0774544334",
                        ContactNumber3 = "0112334555",
                        
                    },
                    new BranchContact
                    {
                        Name = "Rathnapura Branch",
                        District = "Rathnapura",
                        Address = "77, Rathnapura Rd",
                        ContactNumber1 =  "0112343344",
                        ContactNumber2 = "0774544334",
                        ContactNumber3 = "0112334555",
                        ContactNumber4 = "0774544334",
                    },
                    new BranchContact
                    {
                        Name = "Negombo Branch",
                        District = "Negombo",
                        Address = "20, Negombo Rd",
                        ContactNumber1 =  "0112343344",
                        ContactNumber2 = "0774544334",
                        ContactNumber3 = "0112334555",
                        ContactNumber4 = "0774544334",
                        SMContactNumber = "0772334554",
                        RSMContactNumber = "0774567956",
                        BrnAdminContactNo = "0778455644",
                    },
                    new BranchContact
                    {
                        Name = "Matara Branch",
                        District = "Matara",
                        Address = "5A, Hakmana Rd",
                        ContactNumber1 =  "0112343344",
                        ContactNumber2 = "0774544334",
                        ContactNumber3 = "0112334555",
                        ContactNumber4 = "0774544334",
                        SMContactNumber = "0772334554",
                        RSMContactNumber = "0774567956",
                        BrnAdminContactNo = "0778455644",
                    },
                    

                    };

            return BranchList;
        }

        private List<Notification> GetNotificationsList()
        {
            List<Notification> NotifsList = new List<Notification>
                {
                new Notification
                    {
                        PolicyNumber = "VM23456777445667",
                        Department = "M",
                        BusinessType = "G",
                        InsuredName = "Nihal Fernando",
                        ContactNumber = "0775666777",
                        EventName = "Claim Intimation",
                        EventDescription = "Claim Intimated on 02/07/2018",
                        EventImage = "clmIntimated.png",
                        NotifiedDate = DateTime.Now,
                    },
                new Notification
                    {
                        PolicyNumber = "A/P/00045653322/N",
                        Department = "M",
                        BusinessType = "G",
                        InsuredName = "Roshan Fernando",
                        ContactNumber = "0778456544",
                        EventName = "Claim Rejection",
                        EventDescription = "Claim rejected on 02/07/2018 due to insufficient information and not submitted on time",
                        EventImage = "clmRejected.jpg",
                        NotifiedDate = DateTime.Now,
                    },
                new Notification
                    {
                        PolicyNumber = "VM0056334567652",
                        Department = "M",
                        BusinessType = "G",
                        InsuredName = "Thilak Ranathunga",
                        ContactNumber = "0778945644",
                        EventName = "Claim Intimation",
                        EventDescription = "Claim Intimated on 02/07/2018",
                        EventImage = "clmIntimated.png",
                        NotifiedDate = DateTime.Now,
                    },
                new Notification
                    {
                        PolicyNumber = "SHE/00878889/SN",
                        Department = "G",
                        BusinessType = "G",
                        InsuredName = "Rani Perera",
                        ContactNumber = "0775666777",
                        EventName = "Hospitalization",
                        EventDescription = "Hospitalized on 02/07/2018",
                        EventImage = "hospitalized.png",
                        NotifiedDate = DateTime.Now,
                    },
                  };

            return NotifsList;
        }

        public async Task<ObservableCollection<CustPolicy>> GetTodaysLifeRemindsAsync(string accessToken)
        {
            /* var client = new HttpClient();

             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

             var json = await client.GetStringAsync("http://203.115.11.236:10455/MobileAuthWS/api/CustPolicies");

             var custPolicies = JsonConvert.DeserializeObject<List<CustPolicy>>(json);*/// Original code

            //---------------------only for testing---------------------------------------
            var remindList = GenerateFlaggedForTodayLife();

            await Task.Delay(2000);
            return remindList;

            //-----------------------------------------------------------------------------------

            //return custPolicies; --- Original code

        } 

        public ObservableCollection<CustPolicy> GenerateFlaggedForTodayLife()
        {
            var polList = new ObservableCollection<CustPolicy>
                {
                    new CustPolicy
                    {
                        PolicyNumber = "968264",
                        AgentCode="111558" ,
                        InsuredName = "H.K.K.T.DUMINDA",
                        StartDate = "22-JUN-17",
                        EndDate = "22-JUN-18",
                        Department = "G",
                        PolicyType = "PA",
                        PolTypeDesc = "Praguna",
                        VehicleNumber = "",
                        PolTypeImage = "health.png",
                        PolStatusImage = "tick.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        FlagImage = "filledStar.png",
                        Flagged = true,
                        AgentComment = "Have to call the customer and find out the issue in claim. need to know if all documents submitted and requirements completed.",
                    },

                     new CustPolicy
                     {
                        PolicyNumber = "13713",
                        AgentCode="111558" ,
                        InsuredName = "W.A.D.N PERERA",
                        StartDate = "02-JUN-18",
                        EndDate = "02-JUN-19",
                        Department = "G",
                        PolicyType = "HC",
                        PolTypeDesc = "Divi Thilina",
                        VehicleNumber = "",
                        PolTypeImage = "health.png",
                        PolStatusImage = "tick.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        FlagImage = "filledStar.png",
                        Flagged = true,
                        AgentComment = "test comment, have to look into the claim pending",
                     },

                     new CustPolicy
                     {
                        PolicyNumber = "931973",
                        AgentCode="111558" ,
                        InsuredName = "Mr. S.L.S.GUNARATHNA",
                        StartDate = "16-JUN-18",
                        EndDate = "15-JUN-19",
                        Department = "M",
                        PolicyType = "M11",
                        PolTypeDesc = "Praguna",
                        VehicleNumber = "CAH 0945",
                        PolTypeImage = "health.png",
                        PolStatusImage = "tick.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        MotorPolicy = true,
                        FlagImage = "filledStar.png",
                        Flagged = true,

                     },
            };



            return polList;
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
    }
}
