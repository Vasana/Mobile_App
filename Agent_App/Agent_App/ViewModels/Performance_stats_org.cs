using Agent_App.Models;
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

namespace Agent_App.ViewModels
{
    public class Performance_stats_org : INotifyPropertyChanged
    {
        //public ObservableCollection<AgtPerfmStat> CardDataCollection { get; set; }
        public AgtPerfmStat ownAgt { get; set; }
        public List<Month> MonthList { get; set; }
        public List<Year> YearList { get; set; }
        public string inq_month { get; set; }
        public int inq_year { get; set; }

        public PlotModel PieMonthNoPol { get; set; }
        public PlotModel PieYearNoPol { get; set; }
        public PlotModel PieYearPrem { get; set; }
        public PlotModel PieMonthPrem { get; set; }

        public string lbl_mon_NOP { get; set; }
        public string lbl_yr_NOP { get; set; }
        public string lbl_mon_prem { get; set; }
        public string lbl_yr_prem { get; set; }

        public Performance_stats_org()
        {
            GetAgentStats();
            MonthList = getMonths();
            YearList = getYears();

            inq_year = DateTime.Today.Year;
            lbl_mon_NOP = "Number of Policies for " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Today.Month);
            lbl_yr_NOP = "Number of Policies for " + DateTime.Today.Year;
            lbl_mon_prem = "Total Premium for " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Today.Month);
            lbl_yr_prem = "Total Premium for " + DateTime.Today.Year;

            PieMonthNoPol = Indvidual_month_no_of_pol();
            PieYearNoPol = Indvidual_year_no_of_pol();
            PieMonthPrem = Indvidual_month_premium();
            PieYearPrem = Indvidual_year_premium();
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

        public event PropertyChangedEventHandler PropertyChanged;

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
            IsBusy = true;
            hardCoded();
            IsBusy = false;
        }


        private void hardCoded()
        {

            ownAgt = new AgtPerfmStat
            {


                agentID = 905717,
                year = 2018,
                month = "July",

                indMonthNoPolTotal = 12,
                indMonthNoPolTotal_cash = 9,
                indMonthNoPolTotal_Dbt = 3,

                indYearPolTotal = 78,
                indYearPolTotal_cash = 58,
                indYearPolTotal_Dbt = 20,

                indMonthPremTotal = 12000,
                indMonthPremTotal_cash = 8000,
                indMonthPremTotal_Dbt = 4000,

                indYearPremTotal = 3408000,
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

                branchMonthNoPolTotal = 67,
                branchYearPolTotal = 560,

                branchMonthPremTotal = 4500000,
                branchYearPremTotal = 6788888,


            };
        }

        private PlotModel Indvidual_month_no_of_pol()
        {

            var model = new PlotModel { Title = "Number of Policies for " + ownAgt.month };

            var ps = new PieSeries
            {
                StrokeThickness = .25,
                InsideLabelPosition = .25,
                AngleSpan = 360,
                StartAngle = 0
            };

            ps.Slices.Add(new PieSlice("Cash - New ", ownAgt.indMonthNoPol_New_Cash) { IsExploded = false });
            ps.Slices.Add(new PieSlice("Debit - New", ownAgt.indMonthNoPol_New_Dbt) { IsExploded = false });
            ps.Slices.Add(new PieSlice("Cash - Renewals", ownAgt.indMonthNoPol_Renewal_cash) { IsExploded = false });
            ps.Slices.Add(new PieSlice("Debit - Renewals", ownAgt.indMonthNoPol_Renewal_Dbt) { IsExploded = false });
            model.Series.Add(ps);
            return model;
        }

        private PlotModel Indvidual_year_no_of_pol()
        {

            var model = new PlotModel { Title = "Number of Policies for " + ownAgt.year };

            var ps = new PieSeries
            {
                StrokeThickness = .25,
                InsideLabelPosition = .25,
                AngleSpan = 360,
                StartAngle = 0
            };

            ps.Slices.Add(new PieSlice("Cash - New", ownAgt.indYearPol_New_Cash) { IsExploded = false });
            ps.Slices.Add(new PieSlice("Debit - New", ownAgt.indYearPol_New_Dbt) { IsExploded = false });
            ps.Slices.Add(new PieSlice("Cash - Renewals", ownAgt.indYearPol_Renewal_cash) { IsExploded = false });
            ps.Slices.Add(new PieSlice("Debit - Renewals", ownAgt.indYearPol_Renewal_Dbt) { IsExploded = false });
            model.Series.Add(ps);
            return model;
        }

        private PlotModel Indvidual_month_premium()
        {

            var model = new PlotModel { Title = "Premium Income for " + ownAgt.month };

            var ps = new PieSeries
            {
                StrokeThickness = .25,
                InsideLabelPosition = .25,
                AngleSpan = 360,
                StartAngle = 0
            };

            ps.Slices.Add(new PieSlice("Cash - New", ownAgt.indMonthPrem_New_Cash) { IsExploded = false });
            ps.Slices.Add(new PieSlice("Debit - New", ownAgt.indMonthPrem_New_Dbt) { IsExploded = false });
            ps.Slices.Add(new PieSlice("Cash - Renewals", ownAgt.indMonthPrem_Renewal_cash) { IsExploded = false });
            ps.Slices.Add(new PieSlice("Debit - Renewals", ownAgt.indMonthPrem_Renewal_Dbt) { IsExploded = false });
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
                AngleSpan = 360,
                StartAngle = 0
            };

            ps.Slices.Add(new PieSlice("Cash - New", ownAgt.indYearPrem_New_Cash) { IsExploded = false });
            ps.Slices.Add(new PieSlice("Debit - New", ownAgt.indYearPrem_New_Dbt) { IsExploded = false });
            ps.Slices.Add(new PieSlice("Cash - Renewals", ownAgt.indYearPrem_Renewal_cash) { IsExploded = false });
            ps.Slices.Add(new PieSlice("Debit - Renewals", ownAgt.indYearPrem_Renewal_Dbt) { IsExploded = false });
            model.Series.Add(ps);
            return model;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand FilterPerformanceDate
        {
            get
            {
                return new Command(async () =>
                {
                    //Application.Current.MainPage = new NavigationPage(new ExampleList());

                    await Application.Current.MainPage.Navigation.PushAsync(new Agent_performance());
                });
            }
        }
    }
}
