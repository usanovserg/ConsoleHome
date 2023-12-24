using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_5
{/// <summary>
 /// Класс для хранения значений текущей позиции
 /// </summary>
    public class CurrentPosition
    {

        //+++++++++++++++++++++++++++++++++++++++++++++++++Fields++++++++++++++++++++++++++++++++++++++++++++++++++

        #region Fields

        private string? _positionType; // Тип открытой позиции

        private decimal _deposit = 10000; // Размер стартового депозита

        private decimal _priceOflot = 0; // Цена лота

        private decimal _price = 0; // Общая стоимость открытой позиции

        private decimal _volume = 0; // Объём открытой позиции

        private string? _secCode; // Название инструмента на бирже

        private string _classCode="Акции обычные"; // Код класс интсрумента

        private DateTime _dateTime = DateTime.Now; // Время сделки

        private string _portfoloiNumber = "Номер клиентского портфеля 1";  // Счёт клиента

        #endregion

        //+++++++++++++++++++++++++++++++++++++++++++++++++Properties++++++++++++++++++++++++++++++++++++++++++++++

        #region Properties

        public decimal PriceOfLot
        {
            get { return _priceOflot; }
            set { _priceOflot = value; }
        }


        public string? PositionType
        {
            get { return _positionType; }
            set { _positionType = value; }
        }
        public decimal Deposit
        {
            get { return _deposit; }
            set { _deposit = value; }
        }
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }
        public decimal Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }
        public string? SecCode
        {
            get { return _secCode; }
            set { _secCode = value; }
        }
        public string ClassCode
        {
            get { return _classCode; }
            set { _classCode = value; }
        }
        public DateTime DateTime
        {
            get { return _dateTime; }
            set { _dateTime = value; }
        }
        public string Porfolio
        {
            get { return _portfoloiNumber; }
            set { _portfoloiNumber = value; }
        }

        #endregion

        //+++++++++++++++++++++++++++++++++++++++++++++++++Mhetods+++++++++++++++++++++++++++++++++++++++++++++++++

        #region Methods

        public void ConsoleWrite()
        { Console.WriteLine("===============================================================");
            
            Console.WriteLine("\tТекущий остаток счёта " + _deposit + " рублей.");
                        
            Console.WriteLine("\tНаправление текущей позиции " + _positionType + ".");
            
            Console.WriteLine("\tЦена одного лота в открытой позиции " + _priceOflot + " рублей.");
            
            Console.WriteLine("\tОбщая стоимость позиции " + _price + " рублей");
            
            Console.WriteLine("\tОбщий объём открытой позиции " + _volume + " лотов.");
                        
            Console.WriteLine("\tНазвание инструмента в позиции " + _secCode + ".");
          
            Console.WriteLine("\tКласс инструмента " + _classCode + ".");
           
            Console.WriteLine("\tВремя открытия позиции " + _dateTime + ".");
            
            Console.WriteLine("\tНомер клиентского счёта " + _portfoloiNumber + ".");

            Console.WriteLine("===============================================================");


        }


        #endregion
    }

}
