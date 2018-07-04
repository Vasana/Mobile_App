using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.ViewModels
{
    public class BranchEvaluation
    {
        public PlotModel PieModel { get; set; }
        public PlotModel AreaModelNoPol { get; set; }
        public PlotModel AreaModelAnnualInc { get; set; }
        public PlotModel BarModel { get; set; }
        public PlotModel StackedBarModel { get; set; }
        public PlotModel LineChart { get; set; }

        public BranchEvaluation()
        {
            PieModel = CreatePieChart();
            AreaModelNoPol = AreaChart_NoOfPolicies();
            StackedBarModel = CreateBarChart(true, "Stacked Bar Chart");
            BarModel = CreateBarChart(false, "Comparison Bar Chart");
            LineChart = TwoLineSeries();
            AreaModelAnnualInc = TwoLineSeries();

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

            // http://www.nationsonline.org/oneworld/world_population.htm
            // http://en.wikipedia.org/wiki/Continent
            ps.Slices.Add(new PieSlice("Africa", 1030) { IsExploded = false });
            ps.Slices.Add(new PieSlice("Americas", 929) { IsExploded = false });
            ps.Slices.Add(new PieSlice("Asia", 4157));
            ps.Slices.Add(new PieSlice("Europe", 739) { IsExploded = false });
            ps.Slices.Add(new PieSlice("Oceania", 35) { IsExploded = false });
            model.Series.Add(ps);
            return model;
        }

        public PlotModel AreaChart_NoOfPolicies()
        {
            var plotModel1 = new PlotModel { Title = "Number of Policies" };
            //var areaSeries1 = new AreaSeries();
            //areaSeries1.Points.Add(new DataPoint(1, 50));
            //areaSeries1.Points.Add(new DataPoint(2, 140));
            //areaSeries1.Points.Add(new DataPoint(3, 60));
            //areaSeries1.Points2.Add(new DataPoint(4, 60));
            //areaSeries1.Points2.Add(new DataPoint(5, 80));
            //areaSeries1.Points2.Add(new DataPoint(6, 70));
            //plotModel1.Series.Add(areaSeries1);
            //return plotModel1;
            var s1 = new AreaSeries()
            {
                Title = "Branch Avg. number of policies",
                Color = OxyColors.LightPink,
                MarkerType = MarkerType.Circle,
                MarkerSize = 6,
                MarkerStroke = OxyColors.White,
                MarkerFill = OxyColors.LightPink,
                MarkerStrokeThickness = 1.5
            };


            s1.Points.Add(new DataPoint(0, 0));
            s1.Points.Add(new DataPoint(1, 10));
            s1.Points.Add(new DataPoint(2, 22));
            s1.Points.Add(new DataPoint(3, 24));
            s1.Points.Add(new DataPoint(4, 10));
            s1.Points.Add(new DataPoint(5, 25));
            s1.Points.Add(new DataPoint(6, 12));
            s1.Points.Add(new DataPoint(7, 2));
            plotModel1.Series.Add(s1);

            var s2 = new AreaSeries()
            {
                Title = "Your Number of policies",
                Color = OxyColors.Teal,
                MarkerType = MarkerType.Diamond,
                MarkerSize = 6,
                MarkerStroke = OxyColors.White,
                MarkerFill = OxyColors.Teal,
                MarkerStrokeThickness = 1.5
            };
            s2.Points.Add(new DataPoint(0, 0));
            s2.Points.Add(new DataPoint(1, 4));
            s2.Points.Add(new DataPoint(2, 32));
            s2.Points.Add(new DataPoint(3, 14));
            s2.Points.Add(new DataPoint(4, 20));
            s2.Points.Add(new DataPoint(5, 15));
            s2.Points.Add(new DataPoint(6, 22));
            s2.Points.Add(new DataPoint(7, 1));
            plotModel1.Series.Add(s2);

            var s3 = new AreaSeries()
            {
                Title = "Branch Best Number of policies",
                Color = OxyColors.Orange,
                MarkerType = MarkerType.Diamond,
                MarkerSize = 6,
                MarkerStroke = OxyColors.White,
                MarkerFill = OxyColors.Orange,
                MarkerStrokeThickness = 1.5
            };
            s3.Points.Add(new DataPoint(0, 0));
            s3.Points.Add(new DataPoint(1, 27));
            s3.Points.Add(new DataPoint(2, 42));
            s3.Points.Add(new DataPoint(3, 28));
            s3.Points.Add(new DataPoint(4, 30));
            s3.Points.Add(new DataPoint(5, 30));
            s3.Points.Add(new DataPoint(6, 27));
            s3.Points.Add(new DataPoint(7, 4));
            plotModel1.Series.Add(s3);
            plotModel1.Axes.Add(new LinearAxis { Position = AxisPosition.Left, IsPanEnabled = false, IsZoomEnabled = false });
            plotModel1.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, IsPanEnabled = false, IsZoomEnabled = false });
            return plotModel1;

        }

        public PlotModel TwoLineSeries()
        {
            var model = new PlotModel { Title = "Annual Premium Income" };
            //model.Axes.Add(new LinearAxis(AxisPosition.Left, -1, 71, "Y-Axis"));
            //model.Axes.Add(new LinearAxis(AxisPosition.Bottom, -1, 61, "X-Axis"));

            // model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = -1, Maximum = 12 });
            // model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = -1, Maximum = 71 });

            var s1 = new LineSeries()
            {
                Title = "Branch Avg Premium Income",
                Color = OxyColors.SkyBlue,
                MarkerType = MarkerType.Circle,
                MarkerSize = 6,
                MarkerStroke = OxyColors.White,
                MarkerFill = OxyColors.SkyBlue,
                MarkerStrokeThickness = 1.5
            };
            s1.Points.Add(new DataPoint(0, 15000));
            s1.Points.Add(new DataPoint(1, 34000));
            s1.Points.Add(new DataPoint(2, 42000));
            s1.Points.Add(new DataPoint(3, 160000));
            s1.Points.Add(new DataPoint(4, 56000));
            s1.Points.Add(new DataPoint(5, 15000));
            s1.Points.Add(new DataPoint(6, 112000));
            s1.Points.Add(new DataPoint(7, 12000));
            model.Series.Add(s1);

            var s2 = new LineSeries()
            {
                Title = "Your Premium Income",
                Color = OxyColors.Teal,
                MarkerType = MarkerType.Diamond,
                MarkerSize = 6,
                MarkerStroke = OxyColors.White,
                MarkerFill = OxyColors.Teal,
                MarkerStrokeThickness = 1.5
            };
            s2.Points.Add(new DataPoint(0, 55000));
            s2.Points.Add(new DataPoint(1, 64000));
            s2.Points.Add(new DataPoint(2, 132000));
            s2.Points.Add(new DataPoint(3, 140000));
            s2.Points.Add(new DataPoint(4, 52000));
            s2.Points.Add(new DataPoint(5, 15000));
            s2.Points.Add(new DataPoint(6, 122000));
            s2.Points.Add(new DataPoint(7, 10000));
            model.Series.Add(s2);


            var s3 = new LineSeries()
            {
                Title = "Branch Best Premium Income",
                Color = OxyColors.Orange,
                MarkerType = MarkerType.Diamond,
                MarkerSize = 6,
                MarkerStroke = OxyColors.White,
                MarkerFill = OxyColors.Orange,
                MarkerStrokeThickness = 1.5
            };
            s3.Points.Add(new DataPoint(0, 16000));
            s3.Points.Add(new DataPoint(1, 84000));
            s3.Points.Add(new DataPoint(2, 152000));
            s3.Points.Add(new DataPoint(3, 160000));
            s3.Points.Add(new DataPoint(4, 55000));
            s3.Points.Add(new DataPoint(5, 25000));
            s3.Points.Add(new DataPoint(6, 132000));
            s3.Points.Add(new DataPoint(7, 20000));
            model.Series.Add(s3);
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, IsPanEnabled = false, IsZoomEnabled = false });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, IsPanEnabled = false, IsZoomEnabled = false });
            return model;
        }

        private PlotModel CreateBarChart(bool stacked, string title)
        {
            var model = new PlotModel
            {
                Title = title,
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0
            };

            var s1 = new BarSeries { Title = "Series 1", IsStacked = stacked, StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            s1.Items.Add(new BarItem { Value = 25 });
            s1.Items.Add(new BarItem { Value = 137 });
            s1.Items.Add(new BarItem { Value = 18 });
            s1.Items.Add(new BarItem { Value = 40 });

            var s2 = new BarSeries { Title = "Series 2", IsStacked = stacked, StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            s2.Items.Add(new BarItem { Value = 12 });
            s2.Items.Add(new BarItem { Value = 14 });
            s2.Items.Add(new BarItem { Value = 120 });
            s2.Items.Add(new BarItem { Value = 26 });

            var categoryAxis = new CategoryAxis { Position = CategoryAxisPosition() };
            categoryAxis.Labels.Add("Category A");
            categoryAxis.Labels.Add("Category B");
            categoryAxis.Labels.Add("Category C");
            categoryAxis.Labels.Add("Category D");
            var valueAxis = new LinearAxis { Position = ValueAxisPosition(), MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };
            model.Series.Add(s1);
            model.Series.Add(s2);
            model.Axes.Add(categoryAxis);
            model.Axes.Add(valueAxis);
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
