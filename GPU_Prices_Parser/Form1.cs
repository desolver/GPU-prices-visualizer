using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;

namespace GPU_Prices_Parser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            var pm = new PlotModel
            {
                Title = "Trigonometric functions",
                Subtitle = "Example using the FunctionSeries",
                PlotType = PlotType.Cartesian,
                Background = OxyColors.White
            };
            var ls = new LineSeries();
            ls.Points.Add(new DataPoint(5, 5));
            ls.Points.Add(new DataPoint(10, 10));
            ls.Points.Add(new DataPoint(20, 50));
            pm.Series.Add(ls);
            plotView.Model = pm;
        }

    }
}
