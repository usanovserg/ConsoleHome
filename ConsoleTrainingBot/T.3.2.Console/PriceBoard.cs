using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleTrainingBot
{
    public class PriceBoard : Levels
    {
        ///////////////////////////////////////Fields///////////////////////////////////////////
        #region Fields
        private decimal? low_price_;
        private decimal? high_price_;
        private decimal? price_step_;
        private List<Levels> level_list_ = new List<Levels>();
        #endregion

        ///////////////////////////////////////Constructors//////////////////////////////////////////
        #region Constructors
        public PriceBoard ()
        {           
            SetLotLevel(GetLotlevel());
        }

        #endregion

        ///////////////////////////////////////Methods//////////////////////////////////////////
        #region Methods
        /// <summary>
        /// запрашивает и возвращает количество ценных бумаг в лоте
        /// </summary>
        /// <returns></returns>
        private decimal GetLotlevel()
        {
            System.Console.WriteLine("Введите количество ценных бумаг в лоте:");
            string input_quantity = System.Console.ReadLine();

            if (decimal.TryParse(input_quantity, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal tmp_lot_level) ||
                decimal.TryParse(input_quantity, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("ru-RU"), out tmp_lot_level))
            {
                if (tmp_lot_level > 0)
                {
                    return tmp_lot_level;
                }
                else
                {
                    System.Console.WriteLine("Введено некорректное значение!");
                    GetLotlevel();
                }
            }
            else
            {
                System.Console.WriteLine("Введено некорректное значение!");
                GetLotlevel();
            }
            return 0;
        }

        /// <summary>
        /// устанавливает минимальную цену
        /// </summary>
        /// <returns></returns>
        public PriceBoard SetLowPrice()
        {
            System.Console.WriteLine("Введите минимальную цену:");
            string input_price = System.Console.ReadLine();
            if (decimal.TryParse(input_price, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal tmp_low_price) ||
                decimal.TryParse(input_price, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("ru-RU"), out tmp_low_price))
            {
                if (tmp_low_price >= 0)
                {
                    low_price_ = tmp_low_price;
                }
                else
                {
                    System.Console.WriteLine("Введено некорректное значение!");
                    SetLowPrice();
                }
            }
            else
            {
                System.Console.WriteLine("Введено некорректное значение!");
                SetLowPrice();
            }
            return this;
        }

        /// <summary>
        /// устанавливает максимальную цену
        /// </summary>
        /// <returns></returns>
        public PriceBoard SetHighPrice()
        {
            System.Console.WriteLine("Введите максимальную цену:");
            string input_price = System.Console.ReadLine();
            if (decimal.TryParse(input_price, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal tmp_high_price) ||
                decimal.TryParse(input_price, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("ru-RU"), out tmp_high_price))
            {
                if (tmp_high_price >= 0 && tmp_high_price > low_price_)
                {
                    high_price_ = tmp_high_price;
                }
                else
                {
                    System.Console.WriteLine("Введено некорректное значение!");
                    SetLowPrice();
                }
            }
            else
            {
                System.Console.WriteLine("Введено некорректное значение!");
                SetLowPrice();
            }
            return this;
        }

        /// <summary>
        /// устанавливает шаг цены
        /// </summary>
        /// <returns></returns>
        public PriceBoard SetPriceStep()
        {
            System.Console.WriteLine("Введите шаг цены:");
            string input_price_step = System.Console.ReadLine();
            if (decimal.TryParse(input_price_step, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal tmp_step) ||
                decimal.TryParse(input_price_step, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("ru-RU"), out tmp_step))
            {
                if (tmp_step > 0 && low_price_ + tmp_step <= high_price_)
                {
                    price_step_ = tmp_step;
                }
                else
                {
                    System.Console.WriteLine("Введено некорректное значение!");
                    SetPriceStep();
                }

            }
            else
            {
                System.Console.WriteLine("Введено некорректное значение!");
                SetPriceStep();
            }
            return this;
        }

        /// <summary>
        /// заполняет Лист значениями исходя из минимальной/максимальной цены и шага
        /// </summary>
        /// <returns></returns>
        public PriceBoard FillPriceList()
        {
            decimal reminder = (int)((high_price_ - low_price_) % price_step_);
            if (reminder == 0)
            {
                int value = (int)((high_price_ - low_price_) / price_step_);
                level_list_.Capacity = value;
                for (int i = 0; i <= value; i++)
                {
                    level_list_.Add(new Levels() { price_level = (decimal)(low_price_ + i * price_step_)}) ;
                }
            }
            else
            {
                int value = (int)((high_price_ - low_price_) / price_step_);
                level_list_.Capacity = value + 1;
                for (int i = 0; i <= value; i++)
                {
                    level_list_.Add(new Levels() { price_level = (decimal)(low_price_ + i * price_step_)});
                }
                level_list_.Add(new Levels() { price_level = (decimal)high_price_ });
            }
            return this;
        }



        /// <summary>
        /// выводит в поток список цен
        /// </summary>
        /// <param name="writer"></param>
        public void PrintPriceList(TextWriter writer)
        {
            if (level_list_.Any())
            {
                writer.WriteLine("Доска уровней:");
                foreach (var price in level_list_)
                {
                    writer.WriteLine($"Цена уровня: {price.price_level}; Лот на уровень: {price.lot_level}; Открытый объем: {price.open_volume}");
                }
            }
            else
            {
                writer.WriteLine("Доска уровней пуста");
            }
        }
        #endregion


    }
}
