using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Agent_App.Models
{
    public class PerformanceStats : INotifyPropertyChanged
    {
        //public ObservableCollection<AgtPerfmStat> CardDataCollection { get; set; }
        public AgtPerfmStat ownAgt { get; set; }

        public PlotModel PieMonthNoPol { get; set; }
        public PlotModel PieYearNoPol { get; set; }
        public PlotModel PieYearPrem { get; set; }
        public PlotModel PieMonthPrem { get; set; }

        public PerformanceStats()
        {
            GetAgentStats();
            PieMonthNoPol = CreatePieChart();
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

            ownAgt = new AgtPerfmStat {
            

                        agentID  = 905717,
                        indMonthNoPolTotal  = 12,
                        indYearPolTotal  = 78,
                        indMonthPremTotal  = 12000,
                        indYearPremTotal  = 3408000,

                        branchMonthNoPolTotal  = 67,
                        branchYearPolTotal  = 560,
                        branchMonthPremTotal  = 4500000,
                        branchYearPremTotal  = 6788888,

                        indMonthNoPolCash  = 6,
                        indYearPolCash  = 34,
                        indMonthPremCash  = 6000,
                        indYearPremCash  = 340000,

                        indMonthNoPolDbt  = 6,
                        indYearPolDbt  = 38,
                        indMonthPremDbt  = 13000,
                        indYearPremDbt  = 33000
                    };

            /*
            {
                CardDataColAgtPerfmStatlection = new ObservableCollection<AgtPerfmStat>
                {
                    new AgtPerfmStat
                    {

                        agentID  = 905717,
                        indMonthNoPolTotal  = 12,
                        indYearPolTotal  = 78,
                        indMonthPremTotal  = 12000,
                        indYearPremTotal  = 3408000,

                        branchMonthNoPolTotal  = 67,
                        branchYearPolTotal  = 560,
                        branchMonthPremTotal  = 4500000,
                        branchYearPremTotal  = 6788888,

                        indMonthNoPolCash  = 6,
                        indYearPolCash  = 34,
                        indMonthPremCash  = 6000,
                        indYearPremCash  = 340000,

                        indMonthNoPolDbt  = 6,
                        indYearPolDbt  = 38,
                        indMonthPremDbt  = 13000,
                        indYearPremDbt  = 33000
                    }
                    
                 
                };*/

            //}
        }

        private PlotModel CreatePieChart()
        {
            var model = new PlotModel { Title = "Pie Chart" };

            var ps = new PieSeries
            {
                StrokeThickness = .25,
                InsideLabelPosition = .25,
                AngleSpan = 360,
                StartAngle = 0
            };

            ps.Slices.Add(new PieSlice("Africa", 1030) { IsExploded = false });
            ps.Slices.Add(new PieSlice("Americas", 929) { IsExploded = false });
            ps.Slices.Add(new PieSlice("Asia", 4157));
            ps.Slices.Add(new PieSlice("Europe", 739) { IsExploded = false });
            ps.Slices.Add(new PieSlice("Oceania", 35) { IsExploded = false });
            model.Series.Add(ps);
            return model;
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
