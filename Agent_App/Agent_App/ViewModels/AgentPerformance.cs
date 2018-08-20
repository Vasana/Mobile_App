using Agent_App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms.Extended;
using Agent_App.Helpers;
using System.Threading.Tasks;
using Agent_App.Services;

namespace Agent_App.ViewModels
{
    public class AgentPerformance: INotifyPropertyChanged
    {
        ApiServices _apiServices = new ApiServices();
        private const int PageSize = 15;
        public AgtPerfmStat agentStat;
        public AgtPerfmStat _previousAgentRec;

        public AgtPerfmStat _previousAgt;

        public List<AgtPerfmStat> teamList;
        public List<Agent_App.Models.AgentPerformance> AgentPerfList;

        public InfiniteScrollCollection<AgtPerfmStat> _agentDetails;
        public InfiniteScrollCollection<AgtPerfmStat> agentsTeamRecs
        {
            get { return _agentDetails; }
            set
            {
                _agentDetails = value;
                OnPropertyChanged();
            }
        }


        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        private bool _isBusy;

        public bool IsBusy2
        {
            get { return _isBusy2; }
            set
            {
                _isBusy2 = value;
                OnPropertyChanged();
            }
        }
        public bool _isBusy2;


        public event PropertyChangedEventHandler PropertyChanged;


        public AgentPerformance()
        {
            load();
            //DownloadNotifsAsync();


        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public void HideOrShowPolicy(AgtPerfmStat agntRec)
        {
            if (_previousAgentRec == agntRec)
            {
                //clicking twice on same item will hide it
                agntRec.IsSelected = !agntRec.IsSelected;
                UpdatePolicies(agntRec);

            }
            else
            {
                if (_previousAgentRec != null)
                {
                    //hide previous selected item
                    _previousAgentRec.IsSelected = false;
                    UpdatePolicies(_previousAgentRec);
                }
                //show selected item
                agntRec.IsSelected = true;
                UpdatePolicies(agntRec);
            }

            _previousAgentRec = agntRec;
        }
        private void UpdatePolicies(AgtPerfmStat agentRec)
        {
            var index = agentsTeamRecs.IndexOf(agentRec);
            agentsTeamRecs.Remove(agentRec);
            agentsTeamRecs.Insert(index, agentRec);
        }

        
        //public async Task DownloadNotifsAsync()
        //{
        //    agentsTeamRecs = new InfiniteScrollCollection<AgentPerformance>
        //    {
        //        OnLoadMore = async () =>
        //        {
        //            IsBusy = true;

        //            // load the next page
        //            var page = agentsTeamRecs.Count / PageSize;

        //            var items = await _apiServices.GetTeamPerformanceAsync(Settings.AccessToken, "", "", "");

        //            IsBusy = false;

        //            // return the items that need to be added
        //            return items;
        //        },
        //        OnCanLoadMore = () =>
        //        {
        //            return agentsTeamRecs.Count < _apiServices.notifCount;
        //        }
        //    };
        //    _previousAgt = null;
        //    IsBusy2 = true;
        //    var items2 = await _apiServices.GetNotificationsAsync(accessToken: Settings.AccessToken, pageIndex: 0, pageSize: PageSize);
        //    IsBusy2 = false;
        //    agentsTeamRecs.AddRange(items2);
        //    //PoliciesCollection = new InfiniteScrollCollection<CustPolicy>(items);
        //}
        

        private void load()
        {
            teamList = new List<AgtPerfmStat>();
            _previousAgentRec = null;  // this should be done whenever policy collection regenerated.
            AgentPerfList =  _apiServices.GetTeamPerformance(Settings.AccessToken,  Settings.orgTeamCode, DateTime.Today.ToString("yyyy-MM-01"), DateTime.Today.ToString("yyyy-MM-dd"));


            foreach (Agent_App.Models.AgentPerformance item in AgentPerfList)
            {
                try
                {
                    AgtPerfmStat agt = new AgtPerfmStat();
                    agt.agentID = item.AGENT_CODE;
                    agt.agentName = item.AGENT_NAME;
                    agt.indMonthNoPolTotal = item.TOT_CASH_MOTOR + item.TOT_DEBIT_MOTOR + item.TOT_CASH_NON_MOTOR + item.TOT_DEBIT_NON_MOTOR - (item.TOT_MOTOR_REFUND + item.TOT_NON_MOTOR_REFUND);
                    agt.indMonthNoPolTotal_cash = item.TOT_CASH_MOTOR + item.TOT_CASH_NON_MOTOR;
                    agt.indMonthNoPolTotal_Dbt = item.TOT_DEBIT_MOTOR + item.TOT_DEBIT_NON_MOTOR;



                    agt.indMonthPremTotal = item.TOT_CASH_MOTOR_PRM + item.TOT_DEBIT_MOTOR_PRM + item.TOT_CASH_NON_MOTOR_PRM + item.TOT_DEBIT_NON_MOTOR_PRM - (item.TOT_MOTOR_REFUND_PRM+item.TOT_NON_MOTOR_REFUND_PRM);
                    agt.indMonthPremTotal_cash = item.TOT_CASH_MOTOR_PRM + item.TOT_CASH_NON_MOTOR_PRM;
                    agt.indMonthPremTotal_Dbt = item.TOT_DEBIT_MOTOR_PRM + item.TOT_DEBIT_NON_MOTOR_PRM;



                    agt.indMonthNoPol_New = item.CASH_NEW_MOTOR + item.DEBIT_NEW_MOTOR + item.CASH_NEW_NON_MOTOR + item.DEBIT_NEW_NON_MOTOR;
                    agt.indMonthNoPol_New_Cash = item.CASH_NEW_MOTOR + item.CASH_NEW_NON_MOTOR;
                    agt.indMonthNoPol_New_Dbt = item.DEBIT_NEW_MOTOR + item.DEBIT_NEW_NON_MOTOR;



                    agt.indMonthPrem_New = item.CASH_NEW_MOTOR_PRM + item.DEBIT_NEW_MOTOR_PRM + item.CASH_NEW_NON_MOTOR_PRM + item.DEBIT_NEW_NON_MOTOR_PRM;
                    agt.indMonthPrem_New_Cash = item.CASH_NEW_MOTOR_PRM + item.CASH_NEW_NON_MOTOR_PRM;
                    agt.indMonthPrem_New_Dbt = item.DEBIT_NEW_MOTOR_PRM + item.DEBIT_NEW_NON_MOTOR_PRM;



                    agt.indMonthNoPol_Renewal = item.CASH_REN_MOTOR + item.DEBIT_REN_MOTOR + item.CASH_REN_NON_MOTOR + item.DEBIT_REN_NON_MOTOR;
                    agt.indMonthNoPol_Renewal_cash = item.CASH_REN_MOTOR + item.CASH_REN_NON_MOTOR;
                    agt.indMonthNoPol_Renewal_Dbt = item.DEBIT_REN_MOTOR + item.DEBIT_REN_NON_MOTOR;



                    agt.indMonthPrem_Renewal = item.CASH_REN_MOTOR_PRM + item.DEBIT_REN_MOTOR_PRM + item.CASH_REN_NON_MOTOR_PRM + item.DEBIT_REN_NON_MOTOR_PRM;
                    agt.indMonthPrem_Renewal_cash = item.CASH_REN_MOTOR_PRM + item.CASH_REN_NON_MOTOR_PRM;
                    agt.indMonthPrem_Renewal_Dbt = item.DEBIT_REN_MOTOR_PRM + item.DEBIT_REN_NON_MOTOR_PRM;

                    teamList.Add(agt);
                }
                catch (Exception q)
                {

                }
                
            }

            agentsTeamRecs = new InfiniteScrollCollection<AgtPerfmStat>();
            foreach (AgtPerfmStat item in teamList)
            {
                agentsTeamRecs.Add(item);
            }

            /*
            agentsTeamRecs = new InfiniteScrollCollection<AgtPerfmStat>
                {
                    new AgtPerfmStat
                    {
                         agentID  = 905717,
                    year = 2018,
                    month = "July",

                    indMonthNoPolTotal  = 12,
                    indMonthNoPolTotal_cash = 9,
                    indMonthNoPolTotal_Dbt = 3,

                    indYearPolTotal  = 78,
                    indYearPolTotal_cash = 58,
                    indYearPolTotal_Dbt = 20,

                    indMonthPremTotal  = 12000,
                    indMonthPremTotal_cash = 8000,
                    indMonthPremTotal_Dbt = 4000,

                    indYearPremTotal  = 3408000,
                    indYearPremTotal_cash = 2408000,
                    indYearPremTotal_Dbt = 1000000,

                    indMonthNoPol_New = 7,
                    indMonthNoPol_New_Cash = 2,
                    indMonthNoPol_New_Dbt = 5,

                    indYearPol_New = 58,
                    indYearPol_New_Cash = 38,
                    indYearPol_New_Dbt = 20,

                    indMonthPrem_New = 6000,
                    indMonthPrem_New_Cash = 3000,
                    indMonthPrem_New_Dbt = 3000,

                    indYearPrem_New = 1408000,
                    indYearPrem_New_Cash = 1000000,
                    indYearPrem_New_Dbt = 408000,

                    indMonthNoPol_Renewal = 5,
                    indMonthNoPol_Renewal_cash = 2,
                    indMonthNoPol_Renewal_Dbt = 3,

                    indYearPol_Renewal = 20,
                    indYearPol_Renewal_cash = 10,
                    indYearPol_Renewal_Dbt = 10,

                    indMonthPrem_Renewal = 6000,
                    indMonthPrem_Renewal_cash = 4000,
                    indMonthPrem_Renewal_Dbt = 2000,

                indYearPrem_Renewal = 2000000,
                indYearPrem_Renewal_cash = 1200000,
                indYearPrem_Renewal_Dbt = 800000,

                branchMonthNoPolTotal  = 67,
                    branchYearPolTotal  = 560,

                    branchMonthPremTotal  = 4500000,
                    branchYearPremTotal  = 6788888,


                    },


                     new AgtPerfmStat
                     {
                         agentID  = 905714,
                    year = 2018,
                    month = "July",

                    indMonthNoPolTotal  = 12,
                    indMonthNoPolTotal_cash = 9,
                    indMonthNoPolTotal_Dbt = 3,

                    indYearPolTotal  = 78,
                    indYearPolTotal_cash = 58,
                    indYearPolTotal_Dbt = 20,

                    indMonthPremTotal  = 12000,
                    indMonthPremTotal_cash = 8000,
                    indMonthPremTotal_Dbt = 4000,

                    indYearPremTotal  = 3408000,
                    indYearPremTotal_cash = 2408000,
                    indYearPremTotal_Dbt = 1000000,

                    indMonthNoPol_New = 7,
                    indMonthNoPol_New_Cash = 2,
                    indMonthNoPol_New_Dbt = 5,

                    indYearPol_New = 58,
                    indYearPol_New_Cash = 38,
                    indYearPol_New_Dbt = 20,

                    indMonthPrem_New = 6000,
                    indMonthPrem_New_Cash = 3000,
                    indMonthPrem_New_Dbt = 3000,

                    indYearPrem_New = 1408000,
                    indYearPrem_New_Cash = 1000000,
                    indYearPrem_New_Dbt = 408000,

                    indMonthNoPol_Renewal = 5,
                    indMonthNoPol_Renewal_cash = 2,
                    indMonthNoPol_Renewal_Dbt = 3,

                    indYearPol_Renewal = 20,
                    indYearPol_Renewal_cash = 10,
                    indYearPol_Renewal_Dbt = 10,

                    indMonthPrem_Renewal = 6000,
                    indMonthPrem_Renewal_cash = 4000,
                    indMonthPrem_Renewal_Dbt = 2000,

                indYearPrem_Renewal = 2000000,
                indYearPrem_Renewal_cash = 1200000,
                indYearPrem_Renewal_Dbt = 800000,

                branchMonthNoPolTotal  = 67,
                    branchYearPolTotal  = 560,

                    branchMonthPremTotal  = 4500000,
                    branchYearPremTotal  = 6788888,

                     },
new AgtPerfmStat
                     {
                         agentID  = 905714,
                    year = 2018,
                    month = "July",

                    indMonthNoPolTotal  = 12,
                    indMonthNoPolTotal_cash = 9,
                    indMonthNoPolTotal_Dbt = 3,

                    indYearPolTotal  = 78,
                    indYearPolTotal_cash = 58,
                    indYearPolTotal_Dbt = 20,

                    indMonthPremTotal  = 12000,
                    indMonthPremTotal_cash = 8000,
                    indMonthPremTotal_Dbt = 4000,

                    indYearPremTotal  = 3408000,
                    indYearPremTotal_cash = 2408000,
                    indYearPremTotal_Dbt = 1000000,

                    indMonthNoPol_New = 7,
                    indMonthNoPol_New_Cash = 2,
                    indMonthNoPol_New_Dbt = 5,

                    indYearPol_New = 58,
                    indYearPol_New_Cash = 38,
                    indYearPol_New_Dbt = 20,

                    indMonthPrem_New = 6000,
                    indMonthPrem_New_Cash = 3000,
                    indMonthPrem_New_Dbt = 3000,

                    indYearPrem_New = 1408000,
                    indYearPrem_New_Cash = 1000000,
                    indYearPrem_New_Dbt = 408000,

                    indMonthNoPol_Renewal = 5,
                    indMonthNoPol_Renewal_cash = 2,
                    indMonthNoPol_Renewal_Dbt = 3,

                    indYearPol_Renewal = 20,
                    indYearPol_Renewal_cash = 10,
                    indYearPol_Renewal_Dbt = 10,

                    indMonthPrem_Renewal = 6000,
                    indMonthPrem_Renewal_cash = 4000,
                    indMonthPrem_Renewal_Dbt = 2000,

                indYearPrem_Renewal = 2000000,
                indYearPrem_Renewal_cash = 1200000,
                indYearPrem_Renewal_Dbt = 800000,

                branchMonthNoPolTotal  = 67,
                    branchYearPolTotal  = 560,

                    branchMonthPremTotal  = 4500000,
                    branchYearPremTotal  = 6788888,

                     }
,new AgtPerfmStat
                     {
                         agentID  = 905714,
                    year = 2018,
                    month = "July",

                    indMonthNoPolTotal  = 12,
                    indMonthNoPolTotal_cash = 9,
                    indMonthNoPolTotal_Dbt = 3,

                    indYearPolTotal  = 78,
                    indYearPolTotal_cash = 58,
                    indYearPolTotal_Dbt = 20,

                    indMonthPremTotal  = 12000,
                    indMonthPremTotal_cash = 8000,
                    indMonthPremTotal_Dbt = 4000,

                    indYearPremTotal  = 3408000,
                    indYearPremTotal_cash = 2408000,
                    indYearPremTotal_Dbt = 1000000,

                    indMonthNoPol_New = 7,
                    indMonthNoPol_New_Cash = 2,
                    indMonthNoPol_New_Dbt = 5,

                    indYearPol_New = 58,
                    indYearPol_New_Cash = 38,
                    indYearPol_New_Dbt = 20,

                    indMonthPrem_New = 6000,
                    indMonthPrem_New_Cash = 3000,
                    indMonthPrem_New_Dbt = 3000,

                    indYearPrem_New = 1408000,
                    indYearPrem_New_Cash = 1000000,
                    indYearPrem_New_Dbt = 408000,

                    indMonthNoPol_Renewal = 5,
                    indMonthNoPol_Renewal_cash = 2,
                    indMonthNoPol_Renewal_Dbt = 3,

                    indYearPol_Renewal = 20,
                    indYearPol_Renewal_cash = 10,
                    indYearPol_Renewal_Dbt = 10,

                    indMonthPrem_Renewal = 6000,
                    indMonthPrem_Renewal_cash = 4000,
                    indMonthPrem_Renewal_Dbt = 2000,

                indYearPrem_Renewal = 2000000,
                indYearPrem_Renewal_cash = 1200000,
                indYearPrem_Renewal_Dbt = 800000,

                branchMonthNoPolTotal  = 67,
                    branchYearPolTotal  = 560,

                    branchMonthPremTotal  = 4500000,
                    branchYearPremTotal  = 6788888,

                     }
,new AgtPerfmStat
                     {
                         agentID  = 905714,
                    year = 2018,
                    month = "July",

                    indMonthNoPolTotal  = 12,
                    indMonthNoPolTotal_cash = 9,
                    indMonthNoPolTotal_Dbt = 3,

                    indYearPolTotal  = 78,
                    indYearPolTotal_cash = 58,
                    indYearPolTotal_Dbt = 20,

                    indMonthPremTotal  = 12000,
                    indMonthPremTotal_cash = 8000,
                    indMonthPremTotal_Dbt = 4000,

                    indYearPremTotal  = 3408000,
                    indYearPremTotal_cash = 2408000,
                    indYearPremTotal_Dbt = 1000000,

                    indMonthNoPol_New = 7,
                    indMonthNoPol_New_Cash = 2,
                    indMonthNoPol_New_Dbt = 5,

                    indYearPol_New = 58,
                    indYearPol_New_Cash = 38,
                    indYearPol_New_Dbt = 20,

                    indMonthPrem_New = 6000,
                    indMonthPrem_New_Cash = 3000,
                    indMonthPrem_New_Dbt = 3000,

                    indYearPrem_New = 1408000,
                    indYearPrem_New_Cash = 1000000,
                    indYearPrem_New_Dbt = 408000,

                    indMonthNoPol_Renewal = 5,
                    indMonthNoPol_Renewal_cash = 2,
                    indMonthNoPol_Renewal_Dbt = 3,

                    indYearPol_Renewal = 20,
                    indYearPol_Renewal_cash = 10,
                    indYearPol_Renewal_Dbt = 10,

                    indMonthPrem_Renewal = 6000,
                    indMonthPrem_Renewal_cash = 4000,
                    indMonthPrem_Renewal_Dbt = 2000,

                indYearPrem_Renewal = 2000000,
                indYearPrem_Renewal_cash = 1200000,
                indYearPrem_Renewal_Dbt = 800000,

                branchMonthNoPolTotal  = 67,
                    branchYearPolTotal  = 560,

                    branchMonthPremTotal  = 4500000,
                    branchYearPremTotal  = 6788888,

                     }
,new AgtPerfmStat
                     {
                         agentID  = 905714,
                    year = 2018,
                    month = "July",

                    indMonthNoPolTotal  = 12,
                    indMonthNoPolTotal_cash = 9,
                    indMonthNoPolTotal_Dbt = 3,

                    indYearPolTotal  = 78,
                    indYearPolTotal_cash = 58,
                    indYearPolTotal_Dbt = 20,

                    indMonthPremTotal  = 12000,
                    indMonthPremTotal_cash = 8000,
                    indMonthPremTotal_Dbt = 4000,

                    indYearPremTotal  = 3408000,
                    indYearPremTotal_cash = 2408000,
                    indYearPremTotal_Dbt = 1000000,

                    indMonthNoPol_New = 7,
                    indMonthNoPol_New_Cash = 2,
                    indMonthNoPol_New_Dbt = 5,

                    indYearPol_New = 58,
                    indYearPol_New_Cash = 38,
                    indYearPol_New_Dbt = 20,

                    indMonthPrem_New = 6000,
                    indMonthPrem_New_Cash = 3000,
                    indMonthPrem_New_Dbt = 3000,

                    indYearPrem_New = 1408000,
                    indYearPrem_New_Cash = 1000000,
                    indYearPrem_New_Dbt = 408000,

                    indMonthNoPol_Renewal = 5,
                    indMonthNoPol_Renewal_cash = 2,
                    indMonthNoPol_Renewal_Dbt = 3,

                    indYearPol_Renewal = 20,
                    indYearPol_Renewal_cash = 10,
                    indYearPol_Renewal_Dbt = 10,

                    indMonthPrem_Renewal = 6000,
                    indMonthPrem_Renewal_cash = 4000,
                    indMonthPrem_Renewal_Dbt = 2000,

                indYearPrem_Renewal = 2000000,
                indYearPrem_Renewal_cash = 1200000,
                indYearPrem_Renewal_Dbt = 800000,

                branchMonthNoPolTotal  = 67,
                    branchYearPolTotal  = 560,

                    branchMonthPremTotal  = 4500000,
                    branchYearPremTotal  = 6788888,

                     }
,new AgtPerfmStat
                     {
                         agentID  = 905714,
                    year = 2018,
                    month = "July",

                    indMonthNoPolTotal  = 12,
                    indMonthNoPolTotal_cash = 9,
                    indMonthNoPolTotal_Dbt = 3,

                    indYearPolTotal  = 78,
                    indYearPolTotal_cash = 58,
                    indYearPolTotal_Dbt = 20,

                    indMonthPremTotal  = 12000,
                    indMonthPremTotal_cash = 8000,
                    indMonthPremTotal_Dbt = 4000,

                    indYearPremTotal  = 3408000,
                    indYearPremTotal_cash = 2408000,
                    indYearPremTotal_Dbt = 1000000,

                    indMonthNoPol_New = 7,
                    indMonthNoPol_New_Cash = 2,
                    indMonthNoPol_New_Dbt = 5,

                    indYearPol_New = 58,
                    indYearPol_New_Cash = 38,
                    indYearPol_New_Dbt = 20,

                    indMonthPrem_New = 6000,
                    indMonthPrem_New_Cash = 3000,
                    indMonthPrem_New_Dbt = 3000,

                    indYearPrem_New = 1408000,
                    indYearPrem_New_Cash = 1000000,
                    indYearPrem_New_Dbt = 408000,

                    indMonthNoPol_Renewal = 5,
                    indMonthNoPol_Renewal_cash = 2,
                    indMonthNoPol_Renewal_Dbt = 3,

                    indYearPol_Renewal = 20,
                    indYearPol_Renewal_cash = 10,
                    indYearPol_Renewal_Dbt = 10,

                    indMonthPrem_Renewal = 6000,
                    indMonthPrem_Renewal_cash = 4000,
                    indMonthPrem_Renewal_Dbt = 2000,

                indYearPrem_Renewal = 2000000,
                indYearPrem_Renewal_cash = 1200000,
                indYearPrem_Renewal_Dbt = 800000,

                branchMonthNoPolTotal  = 67,
                    branchYearPolTotal  = 560,

                    branchMonthPremTotal  = 4500000,
                    branchYearPremTotal  = 6788888,

                     }
,new AgtPerfmStat
                     {
                         agentID  = 905714,
                    year = 2018,
                    month = "July",

                    indMonthNoPolTotal  = 12,
                    indMonthNoPolTotal_cash = 9,
                    indMonthNoPolTotal_Dbt = 3,

                    indYearPolTotal  = 78,
                    indYearPolTotal_cash = 58,
                    indYearPolTotal_Dbt = 20,

                    indMonthPremTotal  = 12000,
                    indMonthPremTotal_cash = 8000,
                    indMonthPremTotal_Dbt = 4000,

                    indYearPremTotal  = 3408000,
                    indYearPremTotal_cash = 2408000,
                    indYearPremTotal_Dbt = 1000000,

                    indMonthNoPol_New = 7,
                    indMonthNoPol_New_Cash = 2,
                    indMonthNoPol_New_Dbt = 5,

                    indYearPol_New = 58,
                    indYearPol_New_Cash = 38,
                    indYearPol_New_Dbt = 20,

                    indMonthPrem_New = 6000,
                    indMonthPrem_New_Cash = 3000,
                    indMonthPrem_New_Dbt = 3000,

                    indYearPrem_New = 1408000,
                    indYearPrem_New_Cash = 1000000,
                    indYearPrem_New_Dbt = 408000,

                    indMonthNoPol_Renewal = 5,
                    indMonthNoPol_Renewal_cash = 2,
                    indMonthNoPol_Renewal_Dbt = 3,

                    indYearPol_Renewal = 20,
                    indYearPol_Renewal_cash = 10,
                    indYearPol_Renewal_Dbt = 10,

                    indMonthPrem_Renewal = 6000,
                    indMonthPrem_Renewal_cash = 4000,
                    indMonthPrem_Renewal_Dbt = 2000,

                indYearPrem_Renewal = 2000000,
                indYearPrem_Renewal_cash = 1200000,
                indYearPrem_Renewal_Dbt = 800000,

                branchMonthNoPolTotal  = 67,
                    branchYearPolTotal  = 560,

                    branchMonthPremTotal  = 4500000,
                    branchYearPremTotal  = 6788888,

                     }

                     };*/


        }
    }
}
