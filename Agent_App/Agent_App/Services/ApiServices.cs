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

namespace Agent_App.Services
{
    public class ApiServices
    {
        private List<CustPolicy> _policyList;

        public int policyCount = 0;

        private GeneralPolicy _genPolicy;
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
            /* var client = new HttpClient();

             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

             var json = await client.GetStringAsync("http://203.115.11.236:10455/MobileAuthWS/api/CustPolicies");

             var custPolicies = JsonConvert.DeserializeObject<List<CustPolicy>>(json);*/// Original code

            //---------------------only for testing---------------------------------------

            if (SearchCriteria.Instance.NewSearch)
            {
                if (SearchCriteria.Instance.PremiumsPending)
                {
                    GeneratePoliciesPending();
                }
                else if (SearchCriteria.Instance.ClaimPending)
                {
                    GenerateClaimPending();
                }
                else if (SearchCriteria.Instance.Flagged)
                {
                    GenerateFlagged();
                }
                else
                {
                    GeneratePolicies();
                }

                await Task.Delay(2000);
                policyCount = _policyList.Count;

                SearchCriteria.Instance.NewSearch = false;
                SearchCriteria.Instance.PremiumsPending = false;
                SearchCriteria.Instance.ClaimPending = false;
                SearchCriteria.Instance.Flagged = false;

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

        public async Task<GeneralPolicy> GetGenPolicyAsync(string accessToken, string dept, string policyNumber)
        {
            /* var client = new HttpClient();

             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

             var json = await client.GetStringAsync("http://203.115.11.236:10455/MobileAuthWS/api/CustPolicies");

             var custPolicies = JsonConvert.DeserializeObject<List<CustPolicy>>(json);*/// Original code

            //---------------------only for testing---------------------------------------
            _genPolicy = new GeneralPolicy
            {
                PolicyNumber = "VM1115003410000519",
                InsuredName = "H.K.K.T.DUMINDA",
                Address = new List<string> { "No 2", "Temple Road", "Manel Avenue", "Colombo 04" },
                VehicleNumber = "DH 1234",
                StartDate = "22-JUN-17",
                EndDate = "22-JUN-18",
                SumInsured = "Rs. 2500000.00",
                AdditionalCovers = new List<string> { "Special Windscreen cover", "Voluntary Excess", "Terrorism cover" }
            };

            await Task.Delay(2000);
            return _genPolicy;

            //-----------------------------------------------------------------------------------

            //return custPolicies; --- Original code

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

        public async Task<ObservableCollection<CustPolicy>> GetTodaysGenrlRemindsAsync(string accessToken)
        {
            /* var client = new HttpClient();

             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

             var json = await client.GetStringAsync("http://203.115.11.236:10455/MobileAuthWS/api/CustPolicies");

             var custPolicies = JsonConvert.DeserializeObject<List<CustPolicy>>(json);*/// Original code

            //---------------------only for testing---------------------------------------
            var remindList = GenerateFlaggedForToday();

            await Task.Delay(2000);
            return remindList;

            //-----------------------------------------------------------------------------------

            //return custPolicies; --- Original code

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

        public GeneralPolicy GenerateGenPolicy()
        {
            GeneralPolicy genPolicy = new GeneralPolicy
            {
                PolicyNumber = "VM1115003410000519",
                InsuredName = "H.K.K.T.DUMINDA",
                Address = { "No 2", "Temple Road", "Colombo 03" },
                VehicleNumber = "DH 1234",
                StartDate = "22-JUN-17",
                EndDate = "22-JUN-18",
                SumInsured = "G",
                AdditionalCovers = { "Cover 1", "Cover 2", "Cover 3" },
            };

            return genPolicy;
        }


        private void GeneratePolicies()
        {
            // for (var i = 0; i < 10; i++)
            {
                _policyList = new List<CustPolicy>
                {
                    new CustPolicy
                    {
                         PolicyNumber = "VM1115003410000506",
                         AgentCode="111558" ,
                         InsuredName = "Mr. N.A.A.R. NEHTHASINGHE",
                         StartDate = "12-JUN-18",
                         EndDate = "11-JUN-19",
                         Department = "M",
                         PolicyType = "M11",
                         PolTypeDesc = "Motor - Comprehensive",
                         VehicleNumber = "CAG 8842",
                         PolTypeImage = "car.png",
                         PolStatusImage = "tick.png",
                         ClaimStatusImage = "claim_pending.png",
                         MobileNumber = "",
                         MotorPolicy = true,
                         FlagImage = "starFrame.png",
                    },

                     new CustPolicy
                    {
                         PolicyNumber = "G/010/AMP/17/00577",
                         AgentCode="111558" ,
                         InsuredName = "H.L.DIAS",
                         StartDate = "13-JUN-18",
                         EndDate = "12-JUN-19",
                         Department = "G",
                         PolicyType = "AMP",
                         PolTypeDesc = "Annual Medical Plan",
                         VehicleNumber = "",
                         PolTypeImage = "health.png",
                         PolStatusImage = "tick.png",
                         ClaimStatusImage = "tick.png",
                         MobileNumber = "0766980982",
                         FlagImage = "starFrame.png",
                     },

                      new CustPolicy
                    {
                        PolicyNumber = "VM1115033710005662",
                        AgentCode="111558" ,
                        InsuredName = "Mr. S.M.W.S.B. KARUNAWARDENA",
                        StartDate = "25-FEB-17",
                        EndDate = "24-FEB-18",
                        Department = "M",
                        PolicyType = "M11",
                        PolTypeDesc = "Motor - Comprehensive",
                        VehicleNumber = "CAE 5077",
                        PolTypeImage = "car.png",
                        PolStatusImage = "alert_red.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        MotorPolicy = true,
                        FlagImage = "starFrame.png",
            },

                       new CustPolicy
                     {
                        PolicyNumber = "A/11/0484596/010/P",
                        AgentCode="111558" ,
                        InsuredName = "Dr. T.R.C. RUBERU",
                        StartDate = "14-MAY-18",
                        EndDate = "13-MAY-19",
                        Department = "M",
                        PolicyType = "M11",
                        PolTypeDesc = "Motor - Comprehensive",
                        VehicleNumber = "KJ  7030",
                        PolTypeImage = "car.png",
                        PolStatusImage = "tick.png",
                        ClaimStatusImage = "claim_pending.png",
                        MobileNumber = "0766980982",
                        MotorPolicy = true,
                        FlagImage = "starFrame.png",
            },

                    new CustPolicy
                    {
                        PolicyNumber = "G/010/PA/37241",
                        AgentCode="111558" ,
                        InsuredName = "H.K.K.T.DUMINDA",
                        StartDate = "22-JUN-17",
                        EndDate = "22-JUN-18",
                        Department = "G",
                        PolicyType = "PA",
                        PolTypeDesc = "Personal Accident",
                        VehicleNumber = "",
                        PolTypeImage = "life.png",
                        PolStatusImage = "alert_yellow.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        FlagImage = "starFrame.png",
                    },

                        new CustPolicy
                    {
                        PolicyNumber = "G/094/AMP/17/00559",
                        AgentCode="111558" ,
                        InsuredName = "H.M.L.ANURADHA KARUNATHILAKE",
                        StartDate = "17-MAY-18",
                        EndDate = "16-MAY-19",
                        Department = "G",
                        PolicyType = "AMP",
                        PolTypeDesc = "Annual Medical Plan",
                        VehicleNumber = "",
                        PolTypeImage = "health.png",
                        PolStatusImage = "tick.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        FlagImage = "starFrame.png",
                     },

                     new CustPolicy
                    {
                        PolicyNumber = "VM1116001110000602",
                        AgentCode="111558" ,
                        InsuredName = "Mr. B.C.R.D.S DE SILVA",
                        StartDate = "08-JUN-18",
                        EndDate = "07-JUN-19",
                        Department = "M",
                        PolicyType = "M11",
                        PolTypeDesc = "Motor - Comprehensive",
                        VehicleNumber = "KY  5427",
                        PolTypeImage = "car.png",
                        PolStatusImage = "tick.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        MotorPolicy = true,
                        FlagImage = "starFrame.png",
                    },

                      new CustPolicy
                    {
                        PolicyNumber = "FFBP170101000287",
                        AgentCode="111558" ,
                        InsuredName = "F.R.N Leitan",
                        StartDate = "10-MAY-17",
                        EndDate = "09-MAY-18",
                        Department = "F",
                        PolicyType = "FBP",
                        PolTypeDesc = "Fire Policy",
                        VehicleNumber = "",
                        PolTypeImage = "car.png",
                        PolStatusImage = "alert_red.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        FlagImage = "starFrame.png",
                     },

                     new CustPolicy
                     {
                        PolicyNumber = "GHC170101000031",
                        AgentCode="111558" ,
                        InsuredName = "W.A.D.N PERERA",
                        StartDate = "02-JUN-18",
                        EndDate = "02-JUN-19",
                        Department = "G",
                        PolicyType = "HC",
                        PolTypeDesc = "Home Protect",
                        VehicleNumber = "",
                        PolTypeImage = "home.png",
                        PolStatusImage = "tick.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        FlagImage = "starFrame.png",
                     },

                     new CustPolicy
                     {
                        PolicyNumber = "VM1115003410000519",
                        AgentCode="111558" ,
                        InsuredName = "Mr. S.L.S.GUNARATHNA",
                        StartDate = "16-JUN-18",
                        EndDate = "15-JUN-19",
                        Department = "M",
                        PolicyType = "M11",
                        PolTypeDesc = "Motor - Comprehensive",
                        VehicleNumber = "CAH 0945",
                        PolTypeImage = "car.png",
                        PolStatusImage = "tick.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        MotorPolicy = true,
                        FlagImage = "starFrame.png",
                     },


                     new CustPolicy
                    {
                         PolicyNumber = "VM111500341000066765",
                         AgentCode="111558" ,
                         InsuredName = "Mr. N.A.A.R. NEHTHASINGHE",
                         StartDate = "12-JUN-18",
                         EndDate = "11-JUN-19",
                         Department = "M",
                         PolicyType = "M11",
                         PolTypeDesc = "Motor - Comprehensive",
                         VehicleNumber = "CAG 8842",
                         PolTypeImage = "car.png",
                         PolStatusImage = "tick.png",
                         ClaimStatusImage = "claim_pending.png",
                         MobileNumber = "",
                         MotorPolicy = true,
                         FlagImage = "starFrame.png",
                    },

                     new CustPolicy
                    {
                         PolicyNumber = "G/010/AMP/17/677887",
                         AgentCode="111558" ,
                         InsuredName = "H.L.DIAS",
                         StartDate = "13-JUN-18",
                         EndDate = "12-JUN-19",
                         Department = "G",
                         PolicyType = "AMP",
                         PolTypeDesc = "Annual Medical Plan",
                         VehicleNumber = "",
                         PolTypeImage = "health.png",
                         PolStatusImage = "tick.png",
                         ClaimStatusImage = "tick.png",
                         MobileNumber = "0766980982",
                         FlagImage = "starFrame.png",
                     },

                      new CustPolicy
                    {
                        PolicyNumber = "VM11150337100011123",
                        AgentCode="111558" ,
                        InsuredName = "Mr. S.M.W.S.B. KARUNAWARDENA",
                        StartDate = "25-FEB-17",
                        EndDate = "24-FEB-18",
                        Department = "M",
                        PolicyType = "M11",
                        PolTypeDesc = "Motor - Comprehensive",
                        VehicleNumber = "CAE 5077",
                        PolTypeImage = "car.png",
                        PolStatusImage = "alert_red.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        MotorPolicy = true,
                        FlagImage = "starFrame.png",
            },

                       new CustPolicy
                     {
                        PolicyNumber = "A/11/034665/010/P",
                        AgentCode="111558" ,
                        InsuredName = "Dr. T.R.C. RUBERU",
                        StartDate = "14-MAY-18",
                        EndDate = "13-MAY-19",
                        Department = "M",
                        PolicyType = "M11",
                        PolTypeDesc = "Motor - Comprehensive",
                        VehicleNumber = "KJ  7030",
                        PolTypeImage = "car.png",
                        PolStatusImage = "tick.png",
                        ClaimStatusImage = "claim_pending.png",
                        MobileNumber = "0766980982",
                        MotorPolicy = true,
                        FlagImage = "starFrame.png",
            },

                    new CustPolicy
                    {
                        PolicyNumber = "G/010/PA/344565",
                        AgentCode="111558" ,
                        InsuredName = "H.K.K.T.DUMINDA",
                        StartDate = "22-JUN-17",
                        EndDate = "22-JUN-18",
                        Department = "G",
                        PolicyType = "PA",
                        PolTypeDesc = "Personal Accident",
                        VehicleNumber = "",
                        PolTypeImage = "life.png",
                        PolStatusImage = "alert_yellow.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        FlagImage = "starFrame.png",
                    },

                        new CustPolicy
                    {
                        PolicyNumber = "G/094/AMP/17/034565",
                        AgentCode="111558" ,
                        InsuredName = "H.M.L.ANURADHA KARUNATHILAKE",
                        StartDate = "17-MAY-18",
                        EndDate = "16-MAY-19",
                        Department = "G",
                        PolicyType = "AMP",
                        PolTypeDesc = "Annual Medical Plan",
                        VehicleNumber = "",
                        PolTypeImage = "health.png",
                        PolStatusImage = "tick.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        FlagImage = "starFrame.png",
                     },

                     new CustPolicy
                    {
                        PolicyNumber = "VM11160011100045655",
                        AgentCode="111558" ,
                        InsuredName = "Mr. B.C.R.D.S DE SILVA",
                        StartDate = "08-JUN-18",
                        EndDate = "07-JUN-19",
                        Department = "M",
                        PolicyType = "M11",
                        PolTypeDesc = "Motor - Comprehensive",
                        VehicleNumber = "KY  5427",
                        PolTypeImage = "car.png",
                        PolStatusImage = "tick.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        MotorPolicy = true,
                        FlagImage = "starFrame.png",
                    },

                      new CustPolicy
                    {
                        PolicyNumber = "FFBP17010100056776",
                        AgentCode="111558" ,
                        InsuredName = "F.R.N Leitan",
                        StartDate = "10-MAY-17",
                        EndDate = "09-MAY-18",
                        Department = "F",
                        PolicyType = "FBP",
                        PolTypeDesc = "Fire Policy",
                        VehicleNumber = "",
                        PolTypeImage = "car.png",
                        PolStatusImage = "alert_red.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        FlagImage = "starFrame.png",
                     },

                     new CustPolicy
                     {
                        PolicyNumber = "GHC170101000063",
                        AgentCode="111558" ,
                        InsuredName = "W.A.D.N PERERA",
                        StartDate = "02-JUN-18",
                        EndDate = "02-JUN-19",
                        Department = "G",
                        PolicyType = "HC",
                        PolTypeDesc = "Home Protect",
                        VehicleNumber = "",
                        PolTypeImage = "home.png",
                        PolStatusImage = "tick.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        FlagImage = "starFrame.png",
                     },

                     new CustPolicy
                     {
                        PolicyNumber = "VM1115003410007865",
                        AgentCode="111558" ,
                        InsuredName = "Mr. S.L.S.GUNARATHNA",
                        StartDate = "16-JUN-18",
                        EndDate = "15-JUN-19",
                        Department = "M",
                        PolicyType = "M11",
                        PolTypeDesc = "Motor - Comprehensive",
                        VehicleNumber = "CAH 0945",
                        PolTypeImage = "car.png",
                        PolStatusImage = "tick.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        MotorPolicy = true,
                        FlagImage = "starFrame.png",
                     },
                   
                 //   AlertColor =  Color.Green : Color.Blue,    This can be added to set alert dialog inside card data model
                };

            }
        }

        public void GeneratePoliciesPending()
        {
            _policyList = new List<CustPolicy>
                {
                    new CustPolicy
                    {
                        PolicyNumber = "G/010/PA/37241",
                        AgentCode="111558" ,
                        InsuredName = "H.K.K.T.DUMINDA",
                        StartDate = "22-JUN-17",
                        EndDate = "22-JUN-18",
                        Department = "G",
                        PolicyType = "PA",
                        PolTypeDesc = "Personal Accident",
                        VehicleNumber = "",
                        PolTypeImage = "life.png",
                        PolStatusImage = "alert_yellow.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        FlagImage = "starFrame.png",
                    },

                     new CustPolicy
                     {
                        PolicyNumber = "GHC170101000031",
                        AgentCode="111558" ,
                        InsuredName = "W.A.D.N PERERA",
                        StartDate = "02-JUN-18",
                        EndDate = "02-JUN-19",
                        Department = "G",
                        PolicyType = "HC",
                        PolTypeDesc = "Home Protect",
                        VehicleNumber = "",
                        PolTypeImage = "home.png",
                        PolStatusImage = "alert_yellow.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        FlagImage = "starFrame.png",
                     },

                     new CustPolicy
                     {
                        PolicyNumber = "VM1115003410000519",
                        AgentCode="111558" ,
                        InsuredName = "Mr. S.L.S.GUNARATHNA",
                        StartDate = "16-JUN-18",
                        EndDate = "15-JUN-19",
                        Department = "M",
                        PolicyType = "M11",
                        PolTypeDesc = "Motor - Comprehensive",
                        VehicleNumber = "CAH 0945",
                        PolTypeImage = "car.png",
                        PolStatusImage = "alert_yellow.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        MotorPolicy = true,
                        FlagImage = "starFrame.png",
                     },
            };
        }

        public void GenerateClaimPending()
        {
            _policyList = new List<CustPolicy>
                {
                    new CustPolicy
                    {
                        PolicyNumber = "G/010/PA/37241",
                        AgentCode="111558" ,
                        InsuredName = "H.K.K.T.DUMINDA",
                        StartDate = "22-JUN-17",
                        EndDate = "22-JUN-18",
                        Department = "G",
                        PolicyType = "PA",
                        PolTypeDesc = "Personal Accident",
                        VehicleNumber = "",
                        PolTypeImage = "life.png",
                        PolStatusImage = "claim_pending.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        FlagImage = "starFrame.png",
                    },

                     new CustPolicy
                     {
                        PolicyNumber = "GHC170101000031",
                        AgentCode="111558" ,
                        InsuredName = "W.A.D.N PERERA",
                        StartDate = "02-JUN-18",
                        EndDate = "02-JUN-19",
                        Department = "G",
                        PolicyType = "HC",
                        PolTypeDesc = "Home Protect",
                        VehicleNumber = "",
                        PolTypeImage = "home.png",
                        PolStatusImage = "claim_pending.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        FlagImage = "starFrame.png",
                     },

                     new CustPolicy
                     {
                        PolicyNumber = "VM1115003410000519",
                        AgentCode="111558" ,
                        InsuredName = "Mr. S.L.S.GUNARATHNA",
                        StartDate = "16-JUN-18",
                        EndDate = "15-JUN-19",
                        Department = "M",
                        PolicyType = "M11",
                        PolTypeDesc = "Motor - Comprehensive",
                        VehicleNumber = "CAH 0945",
                        PolTypeImage = "car.png",
                        PolStatusImage = "claim_pending.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        MotorPolicy = true,
                        FlagImage = "starFrame.png",
                     },
            };
        }

        public void GenerateFlagged()
        {
            _policyList = new List<CustPolicy>
                {
                    new CustPolicy
                    {
                        PolicyNumber = "G/010/PA/37241",
                        AgentCode="111558" ,
                        InsuredName = "H.K.K.T.DUMINDA",
                        StartDate = "22-JUN-17",
                        EndDate = "22-JUN-18",
                        Department = "G",
                        PolicyType = "PA",
                        PolTypeDesc = "Personal Accident",
                        VehicleNumber = "",
                        PolTypeImage = "life.png",
                        PolStatusImage = "tick.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        FlagImage = "filledStar.png",
                        Flagged = true,
                    },

                     new CustPolicy
                     {
                        PolicyNumber = "GHC170101000031",
                        AgentCode="111558" ,
                        InsuredName = "W.A.D.N PERERA",
                        StartDate = "02-JUN-18",
                        EndDate = "02-JUN-19",
                        Department = "G",
                        PolicyType = "HC",
                        PolTypeDesc = "Home Protect",
                        VehicleNumber = "",
                        PolTypeImage = "home.png",
                        PolStatusImage = "tick.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        FlagImage = "filledStar.png",
                        Flagged = true,
                     },

                     new CustPolicy
                     {
                        PolicyNumber = "VM1115003410000519",
                        AgentCode="111558" ,
                        InsuredName = "Mr. S.L.S.GUNARATHNA",
                        StartDate = "16-JUN-18",
                        EndDate = "15-JUN-19",
                        Department = "M",
                        PolicyType = "M11",
                        PolTypeDesc = "Motor - Comprehensive",
                        VehicleNumber = "CAH 0945",
                        PolTypeImage = "car.png",
                        PolStatusImage = "tick.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        MotorPolicy = true,
                        FlagImage = "filledStar.png",
                        Flagged = true,
                     },
            };
        }

        public ObservableCollection<CustPolicy> GenerateFlaggedForToday()
        {
            var polList = new ObservableCollection<CustPolicy>
                {
                    new CustPolicy
                    {
                        PolicyNumber = "G/010/PA/37241",
                        AgentCode="111558" ,
                        InsuredName = "H.K.K.T.DUMINDA",
                        StartDate = "22-JUN-17",
                        EndDate = "22-JUN-18",
                        Department = "G",
                        PolicyType = "PA",
                        PolTypeDesc = "Personal Accident",
                        VehicleNumber = "",
                        PolTypeImage = "life.png",
                        PolStatusImage = "tick.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        FlagImage = "filledStar.png",
                        Flagged = true,
                        AgentComment = "Have to call the customer and find out the issue in claim. need to know if all documents submitted and requirements completed.",
                    },

                     new CustPolicy
                     {
                        PolicyNumber = "GHC170101000031",
                        AgentCode="111558" ,
                        InsuredName = "W.A.D.N PERERA",
                        StartDate = "02-JUN-18",
                        EndDate = "02-JUN-19",
                        Department = "G",
                        PolicyType = "HC",
                        PolTypeDesc = "Home Protect",
                        VehicleNumber = "",
                        PolTypeImage = "home.png",
                        PolStatusImage = "tick.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        FlagImage = "filledStar.png",
                        Flagged = true,
                        AgentComment = "test comment, have to look into the claim pending",
                     },

                     new CustPolicy
                     {
                        PolicyNumber = "VM1115003410000519",
                        AgentCode="111558" ,
                        InsuredName = "Mr. S.L.S.GUNARATHNA",
                        StartDate = "16-JUN-18",
                        EndDate = "15-JUN-19",
                        Department = "M",
                        PolicyType = "M11",
                        PolTypeDesc = "Motor - Comprehensive",
                        VehicleNumber = "CAH 0945",
                        PolTypeImage = "car.png",
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

    }
}
