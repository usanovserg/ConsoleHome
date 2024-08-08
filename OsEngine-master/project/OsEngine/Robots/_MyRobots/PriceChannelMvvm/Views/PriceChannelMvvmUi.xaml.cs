using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OsEngine.Robots._MyRobots.PriceChannelMvvm.Models;
using OsEngine.Robots._MyRobots.PriceChannelMvvm.ViewModels;

namespace OsEngine.Robots._MyRobots.PriceChannelMvvm.Views
{ 
    public partial class PriceChannelMvvmUi : Window
    {
        public PriceChannelMvvmUi(Models.PriceChannelMvvm priceChannelMvvm)
        {
            InitializeComponent();
            DataContext = new Vm(priceChannelMvvm);
        }
    }
}
