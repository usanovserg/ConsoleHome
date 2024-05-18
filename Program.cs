using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace MyConsole
{

    internal class Program
    {

        static List<decimal> levels;
        static decimal priceUp;
        static decimal priceDown;
        static decimal stepLevel;


        static void Main(string[] args)
        {
            decimal userStep;

            Console.OutputEncoding = Encoding.UTF8;

            levels = new List<decimal>();

            WriteOutput();

            priceDown = decimal.Parse(ReadUserInput("Задайте нижнюю цену: "));
            priceUp = decimal.Parse(ReadUserInput("Задайте верхнюю цену: "));



            while (priceDown <= 0 || priceUp <= 0 || priceDown > priceUp)
            {
                Console.WriteLine("Нижняя цена должна быть меньше верхней и отличной от 0.");
                priceDown = decimal.Parse(ReadUserInput("Задайте нижнюю цену: "));
                priceUp = decimal.Parse(ReadUserInput("Задайте верхнюю цену: "));

            }


            do
            {
                string userStepInput = ReadUserInput("Введите шаг уровня: ");

                if (decimal.TryParse(userStepInput, out userStep) && userStep > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите положительное число.");
                }

            } while (true);


            StepLevel = userStep;

            WriteOutput();
            Console.ReadLine();
        }


        public static decimal StepLevel
        {
            get
            {
                return stepLevel;
            }

            set
            {

                if (value <= 1000)
                {
                    stepLevel = value;
                    levels.Clear();

                    decimal priceLevel = priceUp;

                    for (decimal i = priceDown; i <= priceUp; i += stepLevel)
                    {
                        if (priceLevel <= 0)
                        {
                            break;
                        }
                        else
                        {
                            levels.Add(i);
                        }
                    }
                }

            }

        }


        static void WriteOutput()
        {
            Console.WriteLine("Кол-во элементов в списке: " + levels.Count.ToString());

            foreach (var level in levels)
            {
                Console.WriteLine(level);
            }
        }

        static string ReadUserInput(string message)
        {

            Console.WriteLine(message);
            return Console.ReadLine();
        }
    }
}
