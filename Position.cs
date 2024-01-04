using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace Lesson_6
{/// <summary>
 /// Класс для хранения значений текущей позиции
 /// </summary>
    public class CurrentPosition
    {
        //=================================/Fields/=================================//
        #region Fields

        private string? _positionType; // Тип открытой позиции

        private decimal _deposit = 10000; // Размер стартового депозита

        private decimal _priceOflot = 0; // Цена лота

        private decimal _price = 0; // Общая стоимость открытой позиции

        private decimal _volume = 0; // Объём открытой позиции в лотах

        private string? _secCode; // Название инструмента на бирже

        private DateTime _dateTime; // Время сделки

        #endregion
        //=================================/Methods/=================================//
        #region methods
        public void PositionValue(decimal value1, decimal value2, decimal value3, DateTime value4)
        {
            
            if (value1 < 0)
            {
                _positionType = Convert.ToString(MyEnumOfTrades.Short);
            }
            else
            {
                _positionType = Convert.ToString(MyEnumOfTrades.Long);
            }
            _volume = Math.Abs(value1);
            _priceOflot = value2;
            _price = value3;
            _deposit -= value3;
            _dateTime = value4;

            if (_priceOflot > 170)
                _secCode = Convert.ToString(SecCode.Sber);
            if (_priceOflot > 135 && _priceOflot < 170)
                _secCode = Convert.ToString(SecCode.Lukoil);
            if (_priceOflot < 135)
                _secCode = Convert.ToString(SecCode.Gazp);

            EventNewPosition += PositionChange;
            EventNewPosition("Позиция изменилась");

        }
        private void PositionChange(string str1)
        {
            Console.WriteLine(str1);
        }

        public void ConsoleWrite()
        {   
            Console.WriteLine("===============================================================");
            
            Console.WriteLine("\tТекущий остаток счёта " + _deposit + " рублей.");
                        
            Console.WriteLine("\tНаправление текущей позиции " + _positionType + ".");
            
            Console.WriteLine("\tЦена одного лота в открытой позиции " + _priceOflot + " рублей.");
            
            Console.WriteLine("\tОбщая стоимость позиции " + _price + " рублей");
            
            Console.WriteLine("\tОбщий объём открытой позиции " + _volume + " лотов.");
                        
            Console.WriteLine("\tНазвание инструмента в позиции " + _secCode + ".");
                               
            Console.WriteLine("\tВремя открытия позиции " + _dateTime + ".");
                
            Console.WriteLine("===============================================================");
           
        }
        #endregion

       // Событие изменения позиции
        public delegate void NewPosition(string str1);
        public event NewPosition? EventNewPosition;
    }
    
}
