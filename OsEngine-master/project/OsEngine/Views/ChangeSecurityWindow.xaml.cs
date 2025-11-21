using MahApps.Metro.Controls;
using OsEngine.ViewModels;

namespace OsEngine.Views
{
    /// <summary>
    /// Логика взаимодействия для ChangeSecurityWindow.xaml
    /// </summary>
    public partial class ChangeSecurityWindow : MetroWindow
    {
        public ChangeSecurityWindow(Robot robot)
        {
            InitializeComponent();

            DataContext = new ChangeSecurityVM(robot);
        }
    }
}   