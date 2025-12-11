
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HomeWork156
{
    internal class Program
    {
        static void Main(string[] args)
        {
            position = new Position();
            position.OnAddTradeEvent += WriteLinePos;
            /* levels = new List<Level>();
            WriteLine();

            //string str = ReadLine("Введите количество уровней: ");

            //contLevels = Convert.ToInt32(str);

            string str = ReadLine("Задайте верхнюю цену: ");

            priceUp = decimal.Parse(str);

            str = ReadLine("Задайте нижнюю цену: ");

            priceDown = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня: ");



            StepLevel = decimal.Parse(str);


            WriteLine();*/
            Console.ReadLine();
        }
        //==========================================fields=============================
        #region fields
        static Position position;
        static List<Level> levels;
        static decimal priceUp;
        static decimal priceDown;
        static int contLevels;

        static Trade trade = new Trade();
        static Level level = new Level();
        #endregion


        //=========================================Property++++++++++++++++++++++++++++++
        #region
        static decimal StepLevel
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
                   levels=Level.CalculateLevels(priceUp, priceDown, stepLevel);
                }
            }
        }

        static decimal stepLevel;
        #endregion

        //======================================Methods=====================================
        #region

        static void WriteLine()
        {
            Console.WriteLine("Кол-во элементов в списке: " + levels.Count.ToString());
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

        static void WriteLinePos(List<Trade> trades)
        {

            Trade _lastTrade = trades.Last();
            string str = "Дабавлена сделка \n Trade ------ DataTime "+ _lastTrade.DateTime + " / SecCode = " + _lastTrade.SetCode + " / Volume = " + _lastTrade.Volume.ToString() + " / Price = " + _lastTrade.Price.ToString() + " / Direction = " + _lastTrade.Direction +
                "\nРасчет позиции \n Position ------  Lots = " + position.Lots + " / Position = " + string.Format("{0:f2}", position.Pos) + " / BalanceCount = " + position.BalancePrice + " / Direction = " + position.Direction + "\n";
            Console.WriteLine(str);
        }
        #endregion

    }
}
