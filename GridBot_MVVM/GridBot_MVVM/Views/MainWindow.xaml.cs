using GridBotMVVM.ViewModels;
using System.Windows;
using GridBotMVVM.Utils;

namespace GridBotMVVM.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            vm = new Vm();
            DataContext = vm;
            //DataContext = em;
        }
        private Vm vm;
        //private Emulator em;
    }
}