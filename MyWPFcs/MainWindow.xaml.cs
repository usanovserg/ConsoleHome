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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyWPFcs
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _tabItem2.Header = "TabItem";
            Content = _grid;
            _stackPanel.Children.Add(_textBlock);
            _stackPanel.Children.Add(_button);
            _stackPanel.Children.Add(_textBox);
            

            _tabItem1.Content = _stackPanel;

            _tabControl.Items.Add(_tabItem1);
            _tabControl.Items.Add(_tabItem2);

            _grid.Children.Add(_tabControl);

        }



        Grid _grid= new Grid();
        
        TabControl _tabControl =new TabControl();

        TabItem _tabItem1=new TabItem()
        {
            Header ="MyTab"
        };
        TabItem _tabItem2=new TabItem();

        TextBlock _textBlock=new TextBlock()
        {
            Text="MyTextBlock"
        };
        Button _button=new Button()
        {
            Content="MyButton",
            Margin = new Thickness(10)

        };
        TextBox _textBox = new TextBox()
        {
            Text="MyTextBox"
        };
        StackPanel _stackPanel = new StackPanel()
        {
            Orientation = Orientation.Vertical,
            Margin = new Thickness(10, 30, 10, 100),
            Height = 90,
            Width = 100,
            Background = Brushes.Red,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top
            
        };

    }
}
