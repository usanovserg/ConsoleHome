using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleHome
{

    public class Program
    {


        //======================================== Fields ============================================
        #region Fields
        static List<Level> levels;
        static decimal priceUp;
        static decimal stepLevel;
        static int countLevels;
        static decimal lotLevel;



        static Trade trade = new Trade();


        #endregion

        //======================================== Properties ========================================
        #region Properties
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
                    //levels.Clear();
                    levels = Level.CalculateLevels(priceUp, stepLevel, countLevels);

                }

            }

        }

        #endregion


        //======================================== Methods ===========================================
        #region Methods

        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;
            
            Position position = new Position();

            /*

            number = WriteOutput;

            levels = new List<Level>();

            Load();

            number();

            string str = ReadUserInput("Введите количество уровней: ");

            countLevels = Convert.ToInt32(str);

            str = ReadUserInput("Задайте верхнюю цену: ");

            priceUp = decimal.Parse(str);


            str = ReadUserInput("Введите шаг уровня");

            StepLevel = decimal.Parse(str);

            str = ReadUserInput("Введите лот на уровень");

            lotLevel = decimal.Parse(str);

            number();

            Save();
            */
            Console.ReadLine();
        }

        static void WriteOutput()
        {
            Console.WriteLine("Кол-во элементов в списке: " + levels.Count.ToString());

            foreach (var level in levels)
            {
                Console.WriteLine(level.PriceLevel);
            }
        }

        static string ReadUserInput(string message)
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
            try
            {

                using (StreamReader reader = new StreamReader("params.txt"))
                {
                    int index = 0;

                    while (true)
                    {
                        string line = reader.ReadLine();

                        index++;

                        switch (index)
                        {

                            case 1: priceUp = decimal.Parse(line); break;

                            case 2: countLevels = int.Parse(line); break;

                            case 3: StepLevel = decimal.Parse(line); break;

                        }
                        if (line == null)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка считывания, нет данных в файле: " + ex.Message);
            }
        }
        

        #endregion


        delegate void Number();

        static Number number;


    }

  
}
