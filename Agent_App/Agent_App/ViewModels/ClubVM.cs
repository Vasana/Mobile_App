﻿using Agent_App.Helpers;
using Agent_App.Services;
using OxyPlot;
using OxyPlot.Axes;
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
    public class ClubVM : INotifyPropertyChanged
    {
        private bool _isBusy;
        ApiServicesLife _apiServices = new ApiServicesLife();
        public PlotModel AreaModelNoPol { get; set; }
        public double avg_line { get; set; }
        //public Dictionary<int, double> AmountsList = new Dictionary<int, double>();

        public ClubResponse ClubRespons
        {
            get => _clubRespons;
            set
            {
                _clubRespons = value;
                OnPropertyChanged();
            }
        }

        private ClubResponse _clubRespons;
        

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public  void  GetClubInfoAsync()
        {
            IsBusy = true;
            ClubRespons =  _apiServices.GetClubInfoT(accessToken: Settings.AccessToken);
            AreaModelNoPol =  AreaChart_NoOfPoliciesAsync(ClubRespons);
            IsBusy = false;
        }

        public ClubVM()
        {
            GetClubInfoAsync();
            //test();
        }

      

        public  PlotModel AreaChart_NoOfPoliciesAsync(ClubResponse ClubRespons)
        {
            var plotModel1 = new PlotModel { Title = "Performance of last 5 years" };

            plotModel1.InvalidatePlot(true);
            try
            {
                var s1 = new LineSeries()
                {
                    Title = "Current Club",
                    Color = OxyColors.SkyBlue//,
                    //MarkerType = MarkerType.Circle,
                    //MarkerSize = 6,
                    //MarkerStroke = OxyColors.White,
                    //MarkerFill = OxyColors.SkyBlue,
                    //MarkerStrokeThickness = 1.5
                };

                var s2 = new LineSeries()
                {
                    Title = "Next Club",
                    Color = OxyColors.Teal//,
                    //MarkerType = MarkerType.Diamond,
                    //MarkerSize = 6,
                    //MarkerStroke = OxyColors.White,
                    //MarkerFill = OxyColors.Teal,
                    //MarkerStrokeThickness = 1.5
                };


                var s3 = new LineSeries()
                {
                    Title = "Performance",
                    Color = OxyColors.LightPink,
                    MarkerType = MarkerType.Square,
                    MarkerSize = 6,
                    MarkerStroke = OxyColors.White,
                    MarkerFill = OxyColors.LightPink,
                    MarkerStrokeThickness = 1.5
                };

                var s4 = new LineSeries()
                {
                    Title = "Average",
                    Color = OxyColors.LightGray//,
                    //MarkerType = MarkerType.Square,
                    //MarkerSize = 6,
                    //MarkerStroke = OxyColors.White,
                    //MarkerFill = OxyColors.LightPink,
                    //MarkerStrokeThickness = 1.5
                };



                //s1.Points.Add(new DataPoint(0, 0));
                //s2.Points.Add(new DataPoint(0, 0));
                //s3.Points.Add(new DataPoint(0, 0));
                int i = 0;
                int year = DateTime.Today.Year;
                foreach (double item in ClubRespons.Last5yearList)
                {
                    i++;
                    //if (item.YEAR_MONTH.ToString() == running_month_str)
                    //{
                        s1.Points.Add(new DataPoint((year + 1) - i, ClubRespons.CurrentLimit));
                        s2.Points.Add(new DataPoint((year + 1) - i, ClubRespons.NextLimit));
                        s3.Points.Add(new DataPoint((year + 1) - i, item));
                        s4.Points.Add(new DataPoint((year + 1) - i, ClubRespons.last5yearAvg));
                    //}
                    //else
                    //{
                    //    s1.Points.Add(new DataPoint(running_month, 0));
                    //    s2.Points.Add(new DataPoint(running_month, 0));
                    //    s3.Points.Add(new DataPoint(running_month, 0));
                    //}


                }

                plotModel1.Series.Add(s1);
                plotModel1.Series.Add(s2);
                plotModel1.Series.Add(s3);
                plotModel1.Series.Add(s4);

                

                plotModel1.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title="Commission Income",  StringFormat = "N", Minimum = 0, IsPanEnabled = false, IsZoomEnabled = false });
                plotModel1.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MinimumMajorStep = 1d, IsPanEnabled = false, IsZoomEnabled = false });
                //
                //AreaModelNoPol = plotModel1;
            }
            catch (Exception s)
            {

            }

            plotModel1.InvalidatePlot(true);
            return plotModel1;
        }

        private AxisPosition CategoryAxisPosition()
        {
            if (typeof(BarSeries) == typeof(ColumnSeries))
            {
                return AxisPosition.Bottom;
            }

            return AxisPosition.Left;
        }

        private AxisPosition ValueAxisPosition()
        {
            if (typeof(BarSeries) == typeof(ColumnSeries))
            {
                return AxisPosition.Left;
            }

            return AxisPosition.Bottom;
        }
    }

}