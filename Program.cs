using ConsoleHome;
using System;
using System.Collections.Generic;
using System.Data;

namespace MyConsole
{
    internal class Program
    {
        //=================================== Fields ===============================================
        #region Fields
        static List<Level> levels;
        static decimal priceUp;
        static decimal priceDown;
        static decimal lotLevel;
        #endregion

        //=================================== Properties ===============================================
        #region Prperties
        static decimal StepLevel
        {
            get
            {
                return slevel;
            }
            set
            {
                slevel = value;
                levels = Level.CalculateLevels(priceDown, priceUp, slevel);
                Level.LotLevel = lotLevel;
            }
        }
        static decimal slevel;
        #endregion
        static void Main(string[] args)
        {
            WriteResult();
            priceDown = ReadLine("Введите нижний уровень цены:");
            priceUp = ReadLine("Введите верхний уровень цены:", priceDown);
            lotLevel = ReadLine("Введите лот на уровень:");
            StepLevel = ReadLine("Введите шаг изменения цены:");
            WriteResult();
            Console.ReadLine();
        }
        //=================================== Methods ===============================================
        #region Methods
        static decimal ReadLine(string str, decimal value = 0)
        {
            decimal rez;
            Console.WriteLine(str);
            while (!decimal.TryParse(Console.ReadLine(), out rez) || (value >= rez))
            {
                Console.WriteLine($"Ошибка ввода {rez}");
            }
            return rez;
        }
        static void WriteResult()
        {
            for (int i = 0; i < levels?.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {levels[i].PriceLavel.ToString()}");
            }
            Console.WriteLine("\nЧисло уровней цены - " + levels?.Count.ToString());
        }
        #endregion
    }

}

