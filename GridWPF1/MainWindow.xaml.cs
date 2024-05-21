using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection.Emit;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WinRT;
using static System.Net.Mime.MediaTypeNames;

//using Timer = System.Timers.Timer;

namespace GridWPF1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadParams();
            Tb_StartPrice.Text = _startPrice.ToString();
            Tb_StepLevel.Text = _stepLevel.ToString();
            Tb_CountLevels.Text = _countLevels.ToString();
            Tb_Profit.Text = _profit.ToString();
            Tb_Volume.Text= _volume.ToString();
        }


        public decimal Volatility { get; set; }

        //====================== Поля ========================================================

        public List<Level> Levels = [];
        public List<Trade> ExChageTrades = [];
        public List<Trade> Trades = [];
        public List<Level> BuyList = [];
        public List<Level> CloseList = [];

        public List<Trade> BuyListTemp = [];
        public List<Trade> SellListTemp = [];

        private Position _pos = new Position();

        private decimal _startPrice;
        private decimal _priceLevel;
        private decimal _stepLevel;
        private decimal _priceEnd;
        private int _countLevels;
        private decimal _volume;
        private decimal _profit;
        private Side _side;

        private static Connector _connector = new();

        private DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        //private int _timerInterval = 1;

        //private Trade tradeClose;

        //public Trade trade;


        //====================== Метод "Построить сетку" =========================================
        #region Метод построения сетки

        public void StartLevels()
        {
            try
            {
                _startPrice = Convert.ToDecimal(PointChanged(Tb_StartPrice.Text));
                _priceLevel = _startPrice;
                _stepLevel = Convert.ToDecimal(PointChanged(Tb_StepLevel.Text));
                _priceEnd = Convert.ToDecimal(PointChanged(Tb_EndLevel.Text));
                _countLevels = Convert.ToInt32(PointChanged(Tb_CountLevels.Text));
                _volume = Convert.ToDecimal(PointChanged(Tb_Volume.Text));
                _profit = Convert.ToDecimal(PointChanged(Tb_Profit.Text));

                if ((bool) CB_Side.IsChecked!)
                {
                    _side = Side.Buy;
                }
                else
                {
                    _side = Side.Sell;
                }

                //Level Level = new Level();
                //_side = new Side();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            try
            {
                //----------------------------------Если Последний уровень 0----------------------------

                if (Tb_EndLevel.Text == "0")
                {
                    if (!(bool)CB_Side.IsChecked!)
                    {
                        _stepLevel *= -1;
                    }

                    Levels = Level.CalcLevels(_priceLevel, _stepLevel, _countLevels-1, _profit, _volume, _side);

                    // Выводим Последний уровень
                    Tb_CountLevels.Text = Levels.Count.ToString();
                    if (Levels.Count > 0)
                    {
                        if ((bool)CB_Side.IsChecked!)
                        {
                            Tb_EndLevel.Text = Levels[^1].PriceEnter.ToString();
                        }
                        else
                        {
                            Tb_EndLevel.Text = Levels[0].PriceEnter.ToString();
                        }
                    }
                }

                //----------------------------------Если шаг цены 0------------------------------------

                else if (Tb_StepLevel?.Text == "0")
                {
                    var n = GetDecimalDigitsCount(_startPrice);

                    // Расчитываем шаг цены

                    if (_countLevels > 1)
                    {
                        _stepLevel = n > 0 ? Math.Round(((_startPrice - _priceEnd) / (_countLevels - 1)), n) :
                            Math.Floor(((_startPrice - _priceEnd) / (_countLevels - 1)));
                    }
                    else
                    {
                        _stepLevel = 0;
                    }

                    Levels = Level.CalcLevels(_priceLevel, _stepLevel, _countLevels-1, _profit, _volume, _side);

                    // Выводим Шаг цены
                    Tb_StepLevel.Text = Math.Abs(_stepLevel).ToString();
                }

                //----------------------------------Если Количество уровней 0---------------------------------

                else if (Tb_CountLevels.Text == "0")
                {
                    _countLevels = Math.Abs(Convert.ToInt32(Math.Floor((_startPrice - _priceEnd) / _stepLevel)));

                    if (!(bool)CB_Side.IsChecked!)
                    {
                        _stepLevel *= -1;
                    }

                    Levels = Level.CalcLevels(_priceLevel, _stepLevel, _countLevels, _profit, _volume, _side);

                    // Выводим Количество уровней
                    Tb_CountLevels.Text = (_countLevels+1).ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            // Запись параметров в файл
            SaveParams(_priceLevel, _stepLevel, _countLevels, _profit, _volume, _side);
        }

        #endregion
        
        //===================== Вывод уровней в текстовый блок ========================================

        public void LevelsOut(List<Level> levels, TextBox textbox)
        {
            for (var i = 0; i < levels.Count; i++)
            {
                textbox.Text += levels[i].GetSaveStr() + Environment.NewLine;

                //if (i == levels.Count)
                //{
                //    textbox. = FontWeight.Bold;
                //}
            }
        }

        //====================== Кнопка "Построить сетку" =========================================
        private void Btn_Go_Click(object sender, RoutedEventArgs e)
        {
            TB_Out.Clear();
            TB_Close.Clear();
            Levels.Clear();
            CloseList.Clear();
            StartLevels();
            LevelsOut(Levels, TB_Out);
            SaveLevels();
        }

        //===================== Определение количества знаков после запятой в цене ================

        #region Определение количества знаков после запятой в цене

        private int GetDecimalDigitsCount(decimal number)
        {
            string[] str = Tb_StartPrice.Text.ToString(new NumberFormatInfo()
            { NumberDecimalSeparator = "," }).Split(',');
            return str.Length == 2 ? str[1].Length : 0;
        }
        #endregion
        

        //===================== Заменяем . на , при вводе данных ==================================
        #region Заменяем . на , при вводе данных

        private string PointChanged(string str)
        {
            if (str.Contains("."))
            {
                string s = str.Replace(".", ",");
                return s;
            }

            return str;
        }
        #endregion

        //===================== Запись уровней в файл=================================================
        public void SaveLevels()
        {
            try
            {
                using StreamWriter writer = new("Levels.txt", false);
                for (int i = 0; i < Levels.Count; i++)
                {
                    writer.WriteLine(Levels[i].GetSaveStr());
                }
                writer.Close();
            }
            catch (Exception e)
            {
               
            }
        }

        //===================== Запись параметров в файл=================================================
        public void SaveParams(decimal priceLevel, decimal step, int count, decimal profit, decimal volume, Side side)
        {
            try
            {
                using StreamWriter writer = new("params.txt", false);
              
                writer.WriteLine(priceLevel);
                writer.WriteLine(step);
                writer.WriteLine(count);
                writer.WriteLine(profit);
                writer.WriteLine(volume);
                writer.WriteLine(side);
                
                writer.Close();
            }
            catch (Exception e)
            {
                
            }
        }

        //===================== Загрузка параметров из файла=================================================
        private void LoadParams()
        {
            if (!File.Exists("params.txt"))
            {
                return;
            }

            try
            {
                using StreamReader reader = new("params.txt");

                _startPrice = Convert.ToDecimal(reader.ReadLine());
                _stepLevel = Convert.ToDecimal(reader.ReadLine());
                _countLevels = Convert.ToInt32(reader.ReadLine());
                _profit = Convert.ToDecimal(reader.ReadLine());
                _volume = Convert.ToDecimal(reader.ReadLine());
                Enum.TryParse(reader.ReadLine(), out _side);

                reader.Close();
            }
            catch (Exception e)
            {
                
            }
        }

        //========================Таймер=====================================================================

        private void dispatcherTimer_Tick(object? sender, EventArgs e)
        {
            _connector.Connect(Tb_ExChange);
        }

        //================Кнопка включения генерации котировок==========================================

        private void Btn_OnExChange_Click(object sender, RoutedEventArgs e)
        {
            _dispatcherTimer.Tick += dispatcherTimer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, TimerInterval);
            _dispatcherTimer.Start();

            if ((bool)CB_OnGridRegime.IsChecked!)
            {
                _connector.NewTradeEvent -= Connector_NewTradeEvent;
                _connector.NewTradeEvent += GridTrading;
            }

            else
            {
                _connector.NewTradeEvent -= GridTrading;
                _connector.NewTradeEvent += Connector_NewTradeEvent;
            }
        }

        //================Кнопка выключения генерации котировок==========================================

        private void Btn_OffExChange_Click(object sender, RoutedEventArgs e)
        {
            _dispatcherTimer.Tick -= dispatcherTimer_Tick;
        }

        private int _buyListCountLast;
        private int _sellListCountLast;
        private decimal _lastTradePrice;


        //================Обработка события коннектора==========================================

        private void Connector_NewTradeEvent(Trade trade)
        {
            if (trade.Price == _lastTradePrice)
            {
                return;
            }

            if (trade.Side == Side.Buy)
            {
                BuyListTemp.Add(trade);

                string str = "Volume =" + trade.Volume.ToString() + "  " + "Price = " +
                             trade.Price.ToString() + "\r\n";

                Tb_Buy.AppendText(str);
                Tb_Buy.CaretIndex = Tb_Buy.LineCount;
                Tb_Buy.ScrollToEnd();
                //Tb_Buy.Padding = new Thickness(5);
                _buyListCountLast = BuyList.Count;
                _lastTradePrice = trade.Price;
            }

            else
            {
                SellListTemp.Add(trade);

                string str = "Volume =" + trade.Volume.ToString() + "  " + "Price = " +
                             trade.Price.ToString() + "\r\n";

                Tb_Sell.AppendText(str);
                Tb_Sell.CaretIndex = Tb_Sell.LineCount;
                Tb_Sell.ScrollToEnd();
                _sellListCountLast = CloseList.Count;
                _lastTradePrice = trade.Price;
            }
        }

        //================ Сеточная торговая логика==========================================

        private void GridTrading(Trade trade)
        {
            if (CloseList.Count > 0)
            {
                decimal minCloseLevel = GetCloseLevelsMinLevel(CloseList);

                while (trade.Price > minCloseLevel)
                {
                    Levels.Add(AddLevel());
                    Levels = [.. Levels.OrderByDescending(l => l.PriceEnter)];
                    CloseList.RemoveAt(CloseList.Count-1);
                    CloseList = [.. CloseList.OrderByDescending(l => l.PriceEnter)];
                    minCloseLevel = GetCloseLevelsMinLevel(CloseList);
                }
            }

            if (Levels.Count > 0)
            {
                decimal startLevel = GetLevelsMaxLevel(Levels);
                while (trade.Price < startLevel)
                {
                    CloseList.Add(LevelClose());
                    CloseList = [.. CloseList.OrderByDescending(l => l.PriceEnter)];
                    Levels.RemoveAt(0);
                    Levels = [..Levels.OrderByDescending(l => l.PriceEnter)];
                    startLevel = GetLevelsMaxLevel(Levels);
                } 
            }

            TB_Out.Text = "";
            LevelsOut(Levels, TB_Out);
            TB_Close.Text = "";
            LevelsOut(CloseList, TB_Close);
        }

        //================ Формирование уровня закрытия ==========================================

        public Level LevelClose()
        {
            Level levelClose = new ();

            decimal priceExit = Levels[0].PriceExit;
            levelClose.PriceEnter = priceExit;

            levelClose.PriceExit = levelClose.PriceEnter - Convert.ToDecimal(Tb_Profit.Text);
            levelClose.Volume = Convert.ToDecimal(Tb_Volume.Text);
            levelClose.side = Side.Sell;

            return levelClose;
        }

        //================ Формирование нового уровня ==========================================

        public Level AddLevel()
        {
            Level addLevel = new ();

            addLevel.PriceEnter = GetLevelsMaxLevel(Levels) + Convert.ToDecimal(Tb_StepLevel.Text);
            addLevel.PriceExit = addLevel.PriceEnter + Convert.ToDecimal(Tb_Profit.Text);
            addLevel.Volume = Convert.ToDecimal(Tb_Volume.Text);
            addLevel.side = Side.Buy;

            return addLevel;
        }

        //================ Получение максимального уровня покупки =================================

        private static decimal GetLevelsMaxLevel(List<Level> lines)
        {
            decimal maxLevel = 0;

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].PriceEnter > maxLevel)
                {
                    maxLevel = lines[i].PriceEnter;
                }
            }
            return maxLevel;
        }

        //================ Получение минимального уровня продажи =================================

        private static decimal GetCloseLevelsMinLevel(List<Level> lines)
        {
            decimal minCloseLevel = decimal.MaxValue;

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].PriceEnter < minCloseLevel)
                {
                    minCloseLevel = lines[i].PriceEnter;
                }
            }
            return minCloseLevel;
        }

        //================ Изменение значения волатильности генерируемыхъ котировок =================

        private void Tb_Vola_TextChanged(object sender, TextChangedEventArgs e)
        {
            _connector.Volatility = Convert.ToInt32(Tb_Vola.Text);
        }

        //================ Изменение частоты генерируемыхъ котировок =================================

        private void Tb_Interval_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            TimerInterval = Convert.ToInt32(Tb_Interval.Text);
        }

        private int TimerInterval { get; set; }
    }
}