using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleHome
{
    public class Program
    {
        static void Main(string[] args)
        {
            Position position = new Position();

            position.PositionChangeEvent += PositionChanged; //подписались на событие "изменение позиции"
            
            Console.ReadLine();

            //=================работа с уровнями============================================================================================
            /*levels = new List<Level>();                           //для хранения списка уровней

            WriteLine(levels);                                            //выводит кол-во элементов списка и все значения списка уровней

            priceDown = ReadLine("Введите нижнюю цену");                   //запрос на ввод значения пользователем
                        
            do 
            {
                priceUp = ReadLine("\nВведите верхнюю цену");

                if (priceUp <= priceDown) 
                {
                    Console.WriteLine("Верхняя цена должна быть больше нижней!");
                }
            }
            while (priceUp<=priceDown);

            
            StepLevel = ReadLine("\nВведите шаг уровня");

            lotLevel = ReadLine("\nВведите лот на уровень");
                                                
            WriteLine(levels);

            Console.ReadLine();*/

        }

        #region ===============================================Fields=============================================================

        static List<Level>? levels;

        static decimal priceDown=0;

        static decimal priceUp;

        static int countLevels;

        static decimal lotLevel;

        //==============================

        static Trade trade = new Trade();

        #endregion

        #region ===============================================Properties=======================================================
        public static decimal StepLevel
        {
            get
            {
                return stepLevel;
            }
            set
            {
                decimal d_count = 0;

                if (value > 0)
                {
                    d_count = (priceUp - priceDown) / value;
                }

                if (d_count >= 1 && value > 0)
                {
                    stepLevel = value;
                    countLevels = Convert.ToInt32(d_count + 1);

                    levels = Level.CalculateLevels(priceUp, priceDown, stepLevel, countLevels);

                }
                else
                {
                    Console.WriteLine("Неверное значение шага уровня");

                    StepLevel = ReadLine("\nВвведите шаг уровня");                        //Рекурсия работает в сеттере свойства!!! Обалденные возможности!!!
                }

            }

        }
        static decimal stepLevel;

        #endregion

        #region ====================================================Methods========================================================

        /// <summary>
        /// Выводит кол-во элементов списка и все значения списка
        /// </summary>
        /// <param name="i_levels"></param>
        static void WriteLine(List<Level> i_levels)                                   
        {
            int il = i_levels.Count;

            Console.WriteLine("\nКол-во элементов в списке: " + il.ToString());

            if (il != 0)
            {

                Console.WriteLine("");

                for (int i = 0; i < il; i++)
                {
                    Console.WriteLine(i_levels[i].PriceLevel);
                }
                Console.WriteLine("\nНижняя цена: " + priceDown.ToString());

                Console.WriteLine("Верхняя цена: " + priceUp.ToString());

                Console.WriteLine("Шаг уровня: " + StepLevel.ToString());

                Console.WriteLine("Расчетное количество уровней: " + countLevels.ToString());
            }
            else
            {
                Console.WriteLine("Список пуст!\n");
            }


        }

        /// <summary>
        /// Запрашивает ввод значения типа decimal
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        static decimal ReadLine(string message)                                         
        {
            bool sucess = true;

            decimal result;

            do
            {
                Console.WriteLine(message);

                sucess = decimal.TryParse(Console.ReadLine(), out result);

                if (!sucess || result < 0)
                {
                    Console.WriteLine("Введено некорректное значение!");
                }
            }
            while (!sucess || result < 0);

            return result;

        }

        /// <summary>
        /// Возвращает случайную строку с указанным префиксом
        /// </summary>
        /// <returns></returns>
        public static string RandomString(string prefix)
        {

            Random rnd = new Random();

            int randomNum = rnd.Next(1, 1000);

            string str = prefix + randomNum.ToString("X");

            return str;
        }

        /// <summary>
        /// Обработчик события - изменения позиции
        /// </summary>
        /// <param name="position"></param>
        static void PositionChanged(Position position)
        {
            Console.WriteLine($"Позиция изменена. Данные о всей позиции переданы в Main через Событие. Размер позиции {position.Volume} lot.");

            Console.Write("Новая позиция: ");

            PrintProperties(position);

            Console.WriteLine();
        }

        /// <summary>
        /// Вывод на экран всех свойств объекта
        /// </summary>
        /// <param name="obj"></param>
        static public void PrintProperties(object obj)
        {
            foreach (PropertyInfo property in obj.GetType().GetProperties())
            {
                Console.Write($"{property.Name} = {property.GetValue(obj)}/ ");
            }
            Console.WriteLine();
        }
        
        #endregion
    }


}
