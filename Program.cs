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

        static decimal upperPrice = 10000;
        static decimal levelStep = 100;
        static int levelCount = 10;

        static string paramsFilename = "params.txt";

        static Trade trade = new Trade();
        #endregion

        //======================================== Properties ========================================
        #region Properties
        public static decimal LevelStep
        {
            get
            {
                return levelStep;
            }

            set
            {
                if (value <= 1000)
                {
                    levelStep = value;
                    levels = Level.CalculateLevels(upperPrice, levelStep, levelCount);
                }
            }
        }
        #endregion

        //======================================== Methods ===========================================
        #region Methods

        static void Main(string[] args)
        {
            // значения по-умолчанию
            levels = Level.CalculateLevels(upperPrice, levelStep, levelCount);

            // читаем значения из файла
            LoadParams();

            // корректируем вручную
            ReadParams();

            // обновляем файл
            SaveParams();

            // отображаем текущие уровни
            DumpLevels();

            Position position = new Position();
            Console.ReadLine();
        }

        static void DumpLevels()
        {
            Console.WriteLine("Кол-во элементов в списке: " + levels.Count.ToString());

            foreach (var level in levels)
                Console.WriteLine(level.Price);
        }

        static string? ReadUserInput(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        static void ReadParams()
        {
            string? str = ReadUserInput($"Введите количество уровней ({levelCount}): ");
            if (str.Length > 0)
                levelCount = int.Parse(str);

            str = ReadUserInput($"Задайте верхнюю цену ({upperPrice}): ");
            if (str.Length > 0)
                upperPrice = decimal.Parse(str);

            str = ReadUserInput($"Введите шаг уровня ({levelStep})");
            if (str.Length > 0)
                levelStep = decimal.Parse(str);
        }

        static void SaveParams()
        {
            using (StreamWriter writer = new StreamWriter(paramsFilename, false))
            {
                writer.WriteLine(upperPrice.ToString());
                writer.WriteLine(levelCount.ToString());
                writer.WriteLine(levelStep.ToString());
            }
        }

        static void LoadParams()
        {
            try
            {
                using (StreamReader reader = new StreamReader(paramsFilename))
                {
                    for (int i = 0; i < 3; ++i)
                    {
                        string? line = reader.ReadLine();

                        if (line == null)
                            break;

                        switch (i)
                        {
                            case 0:
                                upperPrice = decimal.Parse(line);
                                break;

                            case 1:
                                levelCount = int.Parse(line);
                                break;

                            case 2:
                                LevelStep = decimal.Parse(line);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка чтения файла: {ex.Message}");
            }
        }

        #endregion


        delegate void Number();

        static Number number;


    }


}
