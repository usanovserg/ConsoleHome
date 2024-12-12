using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace УровниСетки
{
    internal class Program
    {
        static void Main(string[] args)
        {            
            levels = new List<decimal>();

            WriteLine();

            string str = ReadLine("Введите верхнию цену(знак припенания , ) : ");          

            decimal priceUp = decimal.Parse(str);

            str = ReadLine("Введите нижнию чену: ");
                        
            decimal priceDown = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня: ");            

            StepLevel = int.Parse(str);            

            WriteLine();

            Console.ReadLine();
        }        

        //static decimal StepLevel { get; set; }     .. сокращенная запись свойства

        public static int StepLevel    //развернутая запись свойства ( в ней можно расписывать различные действия)
        {
            get
            {
                return stepLevel;
            }

            set
            {
                if (value <= 100) //проверка на коректность
                {
                    stepLevel = value;

                    decimal stepPrice = priceUp;

                    int count = (int)((priceUp - priceDown) / stepLevel);  // для СЕРГЕЯ ; запутался в этой формуле, не могу понять почему не считает

                    for (decimal i = 0; i < count; i++)
                    {
                        levels.Add(stepPrice);

                        stepPrice -= stepLevel;
                    }
                }
            }
        }

        static List<decimal> levels;  //поле        для СЕРГЕЯ ; почему null

        static int stepLevel; //поле

        static decimal priceUp;

        static decimal priceDown;

        static void WriteLine()
        {
            Console.WriteLine("Количество элементов в списке: " + levels.Count.ToString());

            for (int i = 0; i < levels.Count; i++)
            {
                Console.WriteLine(levels[i]);
            }           
        }

        static string ReadLine(string message)
        {
            Console.WriteLine(message);

            return Console.ReadLine();       //   для СЕРГЕЯ   что не нравиться
        }          
    }
}
