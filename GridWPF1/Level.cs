using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWPF1
{
    public class Level
    {
        //public Level()
        //{
        //    PriceEnter = priceLevel;
        //}

        /// <summary>
        /// Цена входа
        /// </summary>
        public decimal PriceEnter = 0;

        /// <summary>
        /// Цена вsхода
        /// </summary>
        public decimal PriceExit = 0;

        /// <summary>
        /// Лот на уровень
        /// </summary>
        public decimal LotLevel = 0;

        /// <summary>
        /// Объем на уровень
        /// </summary>
        public decimal Volume = 0;

        public Side side;


        //====================== Цикл расчета сетки =========================================
        #region Цикл расчета сетки

        public static List<Level> CalcLevels(decimal priceLevel, decimal step, int count, 
                                                decimal profit, decimal volume, Side side)
        {
            List<Level> levels = [];
            //side = Side.Buy;

            for (var i = 0; i < count + 1; i++)
            {
                Level level = new()
                {
                    PriceEnter = priceLevel,
                    PriceExit = priceLevel + profit,
                    Volume = volume,
                    side = side
                };

                levels.Add(level);
                priceLevel -= step;
            }

            levels = [.. levels.OrderByDescending(x => x.PriceEnter)];
            
            return levels;
        }
        #endregion
        // ========================================================================================

        public string GetSaveStr()
        {
            string result = "";

            result += PriceEnter + "|";
            result += PriceExit + "|";
            result += Volume + "|";
            result += side + "|";

            return result;
        }
    }
}

