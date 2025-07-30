using Capital.Entity;
using Capital.Enums;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace Capital
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Init();
        }

        #region Filds ======================================================

        List<StrategyType> _strategies = new List<StrategyType>()
        {
             StrategyType.FIX,
             StrategyType.CAPITALIZATION,
             StrategyType.PROGRESS,
             StrategyType.DOWNGRADE
        };

        Random _random = new Random();

        #endregion

        #region Methods ======================================================

        /// <summary>
        /// заполняет ComboBox стратегиями и устанавливает значения по умолчанию
        /// </summary>
        private void Init()
        {
            _comboBox.ItemsSource = _strategies;

            _comboBox.SelectionChanged += _comboBox_SelectionChanged;
            _comboBox.SelectedIndex = 0;

            _depo.Text = "100000";
            _startLot.Text = "10";
            _take.Text = "300";
            _stop.Text = "100";
            _comiss.Text = "5";
            _countTrades.Text = "1000";
            _percentProfit.Text = "30";
            _go.Text = "5000";
            _minStartPercent.Text = "20";
        }

        /// <summary>
        /// реагирует на выбор стратегии (обработка событий)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            int index = comboBox.SelectedIndex;
        }

        /// <summary>
        /// запускает расчет при нажатии кнопки (обработка событий)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Data> datas = Calculate();

            Draw(datas);
        }

        private List<Data> Calculate()
        {
            //Получение параметров из полей ввода
            decimal depoStart = GetDecimalFromString(_depo.Text); // Начальный депозит
            int startLot = GetIntFromString(_startLot.Text); // Стартовый размер лота
            decimal take = GetDecimalFromString(_take.Text); // Размер тейк-профита
            decimal stop = GetDecimalFromString(_stop.Text); // Размер стоп-лосса
            decimal comiss = GetDecimalFromString(_comiss.Text); // Комиссия за сделку
            int countTrades = GetIntFromString(_countTrades.Text); // Количество сделок
            decimal percProfit = GetDecimalFromString(_percentProfit.Text); // % прибыльных сделок
            decimal minStartPercent = GetDecimalFromString(_minStartPercent.Text); // Мин. % депозита
            decimal go = GetDecimalFromString(_go.Text); // Гарантийное обеспечение

            //Создание списка данных datas для хранения результатов по каждой стратегии
            List<Data> datas = new List<Data>();

            foreach (StrategyType type in _strategies)
            {
                datas.Add(new Data(depoStart, type));
            }

            //* Подготовка переменных для стратегий *//
            int lotPercent = startLot; // Текущий лот для CAPITALIZATION
            decimal percent = startLot * go * 100 / depoStart; // Процент риска
            decimal multiply = take / stop; // Мультипликатор (например: 300/100 = 3)
            int lotProgress = CalculateLot(depoStart, minStartPercent, go); // Лот для PROGRESS
            int lotDown = startLot; // Лот для DOWNGRADE

            for (int i = 0; i < countTrades; i++)
            {
                int rnd = _random.Next(1, 100);

                if (rnd <= percProfit)
                {
                    // Сделка прибыльная

                    //=========== 1 strategy ==============================================
                    datas[0].ResultDepo += (take - comiss) * startLot;

                    //=========== 2 strategy ==============================================

                    datas[1].ResultDepo += (take - comiss) * lotPercent;

                    int newLot = CalculateLot(datas[1].ResultDepo, percent, go);

                    if (lotPercent < newLot) lotPercent = newLot;

                    //=========== з strategy ==================================

                    datas[2].ResultDepo += (take - comiss) * lotProgress;

                    lotProgress = CalculateLot(depoStart, minStartPercent * multiply, go);

                    //4 strategy =============================================

                    datas[3].ResultDepo += (take - comiss) * lotDown;

                    lotDown = startLot;
                }
                else
                {
                    // Сделка убыточная
                    //=========== i strategy ============================
                    datas[0].ResultDepo -= (stop + comiss) * startLot;

                    //=========== 2 strategy ============================

                    datas[1].ResultDepo -= (stop + comiss) * lotPercent;

                    //=========== 3 Strategy ============================

                    datas[2].ResultDepo -= (stop + comiss) * lotProgress;

                    lotProgress = CalculateLot(depoStart, minStartPercent, go);

                    //=========== 4 strategy =====================================

                    datas[3].ResultDepo -= (stop + comiss) * lotDown;

                    lotDown /= 2;

                    if (lotDown == 0) lotDown = 1;
                }

            }
            _dataGrid.ItemsSource = datas;

            return datas;
        }

        /// <summary>
        /// вычисление размера лота
        /// </summary>
        /// <param name="currentDepo"></param>
        /// <param name="percent"></param>
        /// <param name="go"></param>
        /// <returns></returns>
        private int CalculateLot(decimal currentDepo, decimal percent, decimal go) //размер лота по формуле: (депозит / ГО) * (процент / 100)
        {
            if (percent > 100) { percent = 100; }

            decimal lot = currentDepo / go / 100 * percent;

            return (int)lot;
        }

        private void Draw(List<Data> datas)
        {
            _canvas.Children.Clear();

            int index = _comboBox.SelectedIndex;

            List<decimal> listEquity = datas[index].GetListEquity();

            int count = listEquity.Count;
            decimal maxEquity = listEquity.Max();
            decimal minEquity = listEquity.Min();

            double stepX = _canvas.ActualWidth / count;
            double koef = (double)(maxEquity - minEquity) / _canvas.ActualHeight;

            double x = 0;
            double y = 0;

            for (int i = 0; i < count; i++)
            {
                y = _canvas.ActualHeight - (double)(listEquity[i] - minEquity) / koef;

                Ellipse ellipse = new Ellipse()
                {
                    Width = 2,
                    Height = 2,
                    Stroke = Brushes.Black
                };

                Canvas.SetLeft(ellipse, x);
                Canvas.SetTop(ellipse, y);

                _canvas.Children.Add(ellipse);
                
                x += stepX;
            }
        }

        /// <summary>
        /// безопасное преобразование строк в числа decimal
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private decimal GetDecimalFromString(string str)
        {
            if (decimal.TryParse(str, out decimal result)) return result;

            return 0;
        }

        /// <summary>
        /// безопасное преобразование строк в числа int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private int GetIntFromString(string str)
        {
            if (int.TryParse(str, out int result)) return result;

            return 0;
        }

        #endregion
    }
}