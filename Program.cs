using ConsoleHome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;                               
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleHome
{
    public class Program
    {

        static Trade trade = new Trade();

        static void Main(string[] args)
        {


            /*string price = "100,1";   //ошибочное число
            int num = 0;
            decimal dec = 0;
            try                        //сюда помещаеться все что может вызвать ошыбку или есть пожозрение на некоректность данных
            {
                num = int.Parse(price);
                Console.WriteLine(num);
            }
            catch (Exception ex)
            {
                dec = decimal.Parse(price); //если пишем так то на консоль выведется значение децимал, если не писать ни чего то на консоль не выведеться ни чего и программа просто 
                Console.WriteLine(dec);     //пойдет дальше, не смогла спарсить ну и ладно главное не упала!
            }*/


            Connector.Connect();
            // Connector.AddDelegate(WriteLine);  Это вызов делегата
            Connector.NewTradeEvent += WriteLine;  // это событие и ему присваиваем метод который хотим в данном случае (WriteLine)
            //Connector.NewTradeEvent += Fhjksk;
            namber = WriteLine;
            // namber += Fhjksk;

            Position position = new Position();

            levels = new List<Level>();

            namber();

            /*Task.Run(() =>  //создание цикла в отдельном потоке,     Task задача,  Run запуск задачи  => цикл работает в отдельном потоке
            {
                while (true)
                {
                    if (Connector.Trades.Count > 0
                        && Connector.Trades.Count > _lastCount)
                    {
                        //занчит появились новые сделки
                    }

                    Thread.Sleep(100);
                }
            });
            */

            string str = ReadLine("Введите верхнию цену(знак припенания , ) : ");
            priceUp = decimal.Parse(str);
            str = ReadLine("Введите нижнию чену: ");
            priceDown = decimal.Parse(str);
            str = ReadLine("Введите шаг уровня: ");
            StepLevel = decimal.Parse(str);
            str = ReadLine("Введите лот на уровень: ");
            lotlevel = decimal.Parse(str);

            namber();

            // Save();
            Console.ReadLine();
        }




        //============================================================= Fields =======================================
        #region Fields

        static List<Level>? levels;  //поле       = null!   точно не будет null  или можно ставить вопрос
        static decimal stepLevel;
        static decimal priceUp;
        static decimal priceDown;
        static decimal lotlevel;
        static Connector Connector = new Connector();
        static int _lastCount = 0;

        #endregion


        //============================================================= Properties ===================================
        #region Properties

        public static decimal StepLevel                       //развернутая запись свойства ( в ней можно расписывать различные действия)
        {
            get
            {
                return stepLevel;
            }
            set
            {
                if (value <= 100)                                            //проверка на коректность
                {
                    stepLevel = value;
                    levels = Level.CalculateLevels(priceUp, stepLevel, priceDown);

                }
            }
        }


        #endregion





        //============================================================= Methods ========================================
        #region Methods

        static void WriteLine()
        {
            Console.WriteLine("Количество элементов в списке: " + levels?.Count.ToString());

            for (int i = 0; i < levels?.Count; i++)
            {
                Console.WriteLine(levels[i].PriceLevel);
            }
        }

        static string ReadLine(string message)
        {
            Console.WriteLine(message);
            //string? str = Console.ReadLine();     //аналог ??
            // if (str == null) return "";
            return Console.ReadLine() ?? "";  //str;
        }

        /*static void Fhjksk()
        {
            //Level level = new Level();
            //level.PriceLevel = 9900;
            Console.WriteLine("Чего то вывели");
        }*/

        #endregion


        delegate void Namber();
        static Namber namber;

    }
}  


   
