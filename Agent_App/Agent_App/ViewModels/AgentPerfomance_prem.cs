using Agent_App.Helpers;
using Agent_App.Models;
using Agent_App.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms.Extended;

namespace Agent_App.ViewModels
{
    public class AgentPerfomance_prem : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

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

        public AgentPerfomance_prem()
        {
            load();
        }


        private void UpdatePolicies(AgtPerfmStat agentRec)
        {
            var index = agentsTeamRecs.IndexOf(agentRec);
            agentsTeamRecs.Remove(agentRec);
            agentsTeamRecs.Insert(index, agentRec);
        }
        private void load()
        {
            _previousAgentRec = null;  // this should be done whenever policy collection regenerated.

            teamList = new List<AgtPerfmStat>();
            _previousAgentRec = null;  // this should be done whenever policy collection regenerated.
            AgentPerfList = _apiServices.GetTeamPerformance(Settings.AccessToken, Settings.orgTeamCode, DateTime.Today.ToString("yyyy-01-01"), DateTime.Today.ToString("yyyy-MM-dd"));


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



                    agt.indMonthPremTotal = item.TOT_CASH_MOTOR_PRM + item.TOT_DEBIT_MOTOR_PRM + item.TOT_CASH_NON_MOTOR_PRM + item.TOT_DEBIT_NON_MOTOR_PRM - ((item.TOT_MOTOR_REFUND_PRM + item.TOT_NON_MOTOR_REFUND_PRM));
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
        }
    }
}
