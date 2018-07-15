using Agent_App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms.Extended;
using Agent_App.Helpers;

namespace Agent_App.ViewModels
{
    public class AgentPerformance: INotifyPropertyChanged
    {
        public AgtPerfmStat agentStat;
        public AgtPerfmStat _previousAgentRec;
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


        public AgentPerformance()
        {
            load();


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

        private void load()
        {
            _previousAgentRec = null;  // this should be done whenever policy collection regenerated.

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

                     };


        }
    }
}
