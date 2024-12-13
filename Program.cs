using ConsoleHome;
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
            Position position = new Position();
           
                /*
            levels = new List<decimal>();

            WriteLine();

            string str = ReadLine("Введите верхнию цену(знак припенания , ) : ");               
            
            priceUp = decimal.Parse(str);

            str = ReadLine("Введите нижнию чену: ");
                        
            priceDown = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня: ");            

            StepLevel = decimal.Parse(str);            

            WriteLine();
            */
            Console.ReadLine();
        }        

        //static decimal StepLevel { get; set; }     .. сокращенная запись свойства

        public static decimal StepLevel    //развернутая запись свойства ( в ней можно расписывать различные действия)
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

                    int count = (int)((priceUp - priceDown) / stepLevel);  

                    if (levels != null)
                    {
                        for (decimal i = 0; i < count; i++)
                        {
                            levels.Add(stepPrice);

                            stepPrice -= stepLevel;
                        }
                    }                    
                }
            }
        }

        static List<decimal> levels = null!;  //поле        ! точно не будет null

        static decimal stepLevel; //поле

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

            string? str = Console.ReadLine();

            if (str == null) return "";

            return str;       
        }          
    }
}
