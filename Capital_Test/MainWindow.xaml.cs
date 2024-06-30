using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Capital_Test.Enums;
using Capital_Test.ViewModels;
using ScottPlot.Plottables;
using Color = System.Drawing.Color;
// ReSharper disable SuggestVarOrType_SimpleTypes

namespace Capital_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = new VM();
        }

        //private int _indexCountCalc = 0;
        //private readonly List<Scatter> _plot = [];
        //private int _indexCombo;



        //private void _comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ComboBox? comboBox = sender as ComboBox;
        //    _indexCombo = comboBox.SelectedIndex;

        //    if (_indexCountCalc > 0)
        //    {
        //        PlotCalc(_indexCombo, true);
        //        //CheckBoxState(this, null);
        //    }
        //}

        //private void CB_Checked(object sender, RoutedEventArgs e)
        //{
        //    if (_indexCountCalc > 0)
        //    {
        //        IsCheckBoxesChecked();
        //    }
        //}

        //private void PlotCalc(int index, bool clear)
        //{
        //    var t1 = _plot[index].Data.GetScatterPoints();
        //    double[] dataX1 = t1.Select(x => x.X).ToArray();
        //    double[] dataY1 = t1.Select(x => x.Y).ToArray();

        //    ScottPlot.Color color = default;

        //    switch (index)
        //    {
        //        case 0:
        //            color = ScottPlot.Color.FromColor(Color.CornflowerBlue);
        //            break;
        //        case 1:
        //            color = ScottPlot.Color.FromColor(Color.DarkRed);
        //            break;
        //        case 2:
        //            color = ScottPlot.Color.FromColor(Color.DarkGreen);
        //            break;
        //        case 3:
        //            color = ScottPlot.Color.FromColor(Color.Orange);
        //            break;
        //    }

        //    if (clear)
        //    {
        //        //WpfPlot1.Plot.Clear();
        //    }

            //WpfPlot1.Plot.Add.ScatterLine(dataX1, dataY1, color);
            //WpfPlot1.Plot.Axes.AutoScale();
            //WpfPlot1.Refresh();
        //}

        //private void IsCheckBoxesChecked()
        //{
        //    //WpfPlot1.Plot.Clear();

        //    CheckBox[] checkBoxArray = [CB_Fix, CB_Cap, CB_Prog, CB_Dawn];

        //    for (int i = 0; i < checkBoxArray.Length; i++)
        //    {
        //        if ((bool)checkBoxArray[i].IsChecked!)
        //        {
        //            PlotCalc(i, false);
        //        }
        //    }
        //}

    }
}