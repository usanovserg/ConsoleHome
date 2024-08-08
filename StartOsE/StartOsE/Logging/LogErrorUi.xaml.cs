/*
 *Your rights to use the code are governed by this license https://github.com/AlexWan/StartOsE/blob/master/LICENSE
 *Ваши права на использование кода регулируются данной лицензией http://o-s-a.net/doc/license_simple_engine.pdf
*/

using System.Windows.Forms;
using StartOsE.Language;

namespace StartOsE.Logging
{
    /// <summary>
    /// Interaction logic for LogErrorUi.xaml
    /// Логика взаимодействия для LogErrorUi.xaml
    /// </summary>
    public partial class LogErrorUi
    {
        public LogErrorUi(DataGridView gridErrorLog)
        {
            InitializeComponent();
            StartOsE.Layout.StickyBorders.Listen(this);
            HostLog.Child = gridErrorLog;
            Title = OsLocalization.Logging.TitleExtraLog;

            this.Activate();
            this.Focus();
        }
    }
}
