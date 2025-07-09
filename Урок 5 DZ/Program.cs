using MyConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace MyConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            Position position = new Position();
            
            Connector.Connect();

            levels = new List<Level>(); 
            /*
            string str = ReadLine("Введите количество уровней: ");

            contLevels = Convert.ToInt32(str);

            str = ReadLine("Задайте верхнюю цену: ");

            priceUp = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня: ");

            StepLevel = decimal.Parse(str);

            str = ReadLine("Введите лот на уровнь: ");

            lotLevel = decimal.Parse(str);*/

            Console.ReadLine();
        }

        /* ============================== Felds =====================================   */

        #region Felds
        /// <summary>
        /// Список уровней
        /// </summary>
        static List<Level> levels;
        /// <summary>
        /// Верхняя цена уровня
        /// </summary>
        static decimal priceUp;
        /// <summary>
        /// Количество уровней
        /// </summary>
        static int contLevels;
        /// <summary>
        /// Количество лотов на уровень
        /// </summary>
        static decimal lotLevel;

        /*создаем новую переменную Connector  и через нее подключаемся  к бирже*/
        static Connector Connector = new Connector();

        //===============================================================================

        static Trade trade = new Trade();

        static int _lastCount = 0;

        #endregion
               
        /* ============================== Proporties =====================================   */
        #region Proporties
        public static decimal StepLevel
        {
            get
            {
                return stepLevel;
            }

            set
            {
                if (value <= 100)
                {
                    stepLevel = value;

                    levels = Level.CalculateLevels(priceUp, stepLevel, contLevels);
                }
            }
        }

        static decimal stepLevel;

        #endregion

        
        /* ============================== Metods =====================================   */
        #region Metods
        
        static void WriteLine()
        {
            Console.WriteLine("Количество элементов в списке: " + levels.Count.ToString());

            for (int i = 0; i < levels.Count; i++)
            {
                Console.WriteLine(levels[i].PriceLevel);
            }
        }

        static string ReadLine(string message)
        {
            Console.WriteLine(message);

            return Console.ReadLine();
        }

        
    }
    #endregion
    /* Для примера делаем коннектор к бирже, одна из функций которого - формирование списка
     * поступающих сделок  List<Trade> */
    public class Connector
    {
        public delegate void newTradeEvent();

        public event newTradeEvent NewTradeEvent;

        public List<Trade> Trades = new List<Trade>();

        private void NewTrade(Trade trade)
        {
            Trades.Add(trade);
        }

        public void Connect()
        {
            Console.WriteLine("Connect is ExChange");
        }

        public void AddDelegate(newTradeEvent metod)
        {
            NewTradeEvent = metod;
        }
    }
}
