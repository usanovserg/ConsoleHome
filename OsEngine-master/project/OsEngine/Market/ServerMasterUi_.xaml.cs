///*
// *Your rights to use the code are governed by this license https://github.com/AlexWan/OsEngine/blob/master/LICENSE
// *Ваши права на использование кода регулируются данной лицензией http://o-s-a.net/doc/license_simple_engine.pdf
//*/

//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Windows.Forms.VisualStyles;
//using MahApps.Metro.Controls;
//using OsEngine.Language;
//using OsEngine.Market.Servers;
//using OsEngine.Market.SupportTable;
//using OsEngine.ViewModels;

//namespace OsEngine.Market
//{
//    /// <summary>
//    /// interaction logic for ServerPrimeUi.xaml
//    /// Логика взаимодействия для ServerPrimeUi.xaml
//    /// </summary>
//    public partial class ServerMasterUi: MetroWindow
//    {
//        /// <summary>
//        /// constructor
//        /// конструктор
//        /// </summary>
//        /// <param name="isTester">shows whether the method is called from the tester / вызывается ли метод из тестера </param>
//        public ServerMasterUi(bool isTester)
//        {
//            InitializeComponent();
//            Layout.StickyBorders.Listen(this);
//            Layout.StartupLocation.Start_MouseInCentre(this);

//            //List<IServer> servers = ServerMaster.GetServers();
            
//            Title = OsLocalization.Market.TitleServerMasterUi;
//            //TabItem1.Header = OsLocalization.Market.TabItem1;
//            //TabItem2.Header = OsLocalization.Market.TabItem2;
//            //CheckBoxServerAutoOpen.Content = OsLocalization.Market.Label20;
//            //ButtonSupportTable.Content = OsLocalization.Market.Label81;

//            //ServerMasterSourcesPainter painter = new ServerMasterSourcesPainter(HostSource, HostLog, CheckBoxServerAutoOpen);

//            //Closing += delegate (object sender, CancelEventArgs args)
//            //{
//            //    painter.Dispose();
//            //    painter = null;
//            //};

//            vm = new MyRobotsVM(this);
//            DataContext = vm; 

//            this.Activate();
//            this.Focus();
//        }

//        private MyRobotsVM vm;

//        private void ButtonSupportTable_Click(object sender, System.Windows.RoutedEventArgs e)
//        {
//            SupportTableUi supportTableUi = new SupportTableUi();
//            supportTableUi.ShowDialog();
//        }
//    }
//}
