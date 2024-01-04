using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace MyConsole
{
    public class Program
    {

        static void Main(string[] args)
        {

            Positions positions = new Positions();

            positions.IzmPoz += IzmeneniePoz;

                            
            /*number = WriteLine;

            levels = new List<Level>();

            Load();

            number();

            string str = ReadLine("Введите количество уровней:  ");              

            countLevels = Convert.ToInt32(str);

            str = ReadLine("Задайте верхнюю цену:  ");

            priceUP = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня:  ");

            StepLevel = decimal.Parse(str);

            str = ReadLine("Введите лот на уровень:  ");

            lotlevel = decimal.Parse(str);

            number();

            Save();*/
            
            Console.ReadLine();

        }
        //================================================Pole===================================================================================
        #region Field  Pole

        static List<Level> levels;  // создали поле, типа список состоящее из объектов типа decimal. Обычное поле класса Program

        static decimal priceUP;

        static int countLevels;

        static decimal lotlevel;
        //=============================================

        static Trade trade = new Trade();

        #endregion

        //================================================Svoystva===============================================================================
        #region Properties Svoystva

        static decimal stepLevel; // создали поле

        public static decimal StepLevel // создаем поле типа Свойства, имеет два метода Get и Set

        {
            get
            {
                return stepLevel;
            }
            set
            {
                stepLevel = value;

                levels = Level.CalculateLevels(priceUP, stepLevel, countLevels);

            }
        }
        #endregion
        //================================================Metod==================================================================================
        #region Metod

        public static void IzmeneniePoz()   // создаем метод изменения позиции
        {
            Console.WriteLine("Позиция изменилась:  ");

        }





        static void WriteLine()   // создаем метод WriteLine
        {
            Console.WriteLine("Количество элементов в списке:  " + levels.Count.ToString());

            for (int i = 0; i < levels.Count; i++)
            {
                Console.WriteLine(levels[i].PriceLevel);
            }

        }

        static string ReadLine(string message) // метод ввода
        {
            Console.WriteLine(message);
            
            return Console.ReadLine();
        }

        static void Save()
        {
            using (StreamWriter writer = new StreamWriter("params.txt", false))
            {
                writer.WriteLine(priceUP.ToString());
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

                    switch (index)
                    {
                        case 1:
                            priceUP = decimal.Parse(line); break;
                        case 2:
                            countLevels = int.Parse(line); break;
                        case 3:
                            StepLevel = int.Parse(line); break;

                    }
                    
                    if (line==null)
                    {
                        break;
                    }
                }
            }

        }

        



        #endregion

        delegate void Number();
        
        static Number number;




        

    }


}
