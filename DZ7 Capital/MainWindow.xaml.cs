using Capital.Entity;
using Capital.Enums;
using System.Collections.ObjectModel;
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

            _dataGrid.DataContext = renderData;//Теперь DataGrid автоматически обновляется при изменении renderData
        }

        #region Filds ======================================================
        /// <summary>
        /// Коллекция ObservableCollection для динамического отображения данных в графических интерфейсах
        /// </summary>
        private ObservableCollection<Data> renderData = new ObservableCollection<Data>();

        List<StrategyType> _strategies = new List<StrategyType>()
        {
             StrategyType.FIX,
             StrategyType.CAPITALIZATION,
             StrategyType.PROGRESS,
             StrategyType.DOWNGRADE
        };

        Random _random = new Random();
        private List<Data> _calculatedDatas = new List<Data>(); //поле _calculatedDatas для хранения вычисленных
                                                                //данных между вызовами метода

        #endregion

        #region Methods ======================================================

        /// <summary>
        /// заполняет ComboBox стратегиями и устанавливает значения по умолчанию
        /// </summary>
        private void Init()
        {
            _comboBox.ItemsSource = _strategies;
            _comboBox.SelectionChanged += Redraw;
            _comboBox.SelectedIndex = 0;

            this.SizeChanged += Redraw;

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
        /// запускает расчет при нажатии кнопки (обработка событий)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _calculatedDatas = Calculate();
            Redraw();
        }

        private bool HasData() {
            return _calculatedDatas != null && _calculatedDatas.Any();
        }

        private void Redraw(object sender, EventArgs e)
        {
            Redraw();
        }

        private void Redraw() 
        {
            if (HasData())
            {
                Draw(getCurrentData());
            }
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
                // Добавляем текущее состояние депозита для каждой стратегии
                foreach (var data in datas)
                {
                    data.AddEquityPoint(data.ResultDepo);
                }


            }

            renderData.Clear(); // Очистка старых данных в коллекции ObservableCollection

            foreach (var data in datas)
            {
                renderData.Add(data); // Добавление новых данных в коллекцию ObservableCollection
            }
           
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

        private void Draw(Data data) {
            _canvas.Children.Clear();
            List<decimal> listEquity = data.GetListEquity();

            int count = listEquity.Count;
            decimal maxEquity = listEquity.Max();
            decimal minEquity = listEquity.Min();

            double stepX = _canvas.ActualWidth / count;
            double koef = (double)(maxEquity - minEquity) / _canvas.ActualHeight;

            // Визуальные параметры графика
            Polyline polyline = new Polyline()
            {
                Stroke = data.LineColor,
                StrokeThickness = 2
            };

            PointCollection points = new PointCollection();

            double x = 0;
            for (int i = 0; i < count; i++)
            {
                double y = _canvas.ActualHeight - (double)(listEquity[i] - minEquity) / koef;
                points.Add(new Point(x, y));
                x += stepX;
            }

            polyline.Points = points;
            _canvas.Children.Add(polyline);
        }

        private Data getCurrentData() 
        {
            int index = _comboBox.SelectedIndex;
            return _calculatedDatas[index];
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