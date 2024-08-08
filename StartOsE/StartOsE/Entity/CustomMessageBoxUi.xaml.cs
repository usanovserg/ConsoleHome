using StartOsE.Language;
using System.Windows;

namespace StartOsE.Entity
{
    /// <summary>
    /// Interaction logic for CustomMessageBoxUi.xaml
    /// </summary>
    public partial class CustomMessageBoxUi : Window
    {
        public CustomMessageBoxUi(string text)
        {
            InitializeComponent();

            //StartOsE.Layout.StickyBorders.Listen(this);
            //StartOsE.Layout.StartupLocation.Start_MouseInCentre(this);
            TextBoxMessage.Text = text;

            ButtonAccept.Content = OsLocalization.Entity.ButtonAccept;

            this.Activate();
            this.Focus();
        }

        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
