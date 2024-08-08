using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using StartOsE.Market.Servers;
using StartOsE.Language;
using StartOsE.Market.SupportTable;

namespace StartOsE.Market;

public partial class ServerMasterUi
{
    /// <summary>
    /// constructor
    /// конструктор
    /// </summary>
    /// <param name="isTester">shows whether the method is called from the tester / вызывается ли метод из тестера </param>
    public ServerMasterUi(bool isTester)
    {
        InitializeComponent();
        StartOsE.Layout.StickyBorders.Listen(this);
        StartOsE.Layout.StartupLocation.Start_MouseInCentre(this);

        List<IServer> servers = ServerMaster.GetServers();

        if (isTester)
        {
            servers = ServerMaster.GetServers();

            if (servers == null ||
                servers.Find(s => s.ServerType == ServerType.Tester) == null)
            {
                ServerMaster.CreateServer(ServerType.Tester, false);
            }

            Close();

            servers = ServerMaster.GetServers();
            servers.Find(s => s.ServerType == ServerType.Tester).ShowDialog();
            return;
        }

        Title = OsLocalization.Market.TitleServerMasterUi;
        TabItem1.Header = OsLocalization.Market.TabItem1;
        TabItem2.Header = OsLocalization.Market.TabItem2;
        CheckBoxServerAutoOpen.Content = OsLocalization.Market.Label20;
        ButtonSupportTable.Content = OsLocalization.Market.Label81;

        ServerMasterSourcesPainter painter = new ServerMasterSourcesPainter(HostSource, HostLog, CheckBoxServerAutoOpen);

        Closing += delegate (object sender, CancelEventArgs args)
        {
            painter.Dispose();
            painter = null;
        };

        this.Activate();
        this.Focus();
    }

    private void ButtonSupportTable_Click(object sender,RoutedEventArgs e)
    {
        SupportTableUi supportTableUi = new SupportTableUi();
        supportTableUi.ShowDialog();
    }
}
