using System;
using System.Collections.Generic;

namespace MyConsole
{
    internal class Program
    {
        //=================================== Fields ===============================================
        #region Fields
        static List<decimal> levels;
        static decimal priceUp;
        static decimal priceDown;
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
                levels = new List<decimal>();
                for (decimal price = priceUp; price >= priceDown; price -= slevel)
                {
                    levels.Add(price);
                }
            }
        }
        static decimal slevel;
        #endregion
        static void Main(string[] args)
        {
            WriteResult();
            priceDown = ReadLine("Введите нижний уровень цены:");
            priceUp = ReadLine("Введите верхний уровень цены:", priceDown);
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
                Console.WriteLine($"{i + 1} - {levels[i].ToString()}");
            }
            Console.WriteLine("\nЧисло уровней цены - " + levels?.Count.ToString());
        }
        #endregion
    }
}

