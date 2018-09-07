using Agent_App.Helpers;
using Agent_App.Models;
using Agent_App.Services;
using Agent_App.Views;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace Agent_App.ViewModels
{
    public class Performance_stats_org : INotifyPropertyChanged
    {
        //public ObservableCollection<AgtPerfmStat> CardDataCollection { get; set; }
        //public AgtPerfmStat ownAgt { get; set; }
        //public List<AgtPerfmStat> ownAgtList { get; set; }
        public List<Month> MonthList { get; set; }
        public List<Year> YearList { get; set; }

        private List<Models.AgentPerformance> month_performance;
        private List<Models.AgentPerformance> year_performance;


        private Models.AgentPerformance month_performance_comb;
        private Models.AgentPerformance year_performance_comb;

        private ApiServices _apiServices = new ApiServices();

        public string inq_month { get; set; }
        public int inq_year { get; set; }

        public Month _getMoth { get; set; }
        public Year _getYear { get; set; }

        private AgtPerfmStat _ownAgt;

        public AgtPerfmStat ownAgt
        {
            get => _ownAgt;
            set
            {
                _ownAgt = value;
                OnPropertyChanged();
            }
        }

        private List<AgtPerfmStat> _ownAgtList;

        public List<AgtPerfmStat> ownAgtList
        {
            get => _ownAgtList;
            set
            {
                _ownAgtList = value;
                OnPropertyChanged();
            }
        }


        private PlotModel _pieMonthNoPol;

        public PlotModel PieMonthNoPol
        {
            get => _pieMonthNoPol;
            set
            {
                _pieMonthNoPol = value;
                OnPropertyChanged();
            }
        }

        private PlotModel _pieYearNoPol;

        public PlotModel PieYearNoPol
        {
            get => _pieYearNoPol;
            set
            {
                _pieYearNoPol = value;
                OnPropertyChanged();
            }
        }

        private PlotModel _pieYearPrem;

        public PlotModel PieYearPrem
        {
            get => _pieYearPrem;
            set
            {
                _pieYearPrem = value;
                OnPropertyChanged();
            }
        }

        private PlotModel _pieMonthPrem;

        public PlotModel PieMonthPrem
        {
            get => _pieMonthPrem;
            set
            {
                _pieMonthPrem = value;
                OnPropertyChanged();
            }
        }

        private string _lbl_mon_NOP;

        public string lbl_mon_NOP
        {
            get => _lbl_mon_NOP;
            set
            {
                _lbl_mon_NOP = value;
                OnPropertyChanged();
            }
        }

        private string _lbl_yr_NOP;

        public string lbl_yr_NOP
        {
            get => _lbl_yr_NOP;
            set
            {
                _lbl_yr_NOP = value;
                OnPropertyChanged();
            }
        }

        private string _lbl_mon_prem;

        public string lbl_mon_prem
        {
            get => _lbl_mon_prem;
            set
            {
                _lbl_mon_prem = value;
                OnPropertyChanged();
            }
        }

        private string _lbl_yr_prem;

        public string lbl_yr_prem
        {
            get => _lbl_yr_prem;
            set
            {
                _lbl_yr_prem = value;
                OnPropertyChanged();
            }
        }



        public Performance_stats_org()
        {
            month_performance_comb = new Models.AgentPerformance();
            year_performance_comb = new Models.AgentPerformance();

            fetchData();
            if (month_performance.Count > 0 && year_performance.Count > 0)
            {
                GetAgentStats(month_performance_comb, year_performance_comb, DateTime.Today.ToString("MM"), Convert.ToInt32(DateTime.Today.ToString("yyyy")));
                Populate_controls(month_performance_comb, year_performance_comb, DateTime.Today.ToString("MM"), DateTime.Today.ToString("yyyy"));
            }
            MonthList = getMonths();
            YearList = getYears();
        }

        private void fetchData()
        {
            try
            {
                //month_performance = _apiServices.GetTeamPerformance(Settings.AccessToken, Settings.orgTeamCode, DateTime.Today.ToString("yyyy-MM-01"), DateTime.Today.ToString("yyyy-MM-dd"));
                month_performance = _apiServices.GetTeamPerformance(Settings.AccessToken, Settings.orgTeamCode, DateTime.Today.AddYears(-1).ToString("yyyy-MM-01"), DateTime.Today.AddYears(-1).ToString("yyyy-MM-dd"));
                month_performance_comb.CASH_NEW_MOTOR = month_performance.Sum(x => x.CASH_NEW_MOTOR);
                month_performance_comb.CASH_NEW_MOTOR_PRM = month_performance.Sum(x => x.CASH_NEW_MOTOR_PRM);
                month_performance_comb.CASH_NEW_NON_MOTOR = month_performance.Sum(x => x.CASH_NEW_NON_MOTOR);
                month_performance_comb.CASH_NEW_NON_MOTOR_PRM = month_performance.Sum(x => x.CASH_NEW_NON_MOTOR_PRM);
                month_performance_comb.CASH_REN_MOTOR = month_performance.Sum(x => x.CASH_REN_MOTOR);
                month_performance_comb.CASH_REN_MOTOR_PRM = month_performance.Sum(x => x.CASH_REN_MOTOR_PRM);
                month_performance_comb.CASH_REN_NON_MOTOR = month_performance.Sum(x => x.CASH_REN_NON_MOTOR);
                month_performance_comb.CASH_REN_NON_MOTOR_PRM = month_performance.Sum(x => x.CASH_REN_NON_MOTOR_PRM);
                month_performance_comb.DEBIT_NEW_MOTOR = month_performance.Sum(x => x.DEBIT_NEW_MOTOR);
                month_performance_comb.DEBIT_NEW_MOTOR_PRM = month_performance.Sum(x => x.DEBIT_NEW_MOTOR_PRM);
                month_performance_comb.DEBIT_NEW_NON_MOTOR = month_performance.Sum(x => x.DEBIT_NEW_NON_MOTOR);
                month_performance_comb.DEBIT_NEW_NON_MOTOR_PRM = month_performance.Sum(x => x.DEBIT_NEW_NON_MOTOR_PRM);
                month_performance_comb.DEBIT_REN_MOTOR = month_performance.Sum(x => x.DEBIT_REN_MOTOR);
                month_performance_comb.DEBIT_REN_MOTOR_PRM = month_performance.Sum(x => x.DEBIT_REN_MOTOR_PRM);
                month_performance_comb.DEBIT_REN_NON_MOTOR = month_performance.Sum(x => x.DEBIT_REN_NON_MOTOR);
                month_performance_comb.DEBIT_REN_NON_MOTOR_PRM = month_performance.Sum(x => x.DEBIT_REN_NON_MOTOR_PRM);
                month_performance_comb.TOT_CASH_MOTOR = month_performance.Sum(x => x.TOT_CASH_MOTOR);
                month_performance_comb.TOT_CASH_MOTOR_PRM = month_performance.Sum(x => x.TOT_CASH_MOTOR_PRM);
                month_performance_comb.TOT_CASH_NON_MOTOR = month_performance.Sum(x => x.TOT_CASH_NON_MOTOR);
                month_performance_comb.TOT_CASH_NON_MOTOR_PRM = month_performance.Sum(x => x.TOT_CASH_NON_MOTOR_PRM);
                month_performance_comb.TOT_DEBIT_MOTOR = month_performance.Sum(x => x.TOT_DEBIT_MOTOR);
                month_performance_comb.TOT_DEBIT_MOTOR_PRM = month_performance.Sum(x => x.TOT_DEBIT_MOTOR_PRM);
                month_performance_comb.TOT_DEBIT_NON_MOTOR = month_performance.Sum(x => x.TOT_DEBIT_NON_MOTOR);
                month_performance_comb.TOT_DEBIT_NON_MOTOR_PRM = month_performance.Sum(x => x.TOT_DEBIT_NON_MOTOR_PRM);
                month_performance_comb.TOT_MOTOR_REFUND = month_performance.Sum(x => x.TOT_MOTOR_REFUND);
                month_performance_comb.TOT_NON_MOTOR_REFUND = month_performance.Sum(x => x.TOT_NON_MOTOR_REFUND);
                month_performance_comb.TOT_MOTOR_REFUND_PRM = month_performance.Sum(x => x.TOT_MOTOR_REFUND_PRM);
                month_performance_comb.TOT_NON_MOTOR_REFUND_PRM = month_performance.Sum(x => x.TOT_NON_MOTOR_REFUND_PRM);



                year_performance = _apiServices.GetTeamPerformance(Settings.AccessToken, Settings.orgTeamCode, DateTime.Today.AddYears(-1).ToString("yyyy-01-01"), DateTime.Today.AddYears(-1).ToString("yyyy-MM-dd"));
                //year_performance = _apiServices.GetTeamPerformance(Settings.AccessToken, Settings.orgTeamCode, DateTime.Today.ToString("yyyy-MM-01"), DateTime.Today.ToString("yyyy-MM-dd"));
                year_performance_comb.CASH_NEW_MOTOR = year_performance.Sum(x => x.CASH_NEW_MOTOR);
                year_performance_comb.CASH_NEW_MOTOR_PRM = year_performance.Sum(x => x.CASH_NEW_MOTOR_PRM);
                year_performance_comb.CASH_NEW_NON_MOTOR = year_performance.Sum(x => x.CASH_NEW_NON_MOTOR);
                year_performance_comb.CASH_NEW_NON_MOTOR_PRM = year_performance.Sum(x => x.CASH_NEW_NON_MOTOR_PRM);
                year_performance_comb.CASH_REN_MOTOR = year_performance.Sum(x => x.CASH_REN_MOTOR);
                year_performance_comb.CASH_REN_MOTOR_PRM = year_performance.Sum(x => x.CASH_REN_MOTOR_PRM);
                year_performance_comb.CASH_REN_NON_MOTOR = year_performance.Sum(x => x.CASH_REN_NON_MOTOR);
                year_performance_comb.CASH_REN_NON_MOTOR_PRM = year_performance.Sum(x => x.CASH_REN_NON_MOTOR_PRM);
                year_performance_comb.DEBIT_NEW_MOTOR = year_performance.Sum(x => x.DEBIT_NEW_MOTOR);
                year_performance_comb.DEBIT_NEW_MOTOR_PRM = year_performance.Sum(x => x.DEBIT_NEW_MOTOR_PRM);
                year_performance_comb.DEBIT_NEW_NON_MOTOR = year_performance.Sum(x => x.DEBIT_NEW_NON_MOTOR);
                year_performance_comb.DEBIT_NEW_NON_MOTOR_PRM = year_performance.Sum(x => x.DEBIT_NEW_NON_MOTOR_PRM);
                year_performance_comb.DEBIT_REN_MOTOR = year_performance.Sum(x => x.DEBIT_REN_MOTOR);
                year_performance_comb.DEBIT_REN_MOTOR_PRM = year_performance.Sum(x => x.DEBIT_REN_MOTOR_PRM);
                year_performance_comb.DEBIT_REN_NON_MOTOR = year_performance.Sum(x => x.DEBIT_REN_NON_MOTOR);
                year_performance_comb.DEBIT_REN_NON_MOTOR_PRM = year_performance.Sum(x => x.DEBIT_REN_NON_MOTOR_PRM);
                year_performance_comb.TOT_CASH_MOTOR = year_performance.Sum(x => x.TOT_CASH_MOTOR);
                year_performance_comb.TOT_CASH_MOTOR_PRM = year_performance.Sum(x => x.TOT_CASH_MOTOR_PRM);
                year_performance_comb.TOT_CASH_NON_MOTOR = year_performance.Sum(x => x.TOT_CASH_NON_MOTOR);
                year_performance_comb.TOT_CASH_NON_MOTOR_PRM = year_performance.Sum(x => x.TOT_CASH_NON_MOTOR_PRM);
                year_performance_comb.TOT_DEBIT_MOTOR = year_performance.Sum(x => x.TOT_DEBIT_MOTOR);
                year_performance_comb.TOT_DEBIT_MOTOR_PRM = year_performance.Sum(x => x.TOT_DEBIT_MOTOR_PRM);
                year_performance_comb.TOT_DEBIT_NON_MOTOR = year_performance.Sum(x => x.TOT_DEBIT_NON_MOTOR);
                year_performance_comb.TOT_DEBIT_NON_MOTOR_PRM = year_performance.Sum(x => x.TOT_DEBIT_NON_MOTOR_PRM);
                year_performance_comb.TOT_MOTOR_REFUND = year_performance.Sum(x => x.TOT_MOTOR_REFUND);
                year_performance_comb.TOT_NON_MOTOR_REFUND = year_performance.Sum(x => x.TOT_NON_MOTOR_REFUND);
                year_performance_comb.TOT_MOTOR_REFUND_PRM = year_performance.Sum(x => x.TOT_MOTOR_REFUND_PRM);
                year_performance_comb.TOT_NON_MOTOR_REFUND_PRM = month_performance.Sum(x => x.TOT_NON_MOTOR_REFUND_PRM);
            }
            catch (Exception e)
            {

            }
        }

        private void fetchData(string month, string year)
        {
            string fromDateMonth = year + "-" + month + "-01";
            string fromDateYear = year + "-01-01";
            string lastDate = DateTime.DaysInMonth(Convert.ToInt32(year), Convert.ToInt32(month)).ToString();
            string toDateMonth = year + "-" + month + "-" + (lastDate.Length == 1 ? "0" + lastDate : lastDate);

            month_performance = _apiServices.GetTeamPerformance(Settings.AccessToken, Settings.orgTeamCode, fromDateMonth, toDateMonth);
            month_performance_comb.CASH_NEW_MOTOR = month_performance.Sum(x => x.CASH_NEW_MOTOR);
            month_performance_comb.CASH_NEW_MOTOR_PRM = month_performance.Sum(x => x.CASH_NEW_MOTOR_PRM);
            month_performance_comb.CASH_NEW_NON_MOTOR = month_performance.Sum(x => x.CASH_NEW_NON_MOTOR);
            month_performance_comb.CASH_NEW_NON_MOTOR_PRM = month_performance.Sum(x => x.CASH_NEW_NON_MOTOR_PRM);
            month_performance_comb.CASH_REN_MOTOR = month_performance.Sum(x => x.CASH_REN_MOTOR);
            month_performance_comb.CASH_REN_MOTOR_PRM = month_performance.Sum(x => x.CASH_REN_MOTOR_PRM);
            month_performance_comb.CASH_REN_NON_MOTOR = month_performance.Sum(x => x.CASH_REN_NON_MOTOR);
            month_performance_comb.CASH_REN_NON_MOTOR_PRM = month_performance.Sum(x => x.CASH_REN_NON_MOTOR_PRM);
            month_performance_comb.DEBIT_NEW_MOTOR = month_performance.Sum(x => x.DEBIT_NEW_MOTOR);
            month_performance_comb.DEBIT_NEW_MOTOR_PRM = month_performance.Sum(x => x.DEBIT_NEW_MOTOR_PRM);
            month_performance_comb.DEBIT_NEW_NON_MOTOR = month_performance.Sum(x => x.DEBIT_NEW_NON_MOTOR);
            month_performance_comb.DEBIT_NEW_NON_MOTOR_PRM = month_performance.Sum(x => x.DEBIT_NEW_NON_MOTOR_PRM);
            month_performance_comb.DEBIT_REN_MOTOR = month_performance.Sum(x => x.DEBIT_REN_MOTOR);
            month_performance_comb.DEBIT_REN_MOTOR_PRM = month_performance.Sum(x => x.DEBIT_REN_MOTOR_PRM);
            month_performance_comb.DEBIT_REN_NON_MOTOR = month_performance.Sum(x => x.DEBIT_REN_NON_MOTOR);
            month_performance_comb.DEBIT_REN_NON_MOTOR_PRM = month_performance.Sum(x => x.DEBIT_REN_NON_MOTOR_PRM);
            month_performance_comb.TOT_CASH_MOTOR = month_performance.Sum(x => x.TOT_CASH_MOTOR);
            month_performance_comb.TOT_CASH_MOTOR_PRM = month_performance.Sum(x => x.TOT_CASH_MOTOR_PRM);
            month_performance_comb.TOT_CASH_NON_MOTOR = month_performance.Sum(x => x.TOT_CASH_NON_MOTOR);
            month_performance_comb.TOT_CASH_NON_MOTOR_PRM = month_performance.Sum(x => x.TOT_CASH_NON_MOTOR_PRM);
            month_performance_comb.TOT_DEBIT_MOTOR = month_performance.Sum(x => x.TOT_DEBIT_MOTOR);
            month_performance_comb.TOT_DEBIT_MOTOR_PRM = month_performance.Sum(x => x.TOT_DEBIT_MOTOR_PRM);
            month_performance_comb.TOT_DEBIT_NON_MOTOR = month_performance.Sum(x => x.TOT_DEBIT_NON_MOTOR);
            month_performance_comb.TOT_DEBIT_NON_MOTOR_PRM = month_performance.Sum(x => x.TOT_DEBIT_NON_MOTOR_PRM);
            month_performance_comb.TOT_MOTOR_REFUND = month_performance.Sum(x => x.TOT_MOTOR_REFUND);
            month_performance_comb.TOT_NON_MOTOR_REFUND = month_performance.Sum(x => x.TOT_NON_MOTOR_REFUND);
            month_performance_comb.TOT_MOTOR_REFUND_PRM = month_performance.Sum(x => x.TOT_MOTOR_REFUND_PRM);
            month_performance_comb.TOT_NON_MOTOR_REFUND_PRM = month_performance.Sum(x => x.TOT_NON_MOTOR_REFUND_PRM);


            year_performance = _apiServices.GetTeamPerformance(Settings.AccessToken, Settings.orgTeamCode, fromDateYear, DateTime.Today.ToString("yyyy-MM-dd"));
            //year_performance = _apiServices.GetTeamPerformance(Settings.AccessToken, Settings.orgTeamCode, fromDateMonth, toDateMonth);
            year_performance_comb.CASH_NEW_MOTOR = year_performance.Sum(x => x.CASH_NEW_MOTOR);
            year_performance_comb.CASH_NEW_MOTOR_PRM = year_performance.Sum(x => x.CASH_NEW_MOTOR_PRM);
            year_performance_comb.CASH_NEW_NON_MOTOR = year_performance.Sum(x => x.CASH_NEW_NON_MOTOR);
            year_performance_comb.CASH_NEW_NON_MOTOR_PRM = year_performance.Sum(x => x.CASH_NEW_NON_MOTOR_PRM);
            year_performance_comb.CASH_REN_MOTOR = year_performance.Sum(x => x.CASH_REN_MOTOR);
            year_performance_comb.CASH_REN_MOTOR_PRM = year_performance.Sum(x => x.CASH_REN_MOTOR_PRM);
            year_performance_comb.CASH_REN_NON_MOTOR = year_performance.Sum(x => x.CASH_REN_NON_MOTOR);
            year_performance_comb.CASH_REN_NON_MOTOR_PRM = year_performance.Sum(x => x.CASH_REN_NON_MOTOR_PRM);
            year_performance_comb.DEBIT_NEW_MOTOR = year_performance.Sum(x => x.DEBIT_NEW_MOTOR);
            year_performance_comb.DEBIT_NEW_MOTOR_PRM = year_performance.Sum(x => x.DEBIT_NEW_MOTOR_PRM);
            year_performance_comb.DEBIT_NEW_NON_MOTOR = year_performance.Sum(x => x.DEBIT_NEW_NON_MOTOR);
            year_performance_comb.DEBIT_NEW_NON_MOTOR_PRM = year_performance.Sum(x => x.DEBIT_NEW_NON_MOTOR_PRM);
            year_performance_comb.DEBIT_REN_MOTOR = year_performance.Sum(x => x.DEBIT_REN_MOTOR);
            year_performance_comb.DEBIT_REN_MOTOR_PRM = year_performance.Sum(x => x.DEBIT_REN_MOTOR_PRM);
            year_performance_comb.DEBIT_REN_NON_MOTOR = year_performance.Sum(x => x.DEBIT_REN_NON_MOTOR);
            year_performance_comb.DEBIT_REN_NON_MOTOR_PRM = year_performance.Sum(x => x.DEBIT_REN_NON_MOTOR_PRM);
            year_performance_comb.TOT_CASH_MOTOR = year_performance.Sum(x => x.TOT_CASH_MOTOR);
            year_performance_comb.TOT_CASH_MOTOR_PRM = year_performance.Sum(x => x.TOT_CASH_MOTOR_PRM);
            year_performance_comb.TOT_CASH_NON_MOTOR = year_performance.Sum(x => x.TOT_CASH_NON_MOTOR);
            year_performance_comb.TOT_CASH_NON_MOTOR_PRM = year_performance.Sum(x => x.TOT_CASH_NON_MOTOR_PRM);
            year_performance_comb.TOT_DEBIT_MOTOR = year_performance.Sum(x => x.TOT_DEBIT_MOTOR);
            year_performance_comb.TOT_DEBIT_MOTOR_PRM = year_performance.Sum(x => x.TOT_DEBIT_MOTOR_PRM);
            year_performance_comb.TOT_DEBIT_NON_MOTOR = year_performance.Sum(x => x.TOT_DEBIT_NON_MOTOR);
            year_performance_comb.TOT_DEBIT_NON_MOTOR_PRM = year_performance.Sum(x => x.TOT_DEBIT_NON_MOTOR_PRM);
            year_performance_comb.TOT_MOTOR_REFUND = year_performance.Sum(x => x.TOT_MOTOR_REFUND);
            year_performance_comb.TOT_NON_MOTOR_REFUND = year_performance.Sum(x => x.TOT_NON_MOTOR_REFUND);
            year_performance_comb.TOT_MOTOR_REFUND_PRM = year_performance.Sum(x => x.TOT_MOTOR_REFUND_PRM);
            year_performance_comb.TOT_NON_MOTOR_REFUND_PRM = year_performance.Sum(x => x.TOT_NON_MOTOR_REFUND_PRM);

        }

        private List<Year> getYears()
        {
            var years = new List<Year>();
            years.Clear();

            for (int i = DateTime.Today.Year; i > DateTime.Today.Year - 10; i--)
            {
                years.Add(new Year() { yearID = i, yearVal = i });
            }
            return years;


        }

        private List<Month> getMonths()
        {
            var months = new List<Month>()
            {
                new Month() {number = 1, name ="January" },
                new Month() {number = 2, name ="Fenbruary" },
                new Month() {number = 3, name ="March" },
                new Month() {number = 4, name ="April" },
                new Month() {number = 5, name ="May" },
                new Month() {number = 6, name ="June" },
                new Month() {number = 7, name ="July" },
                new Month() {number = 8, name ="August" },
                new Month() {number = 9, name ="September" },
                new Month() {number = 10, name ="October" },
                new Month() {number = 11, name ="November" },
                new Month() {number = 12, name ="December" }
            };

            return months;
        }


        private bool _isBusy;
        

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        public async Task GetAgentStats()
        {
           // hardCoded();            
        }

        public async Task GetAgentStats(Models.AgentPerformance month, Models.AgentPerformance year, string _month, int _year)
        {
            hardCoded(month, year, _month, _year);
        }

        private void hardCoded(Models.AgentPerformance month, Models.AgentPerformance year, string _month, int _year)
        {

            ownAgt = new AgtPerfmStat
            {

                agentID = 0,
                year = _year,
                month = _month,

                indMonthNoPolTotal = month.TOT_CASH_MOTOR + month.TOT_DEBIT_MOTOR + month.TOT_CASH_NON_MOTOR + month.TOT_DEBIT_NON_MOTOR,
                indMonthNoPol_refund = (month.TOT_NON_MOTOR_REFUND + month.TOT_MOTOR_REFUND),
                indMonthNoPolTotal_cash = month.TOT_CASH_MOTOR + month.TOT_CASH_NON_MOTOR,
                indMonthNoPolTotal_Dbt = month.TOT_DEBIT_MOTOR + month.TOT_DEBIT_NON_MOTOR,

                indYearPolTotal = year.TOT_CASH_MOTOR + year.TOT_DEBIT_MOTOR + year.TOT_CASH_NON_MOTOR + year.TOT_DEBIT_NON_MOTOR ,
                indYearNoPol_refund = (year.TOT_NON_MOTOR_REFUND + year.TOT_MOTOR_REFUND),
                indYearPolTotal_cash = year.TOT_CASH_MOTOR + year.TOT_CASH_NON_MOTOR,
                indYearPolTotal_Dbt = year.TOT_DEBIT_MOTOR + year.TOT_DEBIT_NON_MOTOR,

                indMonthPremTotal = month.TOT_CASH_MOTOR_PRM + month.TOT_DEBIT_MOTOR_PRM + month.TOT_CASH_NON_MOTOR_PRM + month.TOT_DEBIT_NON_MOTOR_PRM ,
                indMonthPremTotal_str = (month.TOT_CASH_MOTOR_PRM + month.TOT_DEBIT_MOTOR_PRM + month.TOT_CASH_NON_MOTOR_PRM + month.TOT_DEBIT_NON_MOTOR_PRM).ToString("N0"),
                indMonthPrem_refund = (month.TOT_MOTOR_REFUND_PRM + month.TOT_NON_MOTOR_REFUND_PRM),
                indMonthPrem_refund_str = (month.TOT_MOTOR_REFUND_PRM + month.TOT_NON_MOTOR_REFUND_PRM).ToString("N0"),
                indMonthPremTotal_cash = month.TOT_CASH_MOTOR_PRM + month.TOT_CASH_NON_MOTOR_PRM,
                indMonthPremTotal_Dbt = month.TOT_DEBIT_MOTOR_PRM + month.TOT_DEBIT_NON_MOTOR_PRM,

                indYearPremTotal = year.TOT_CASH_MOTOR_PRM + year.TOT_DEBIT_MOTOR_PRM + year.TOT_CASH_NON_MOTOR_PRM + year.TOT_DEBIT_NON_MOTOR_PRM ,
                indYearPremTotal_str = (year.TOT_CASH_MOTOR_PRM + year.TOT_DEBIT_MOTOR_PRM + year.TOT_CASH_NON_MOTOR_PRM + year.TOT_DEBIT_NON_MOTOR_PRM).ToString("N0"),
                indYearPrem_refund = (year.TOT_MOTOR_REFUND_PRM + year.TOT_NON_MOTOR_REFUND_PRM),
                indYearPrem_refund_str = (year.TOT_MOTOR_REFUND_PRM + year.TOT_NON_MOTOR_REFUND_PRM).ToString("N0"),
                indYearPremTotal_cash = year.TOT_CASH_MOTOR_PRM + year.TOT_CASH_NON_MOTOR_PRM,
                indYearPremTotal_Dbt = year.TOT_DEBIT_MOTOR_PRM + year.TOT_DEBIT_NON_MOTOR_PRM,

                indMonthNoPol_New = month.CASH_NEW_MOTOR + month.DEBIT_NEW_MOTOR + month.CASH_NEW_NON_MOTOR + month.DEBIT_NEW_NON_MOTOR,
                indMonthNoPol_New_Cash = month.CASH_NEW_MOTOR + month.CASH_NEW_NON_MOTOR,
                indMonthNoPol_New_Dbt = month.DEBIT_NEW_MOTOR + month.DEBIT_NEW_NON_MOTOR,

                indYearPol_New = year.CASH_NEW_MOTOR + year.DEBIT_NEW_MOTOR + year.CASH_NEW_NON_MOTOR + year.DEBIT_NEW_NON_MOTOR,
                indYearPol_New_Cash = year.CASH_NEW_MOTOR + year.CASH_NEW_NON_MOTOR,
                indYearPol_New_Dbt = year.DEBIT_NEW_MOTOR + year.DEBIT_NEW_NON_MOTOR,

                indMonthPrem_New = month.CASH_NEW_MOTOR_PRM + month.DEBIT_NEW_MOTOR_PRM + month.CASH_NEW_NON_MOTOR_PRM + month.DEBIT_NEW_NON_MOTOR_PRM,
                indMonthPrem_New_str = (month.CASH_NEW_MOTOR_PRM + month.DEBIT_NEW_MOTOR_PRM + month.CASH_NEW_NON_MOTOR_PRM + month.DEBIT_NEW_NON_MOTOR_PRM).ToString("N0"),
                indMonthPrem_New_Cash = month.CASH_NEW_MOTOR_PRM + month.CASH_NEW_NON_MOTOR_PRM,
                indMonthPrem_New_Dbt = month.DEBIT_NEW_MOTOR_PRM + month.DEBIT_NEW_NON_MOTOR_PRM,

                indYearPrem_New = year.CASH_NEW_MOTOR_PRM + year.DEBIT_NEW_MOTOR_PRM + year.CASH_NEW_NON_MOTOR_PRM + year.DEBIT_NEW_NON_MOTOR_PRM,
                indYearPrem_New_str = (year.CASH_NEW_MOTOR_PRM + year.DEBIT_NEW_MOTOR_PRM + year.CASH_NEW_NON_MOTOR_PRM + year.DEBIT_NEW_NON_MOTOR_PRM).ToString("N0"),
                indYearPrem_New_Cash = year.CASH_NEW_MOTOR_PRM + year.CASH_NEW_NON_MOTOR_PRM,
                indYearPrem_New_Dbt = year.DEBIT_NEW_MOTOR_PRM + year.DEBIT_NEW_NON_MOTOR_PRM,

                indMonthNoPol_Renewal = month.CASH_REN_MOTOR + month.DEBIT_REN_MOTOR + month.CASH_REN_NON_MOTOR + month.DEBIT_REN_NON_MOTOR,
                indMonthNoPol_Renewal_cash = month.CASH_REN_MOTOR + month.CASH_REN_NON_MOTOR,
                indMonthNoPol_Renewal_Dbt = month.DEBIT_REN_MOTOR + month.DEBIT_REN_NON_MOTOR,

                indYearPol_Renewal = year.CASH_REN_MOTOR + year.DEBIT_REN_MOTOR + year.CASH_REN_NON_MOTOR + year.DEBIT_REN_NON_MOTOR,
                indYearPol_Renewal_cash = year.CASH_REN_MOTOR + year.CASH_REN_NON_MOTOR,
                indYearPol_Renewal_Dbt = year.DEBIT_REN_MOTOR + year.DEBIT_REN_NON_MOTOR,

                indMonthPrem_Renewal = month.CASH_REN_MOTOR_PRM + month.DEBIT_REN_MOTOR_PRM + month.CASH_REN_NON_MOTOR_PRM + month.DEBIT_REN_NON_MOTOR_PRM,
                indMonthPrem_Renewal_str = (month.CASH_REN_MOTOR_PRM + month.DEBIT_REN_MOTOR_PRM + month.CASH_REN_NON_MOTOR_PRM + month.DEBIT_REN_NON_MOTOR_PRM).ToString("N0"),
                indMonthPrem_Renewal_cash = month.CASH_REN_MOTOR_PRM + month.CASH_REN_NON_MOTOR_PRM,
                indMonthPrem_Renewal_Dbt = month.DEBIT_REN_MOTOR_PRM + month.DEBIT_REN_NON_MOTOR_PRM,

                indYearPrem_Renewal = year.CASH_REN_MOTOR_PRM + year.DEBIT_REN_MOTOR_PRM + year.CASH_REN_NON_MOTOR_PRM + year.DEBIT_REN_NON_MOTOR_PRM,
                indYearPrem_Renewal_str = (year.CASH_REN_MOTOR_PRM + year.DEBIT_REN_MOTOR_PRM + year.CASH_REN_NON_MOTOR_PRM + year.DEBIT_REN_NON_MOTOR_PRM).ToString("N0"),
                indYearPrem_Renewal_cash = year.CASH_REN_MOTOR_PRM + year.CASH_REN_NON_MOTOR_PRM,
                indYearPrem_Renewal_Dbt = year.DEBIT_REN_MOTOR_PRM + year.DEBIT_REN_NON_MOTOR_PRM,

                branchMonthNoPolTotal = 0,
                branchYearPolTotal = 0,

                branchMonthPremTotal = 0,
                branchYearPremTotal = 0,


            };
        }

        private PlotModel Indvidual_month_no_of_pol()
        {
            double t = ownAgt.indMonthNoPol_Renewal;

            var model = new PlotModel { Title = "No. of Policies for " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(ownAgt.month)) };

            var ps = new PieSeries
            {
                StrokeThickness = .25,
                InsideLabelPosition = .25,
                TickRadialLength = 0.8,
                AngleSpan = 360,
                StartAngle = 0,

                Diameter = 0.9,
                InnerDiameter = .4
            };

            ps.Slices.Add(new PieSlice("", ownAgt.indMonthNoPol_New_Cash) { IsExploded = false, Fill = OxyColor.FromRgb(130, 177, 185) });
            ps.Slices.Add(new PieSlice("", ownAgt.indMonthNoPol_New_Dbt) { IsExploded = false, Fill = OxyColor.FromRgb(54, 139, 193) });
            ps.Slices.Add(new PieSlice("", ownAgt.indMonthNoPol_Renewal_cash) { IsExploded = false, Fill = OxyColor.FromRgb(235, 199, 170) });
            ps.Slices.Add(new PieSlice("", ownAgt.indMonthNoPol_Renewal_Dbt) { IsExploded = false, Fill = OxyColor.FromRgb(195, 159, 130) });
            model.Series.Add(ps);
            return model;
        }

        private PlotModel Indvidual_year_no_of_pol()
        {

            var model = new PlotModel { Title = "No. of Policies for " + ownAgt.year };

            var ps = new PieSeries
            {
                StrokeThickness = .25,
                InsideLabelPosition = .25,
                TickRadialLength = 0.8,
                AngleSpan = 360,
                StartAngle = 0,

                Diameter = 0.9,
                InnerDiameter = .4
            };

            ps.Slices.Add(new PieSlice("", ownAgt.indYearPol_New_Cash) { IsExploded = false, Fill = OxyColor.FromRgb(130, 177, 185) });
            ps.Slices.Add(new PieSlice("", ownAgt.indYearPol_New_Dbt) { IsExploded = false, Fill = OxyColor.FromRgb(54, 139, 193) });
            ps.Slices.Add(new PieSlice("", ownAgt.indYearPol_Renewal_cash) { IsExploded = false, Fill = OxyColor.FromRgb(235, 199, 170) });
            ps.Slices.Add(new PieSlice("", ownAgt.indYearPol_Renewal_Dbt) { IsExploded = false, Fill = OxyColor.FromRgb(195, 159, 130) });
            model.Series.Add(ps);
            return model;
        }

        private PlotModel Indvidual_month_premium()
        {

            var model = new PlotModel { Title = "Premium Income for " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(ownAgt.month)) };

            var ps = new PieSeries
            {
                StrokeThickness = .25,
                InsideLabelPosition = .25,
                TickRadialLength = 0.8,
                AngleSpan = 360,
                StartAngle = 0,

                Diameter = 0.9,
                InnerDiameter = .4
            };

            ps.Slices.Add(new PieSlice("", ownAgt.indMonthPrem_New_Cash) { IsExploded = false, Fill = OxyColor.FromRgb(130, 177, 185) });
            ps.Slices.Add(new PieSlice("", ownAgt.indMonthPrem_New_Dbt) { IsExploded = false, Fill = OxyColor.FromRgb(54, 139, 193) });
            ps.Slices.Add(new PieSlice("", ownAgt.indMonthPrem_Renewal_cash) { IsExploded = false, Fill = OxyColor.FromRgb(235, 199, 170) });
            ps.Slices.Add(new PieSlice("", ownAgt.indMonthPrem_Renewal_Dbt) { IsExploded = false, Fill = OxyColor.FromRgb(195, 159, 130) });
            model.Series.Add(ps);
            return model;
        }

        private PlotModel Indvidual_year_premium()
        {

            var model = new PlotModel { Title = "Premium Income for " + ownAgt.year };

            var ps = new PieSeries
            {
                StrokeThickness = .25,
                InsideLabelPosition = .25,
                TickRadialLength = 0.8,
                AngleSpan = 360,
                StartAngle = 0,

                Diameter = 0.9,
                InnerDiameter = .4
            };

            ps.Slices.Add(new PieSlice("", ownAgt.indYearPrem_New_Cash) { IsExploded = false, Fill = OxyColor.FromRgb(130, 177, 185) });
            ps.Slices.Add(new PieSlice("", ownAgt.indYearPrem_New_Dbt) { IsExploded = false, Fill = OxyColor.FromRgb(54, 139, 193) });
            ps.Slices.Add(new PieSlice("", ownAgt.indYearPrem_Renewal_cash) { IsExploded = false, Fill = OxyColor.FromRgb(235, 199, 170) });
            ps.Slices.Add(new PieSlice("", ownAgt.indYearPrem_Renewal_Dbt) { IsExploded = false, Fill = OxyColor.FromRgb(195, 159, 130) });
            model.Series.Add(ps);
            return model;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Populate_controls(Models.AgentPerformance monthPerf, Models.AgentPerformance yearPerf, string month, string year)
        {
            inq_year = Convert.ToInt32(year);
            lbl_mon_NOP = "No. of Policies for " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(month));
            lbl_yr_NOP = "No. of Policies for " + inq_year;
            lbl_mon_prem = "Total Premium for " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(month));
            lbl_yr_prem = "Total Premium for " + inq_year;

            PieMonthNoPol = Indvidual_month_no_of_pol();
            PieYearNoPol = Indvidual_year_no_of_pol();
            PieMonthPrem = Indvidual_month_premium();
            PieYearPrem = Indvidual_year_premium();
        }

        
        public ICommand FilterPerformanceDate
        {
            get
            {
                return new Command(async () =>
                {
                    
                    month_performance_comb = new Models.AgentPerformance();
                    year_performance_comb = new Models.AgentPerformance();

                    IsBusy = true;
                    //Task.Delay(5000);
                    month_performance = null;
                    year_performance = null;
                    fetchData((_getMoth.number.ToString().Length == 1 ? "0" + _getMoth.number.ToString() : _getMoth.number.ToString()), _getYear.yearVal.ToString());
                    if (month_performance.Count > 0 && year_performance.Count > 0)
                    {
                        await GetAgentStats(month_performance_comb, year_performance_comb, (_getMoth.number.ToString().Length == 1 ? "0" + _getMoth.number.ToString() : _getMoth.number.ToString()), _getYear.yearVal);
                        Populate_controls(month_performance_comb, year_performance_comb, (_getMoth.number.ToString().Length == 1 ? "0" + _getMoth.number.ToString() : _getMoth.number.ToString()), _getYear.yearVal.ToString());
                    }
                    //OnPropertyChanged();
                    IsBusy = false;
                });
            }
        }
    }
}
