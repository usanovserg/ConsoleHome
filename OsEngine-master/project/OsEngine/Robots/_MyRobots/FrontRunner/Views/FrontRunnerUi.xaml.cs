using System.Windows;
using OsEngine.Robots._MyRobots.FrontRunner.Models;
using OsEngine.Robots._MyRobots.FrontRunner.ViewModels;

namespace OsEngine.Robots._MyRobots.FrontRunner.Views
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class FrontRunnerUi : Window
    {
        public FrontRunnerUi(FrontRunnerBot frontRunnerBot)
        { 
            InitializeComponent(); 
            DataContext = new Vm(frontRunnerBot);
        }

       // private Vm vm;
    }
}
