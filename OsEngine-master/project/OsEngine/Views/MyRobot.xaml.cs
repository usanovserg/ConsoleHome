using MahApps.Metro.Controls;
using OsEngine.ViewModels;

namespace OsEngine.Views
{
    /// <summary>
    /// Логика взаимодействия для MyRobot.xaml
    /// </summary>
    public partial class MyRobot : MetroWindow
    {
        public MyRobot()
        {
            InitializeComponent();

            _vm = new MyRobotVM(this);

            DataContext = _vm;
        }

        MyRobotVM _vm;
    }
}
