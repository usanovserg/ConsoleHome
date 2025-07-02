using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace HomeworkLesson_1_3
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Настройка разделителя дробных чисел:
             Создается объект CultureInfo для культуры "en-US" (американский английский)
             Устанавливается пользовательский разделитель дробной части
             Эта культура устанавливается для текущего потока*/

            string str = ReadData("Введите разделитель дробного числа: ");

            CultureInfo culture = new CultureInfo("en-US");
            culture.NumberFormat.NumberDecimalSeparator = str;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;

            /* Ввод параметров финансовой сетки,запрашиваются: верхняя граница цены (priceUp),
             нижняя граница цены (priceDown), шаг цены (stepLevel)*/
            
            str = ReadData("Задайте верхнюю цену: ");
            decimal priceUp = decimal.Parse(str);

            str = ReadData("Задайте нижнюю цену: ");
            decimal priceDown = decimal.Parse(str);

            str = ReadData("Введите шаг уровня: ");
            decimal stepLevel = decimal.Parse(str);

                 /* Создание финансовой сетки: 
                 * Создается объект FinancialGrid с введенными параметрами
                 * Выводится информация об уровнях с помощью метода WriteLevels()
                 * Обрабатываются возможные исключения */
            try
            {
                
                FinancialGrid financialGrid = new FinancialGrid(priceUp, priceDown, stepLevel);
                WriteLevels(financialGrid);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        /// <summary>
        /// выводит сообщение и возвращает введенную пользователем строку
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        static string ReadData(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        /// <summary>
        /// выводит количество уровней и их значения
        /// </summary>
        /// <param name="grid"></param>
        static void WriteLevels(FinancialGrid grid)
        {
            Console.WriteLine("Кол-во элементов в списке: " + grid.getLevelsCount().ToString());
            foreach (decimal level in grid.getLevels())
            {
                Console.WriteLine(level);
            }
        }
    }


    class FinancialGrid
    {
        private decimal highestPrice; //верхняя граница цены
        private decimal lowestPrice;  //нижняя граница цены
        private decimal priceStep;    //шаг между уровнями
        private List<decimal> priceLevels; //список ценовых уровней

        /// <summary>
        /// Создает список ценовых уровней и добавляет уровни в список
        /// </summary>
        /// <returns></returns>
        private List<decimal> buildPriceLevels()
        {
            List<decimal> result = new List<decimal>();
            decimal pricePointer = this.highestPrice;
            while (pricePointer >= lowestPrice)
            {
                result.Add(pricePointer);
                pricePointer = pricePointer - this.priceStep;
            }

            return result;
        }

        /// <summary>
        /// Проверяет корректность входных параметров
        /// </summary>
        /// <param name="highestPrice"></param>
        /// <param name="lowestPrice"></param>
        /// <param name="priceStep"></param>
        /// <exception cref="Exception"></exception>
        public FinancialGrid(decimal highestPrice, decimal lowestPrice, decimal priceStep)
        {
            if (highestPrice <= 0)
            {
                throw new Exception("Некорректная верхняя цена");
            }

            if (lowestPrice <= 0)
            {
                throw new Exception("Некорректная нижняя цена");
            }

            if (priceStep <= 0 || priceStep > 100)
            {
                throw new Exception("Некорректный шаг цены");
            }

            if (lowestPrice >= highestPrice)
            {
                throw new Exception("Некорректный диапазон цен");
            }

            this.highestPrice = highestPrice;
            this.lowestPrice = lowestPrice;
            this.priceStep = priceStep;
            this.priceLevels = this.buildPriceLevels();
        }

        /// <summary>
        /// возвращает количество уровней
        /// </summary>
        /// <returns></returns>
        public int getLevelsCount()
        {
            return this.priceLevels.Count;
        }

        /// <summary>
        /// возвращает список уровней
        /// </summary>
        /// <returns></returns>
        public List<decimal> getLevels()
        {
            return this.priceLevels;
        }

        /// </summary>
        /// <returns></returns>
    }
}
