﻿using Agent_App.Helpers;
using Agent_App.Models;
using Agent_App.Services;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Agent_App.ViewModels
{
    public class BranchEvaluation: INotifyPropertyChanged
    {
        private ApiServices _apiServices = new ApiServices();
        public PlotModel AreaModelNoPol { get; set; }
        public PlotModel AreaModelAnnualInc { get; set; }
        private List<MonthlyPerformance> current_year;
        private List<MonthlyPerformance> last_year;

        public PlotModel PieModel { get; set; }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        public bool _isBusy;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public BranchEvaluation()
        {
            fetchData();
            AreaModelNoPol = AreaChart_NoOfPoliciesAsync();
            //IsBusy = true;
            //LoadAsync();
            AreaModelAnnualInc = AreaChart_PremiumInc();
            //IsBusy = false;
        }

        private void fetchData()
        {
            //last_year = _apiServices.GetMonthlyPerformance(Settings.AccessToken, Settings.agentCode, DateTime.Today.AddYears(-1).ToString("yyyy"));
            ////last_year = last_year.FindAll(x => x.BUSS_TYPE == "Total");
            //last_year.Sort((x, y) => x.YEAR_MONTH.CompareTo(y.YEAR_MONTH));


            current_year = _apiServices.GetMonthlyPerformance(Settings.AccessToken, Settings.agentCode, DateTime.Today.ToString("yyyy"));
            //current_year = current_year.FindAll(x => x.BUSS_TYPE == "Total");
            current_year.Sort((x, y) => x.YEAR_MONTH.CompareTo(y.YEAR_MONTH));
        }



        public PlotModel AreaChart_NoOfPoliciesAsync()
        {
            var plotModel1 = new PlotModel { Title = "Number of Policies" };

            plotModel1.InvalidatePlot(true);
            try
            {
                var s1 = new LineSeries()
                {
                    Title = "Number of policies in this year",
                    Color = OxyColors.SkyBlue,
                    MarkerType = MarkerType.Circle,
                    MarkerSize = 6,
                    MarkerStroke = OxyColors.White,
                    MarkerFill = OxyColors.SkyBlue,
                    MarkerStrokeThickness = 1.5
                };

                var s2 = new LineSeries()
                {
                    Title = "Average No. of policies in Branch",
                    Color = OxyColors.Teal,
                    MarkerType = MarkerType.Diamond,
                    MarkerSize = 6,
                    MarkerStroke = OxyColors.White,
                    MarkerFill = OxyColors.Teal,
                    MarkerStrokeThickness = 1.5
                };


                var s3 = new LineSeries()
                {
                    Title = "Maximum No. of policies in Branch",
                    Color = OxyColors.LightPink,
                    MarkerType = MarkerType.Square,
                    MarkerSize = 6,
                    MarkerStroke = OxyColors.White,
                    MarkerFill = OxyColors.LightPink,
                    MarkerStrokeThickness = 1.5
                };


                string year_str = DateTime.Today.ToString("yyyy");
                int running_month = 1;
                string running_month_str;
                
                running_month = 1;
                s1.Points.Add(new DataPoint(0, 0));
                s2.Points.Add(new DataPoint(0, 0));
                s3.Points.Add(new DataPoint(0, 0));
                foreach (MonthlyPerformance item in current_year)
                {
                    if (running_month < 10)
                        running_month_str = "0" + running_month.ToString();
                    else
                        running_month_str = running_month.ToString();

                    running_month_str = year_str + running_month_str;

                    if (item.YEAR_MONTH.ToString() == running_month_str)
                    {
                        s1.Points.Add(new DataPoint(running_month, item.NO_OF_TOTAL_BUSINESS));
                        s2.Points.Add(new DataPoint(running_month, item.TOTAL_BUSINESS_AVERAGE));
                        s3.Points.Add(new DataPoint(running_month, item.MAX_TOTAL_BUSINESS_COUNT));
                    }
                    else
                    {
                        s1.Points.Add(new DataPoint(running_month, 0));
                        s2.Points.Add(new DataPoint(running_month, 0));
                        s3.Points.Add(new DataPoint(running_month, 0));
                    }

                    if (running_month == 10)
                        running_month_str = "";

                    running_month++;
                }

                plotModel1.Series.Add(s1);
                plotModel1.Series.Add(s2);
                plotModel1.Series.Add(s3);

                
                plotModel1.Axes.Add(new LinearAxis { Position = AxisPosition.Left, IsPanEnabled = false, IsZoomEnabled = false });
                plotModel1.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, IsPanEnabled = false, IsZoomEnabled = false });
                //
                //AreaModelNoPol = plotModel1;
            }
            catch (Exception s)
            {

            }

            plotModel1.InvalidatePlot(true);
            return plotModel1;
        }

        public PlotModel AreaChart_PremiumInc()
        {
            var plotModel1 = new PlotModel { Title = "Premium Collection" };

            plotModel1.InvalidatePlot(true);
            try
            {
                var s1 = new LineSeries()
                {
                    Title = "Premium Collection in this year",
                    Color = OxyColors.SkyBlue,
                    MarkerType = MarkerType.Circle,
                    MarkerSize = 6,
                    MarkerStroke = OxyColors.White,
                    MarkerFill = OxyColors.SkyBlue,
                    MarkerStrokeThickness = 1.5
                };

                var s2 = new LineSeries()
                {
                    Title = "Average Premium Collection in Branch",
                    Color = OxyColors.Teal,
                    MarkerType = MarkerType.Diamond,
                    MarkerSize = 6,
                    MarkerStroke = OxyColors.White,
                    MarkerFill = OxyColors.Teal,
                    MarkerStrokeThickness = 1.5
                };


                var s3 = new LineSeries()
                {
                    Title = "Maximum Premium Collection in Branch",
                    Color = OxyColors.LightPink,
                    MarkerType = MarkerType.Square,
                    MarkerSize = 6,
                    MarkerStroke = OxyColors.White,
                    MarkerFill = OxyColors.LightPink,
                    MarkerStrokeThickness = 1.5
                };


                string year_str = DateTime.Today.ToString("yyyy");
                int running_month = 1;
                string running_month_str;

                running_month = 1;
                s1.Points.Add(new DataPoint(0, 0));
                s2.Points.Add(new DataPoint(0, 0));
                s3.Points.Add(new DataPoint(0, 0));
                foreach (MonthlyPerformance item in current_year)
                {
                    if (running_month < 10)
                        running_month_str = "0" + running_month.ToString();
                    else
                        running_month_str = running_month.ToString();

                    running_month_str = year_str + running_month_str;

                    if (item.YEAR_MONTH.ToString() == running_month_str)
                    {
                        s1.Points.Add(new DataPoint(running_month, item.TOTAL_PREMIUM));
                        s2.Points.Add(new DataPoint(running_month, item.TOTAL_PREMIUM_AVERAGE));
                        s3.Points.Add(new DataPoint(running_month, item.MAX_TOTAL_BUSINESS_PREMIUM));
                    }
                    else
                    {
                        s1.Points.Add(new DataPoint(running_month, 0));
                        s2.Points.Add(new DataPoint(running_month, 0));
                        s3.Points.Add(new DataPoint(running_month, 0));
                    }

                    if (running_month == 10)
                        running_month_str = "";

                    running_month++;
                }

                plotModel1.Series.Add(s1);
                plotModel1.Series.Add(s2);
                plotModel1.Series.Add(s3);


                plotModel1.Axes.Add(new LinearAxis { Position = AxisPosition.Left, StringFormat = "N", IsPanEnabled = false, IsZoomEnabled = false });
                plotModel1.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, IsPanEnabled = false, IsZoomEnabled = false });
                //
                //AreaModelNoPol = plotModel1;
            }
            catch (Exception s)
            {

            }

            plotModel1.InvalidatePlot(true);
            return plotModel1;
        }


        public PlotModel AreaChart_NoOfPolicies()
        {
            var model = new PlotModel { Title = "Annual Premium Income" };


            var s1 = new LineSeries()
            {
                Title = "Annual Income last year",
                Color = OxyColors.SkyBlue,
                MarkerType = MarkerType.Circle,
                MarkerSize = 6,
                MarkerStroke = OxyColors.White,
                MarkerFill = OxyColors.SkyBlue,
                MarkerStrokeThickness = 1.5
            };

            string year_str = DateTime.Today.AddYears(-1).ToString("yyyy");
            int running_month = 1;
            string running_month_str;
            running_month = 1;
            s1.Points.Add(new DataPoint(0, 0));
            foreach (MonthlyPerformance item in last_year)
            {
                if (running_month < 10)
                    running_month_str = "0" + running_month.ToString();
                else
                    running_month_str = running_month.ToString();

                running_month_str = year_str + running_month_str;

                if (item.YEAR_MONTH.ToString() == running_month_str)
                    s1.Points.Add(new DataPoint(running_month, (item.TOTAL_PREMIUM - item.TOTAL_REFUND)));
                else
                    s1.Points.Add(new DataPoint(running_month, 0));

                if (running_month == 10)
                    running_month_str = "";

                running_month++;
            }
            model.Series.Add(s1);

            var s2 = new LineSeries()
            {
                Title = "Annual Income current year",
                Color = OxyColors.Teal,
                MarkerType = MarkerType.Diamond,
                MarkerSize = 6,
                MarkerStroke = OxyColors.White,
                MarkerFill = OxyColors.Teal,
                MarkerStrokeThickness = 1.5
            };
            running_month = 1;
            year_str = DateTime.Today.ToString("yyyy");
            s2.Points.Add(new DataPoint(0, 0));
            foreach (MonthlyPerformance item in current_year)
            {
                if (running_month < 10)
                    running_month_str = "0" + running_month.ToString();
                else
                    running_month_str = running_month.ToString();

                running_month_str = year_str + running_month_str;

                if (item.YEAR_MONTH.ToString() == running_month_str)
                    s2.Points.Add(new DataPoint(running_month, (item.TOTAL_PREMIUM - item.TOTAL_REFUND)));
                else
                    s2.Points.Add(new DataPoint(running_month, 0));

                if (running_month == 10)
                    running_month_str = "";

                running_month++;
            }
            model.Series.Add(s2);
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, IsPanEnabled = false, IsZoomEnabled = false });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, IsPanEnabled = false, IsZoomEnabled = false });
            return model;

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
