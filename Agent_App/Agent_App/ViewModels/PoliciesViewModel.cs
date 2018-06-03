using Agent_App.Models;
using Agent_App.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;

namespace Agent_App.ViewModels
{
    public class PoliciesViewModel : INotifyPropertyChanged
    {
        ApiServices _apiServices = new ApiServices();
        public Cust_Policy _previousPolicy;
        public ObservableCollection<Cust_Policy> PoliciesCollection
        {
            get { return _policies; }
            set
            {
                _policies = value;
                OnPropertyChanged();
            }
        }

        //public ICommand GetIdeasCommand
        //{
        //    get
        //    {
        //        return new Command(async () =>
        //        {
        //            var accessToken = Settings.AccessToken;
        //            Ideas = await _apiServices.GetIdeasAsync(accessToken);
        //        });
        //    }
        //}

        public ObservableCollection<Cust_Policy> _policies;

        public object SelectedItem { get; set; }
                
        public PoliciesViewModel()
        {
            GeneratePolicies();
        }

        private void GeneratePolicies()
        {
            // for (var i = 0; i < 10; i++)
            {
                PoliciesCollection = new ObservableCollection<Cust_Policy>
                {
                    new Cust_Policy
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
                    },

                     new Cust_Policy
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
                     },

					  new Cust_Policy
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
            },

                       new Cust_Policy
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
            },

                    new Cust_Policy
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
                    },

                        new Cust_Policy
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
                     },

                     new Cust_Policy
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
                    },

                      new Cust_Policy
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
                     },

                     new Cust_Policy
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
                     },

                     new Cust_Policy
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
                     },
                   
                 //   AlertColor =  Color.Green : Color.Blue,    This can be added to set alert dialog inside card data model
                };

            }
        }

        public void HideOrShowPolicy(Cust_Policy policy)
        {

            if (_previousPolicy == policy)
            {
                //clicking twice on same item will hide it
                policy.IsSelected = !policy.IsSelected;
                UpdatePolicies(policy);
            }
            else
            {
                if (_previousPolicy != null)
                {
                    //hide previous selected item
                    _previousPolicy.IsSelected = false;
                    UpdatePolicies(_previousPolicy);
                }
                //show selected item
                policy.IsSelected = true;
                UpdatePolicies(policy);
            }

            _previousPolicy = policy;
        }

        private void UpdatePolicies(Cust_Policy policy)
        {
            var index = PoliciesCollection.IndexOf(policy);
            PoliciesCollection.Remove(policy);
            PoliciesCollection.Insert(index, policy);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SearchPolicies()
        {
            Cust_Policy policy = PoliciesCollection.First(p => p.PolicyNumber == "xy");

            if (SearchCriteria.Instance.PremiumsPending)
            {
                PoliciesCollection = new ObservableCollection<Cust_Policy>
                {

                    new Cust_Policy
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
                    },

                     new Cust_Policy
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
                     },

                     new Cust_Policy
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
                     },

                    //   AlertColor =  Color.Green : Color.Blue,    This can be added to set alert dialog inside card data model
                };
            }
            else
            {
                GeneratePolicies();
            }

        }
    }
}
