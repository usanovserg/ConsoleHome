using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Program
    {
        static void Main(string[] args)
        {
            Position position = new Position("SBER");
            /*

            levels = new List<Level>();

            priceUp = GetPricelValue(
                PRICEUPTYPE,
                "Введите цену верхнего уровня: ",
                "Цена верхнего уровня должна быть больше 0"
            );

            priceDown = GetPricelValue(
                PRICEDOWNTYPE,
                "Введите цену нижнего уровня: ",
                "Цена нижнего уровня должна быть меньше цены верхнего уровня",
                priceUp
            );

            stepLevel = GetPricelValue(
                PRICESTEPTYPE,
                "Введите шаг цены: ",
                "Шаг цены должен быть больше 0"
            );

            LevelCount = (int)((priceUp - priceDown) / stepLevel) + 1;
            */
            Console.ReadLine();
        }

        // =============================== Constants ================================
        #region Constants

        const string PRICEUPTYPE = "priceUp";
        const string PRICEDOWNTYPE = "priceDown";
        const string PRICESTEPTYPE = "priceStep";

        #endregion

        // =============================== Fields ================================
        #region Fields 

        static List<Level> levels;
        static decimal priceUp;
        static decimal priceDown;
        static decimal stepLevel;

        #endregion

        // =============================== Properties ================================
        #region Properties

        static int levelCount;
        static int LevelCount
        {
            get
            {
                return levelCount;
            }

            set
            {
                levelCount = value;
                Console.WriteLine("Количество уровней: " + (value).ToString());

                levels = Level.CalculateLevels(priceUp, priceDown, value);
            }
        }

        #endregion

        static Trade trade = new Trade();

        // =============================== Methods ================================
        #region

        static decimal GetDecimalValue(string message)
        {
            Console.WriteLine(message);
            decimal outDecimalValue;
            bool isValid;
            do
            {
                string inputValue = Console.ReadLine();
                isValid = decimal.TryParse(inputValue, out outDecimalValue);
                if (!isValid)
                {
                    Console.WriteLine("Вводимое значение должно быть числовым");
                }
            } while (!isValid);
            return outDecimalValue;
        }

        static decimal GetPricelValue(string priceType, string infoMessage, string errorMessage, decimal? validateValue = null)
        {
            decimal value;
            bool condition;
            do
            {
                value = GetDecimalValue(infoMessage);
                condition = GetCondition(priceType, value, validateValue);
                if (condition)
                {
                    Console.WriteLine(errorMessage);
                }
            } while (condition);

            return value;
        }

        static bool GetCondition(string priceType, decimal value, decimal? validateValue = null)
        {
            if (priceType != PRICEUPTYPE && priceType != PRICEDOWNTYPE && priceType != PRICESTEPTYPE)
                throw new ArgumentException("Ошибка указания типа цены");

            switch (priceType)
            {
                case PRICEUPTYPE:
                    return value < 0;
                case PRICEDOWNTYPE:
                    if (validateValue == null)
                        throw new ArgumentException("Ошибка указания цены");

                    return value < 0 || value > validateValue;
                case PRICESTEPTYPE:
                    return value <= 0;
                default:
                    return false;
            }
        }

        #endregion
    }

}
