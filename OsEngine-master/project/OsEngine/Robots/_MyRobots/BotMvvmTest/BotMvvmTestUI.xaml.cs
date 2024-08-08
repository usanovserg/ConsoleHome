using System;
using System.Windows;
using OsEngine.OsTrader.Panels.Tab;
using OsEngine.Robots._MyRobots.BotMvvmTest.ViewModels;

namespace OsEngine.Robots._MyRobots.BotMvvmTest
{
    /// <summary>
    /// Логика взаимодействия для BotMvvmTestUI.xaml
    /// </summary>
    public partial class BotMvvmTestUI : Window
    {
        public BotMvvmTestUI(BotMvvmTest bot)
        {
            InitializeComponent();

            vm = new Vm(bot);
            DataContext = vm;
        }                    
        private Vm vm;
    }   
}
