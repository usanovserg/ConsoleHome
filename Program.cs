
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleHome
{
    public class Program
    {
        static void Main(string[] args)
        {

          //  Position position = new Position();
            
            number = WriteLine;
            
            levels = new List<Level>();

            Load();

            number();

            string str = ReadLine("Введите кол-во уровней: ");

            countLevels = Convert.ToInt32(str);

            str = ReadLine("Задайте верхнюю цену: ");

            priceUp = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня: ");

            StepLevel = decimal.Parse(str);

            str = ReadLine("Введите лот на уровень: ");

            lotLevel = decimal.Parse(str);

            number();

            Save();

            Console.ReadLine();

        }

        static List<Level> levels;

        static decimal priceUp;

        static int countLevels;

        static decimal lotLevel;

        static Trade trade = new Trade();

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
                    levels = Level.CalculateLevels(priceUp, stepLevel, countLevels);
                }

            }
        }

        static decimal stepLevel;


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

       static void Save()
        {
            using (StreamWriter writer = new StreamWriter("params.txt", false))
            {
                writer.WriteLine(priceUp.ToString());
                writer.WriteLine(countLevels.ToString());
                writer.WriteLine(stepLevel.ToString());
            }
        }

        static void Load()
        {
            using(StreamReader reader = new StreamReader("params.txt"))
            {
                int index = 0;
                while (true)
                {
                    string line = reader.ReadLine();
                    index++;
                    switch(index)
                    {
                        case 1:
                            priceUp = decimal.Parse(line); 
                            break;
                        case 2:
                            countLevels = int.Parse(line);
                            break;
                        case 3:
                            StepLevel = decimal.Parse(line);
                            break;
                    }

                    if (line == null)
                    {
                        break;
                    }
                }
            }
            
        }
        
        delegate void Number();

        static Number number;

        
    }
}
