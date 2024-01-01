using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Lesson_6
{/// <summary>
/// Класс для формирования текущей позиции и передачи данных через события во внешние обработчики 
/// </summary>
    public class StarTrading
    {

        // ==========================================Поля===================================================

        #region Fields

        private string _positionTypeValue; // Тип открытой позиции
        private decimal _deposit = 10000; // Размер стартового депозита
        private decimal _priceOflot = 0; // Цена лота
        private decimal _price = 0; // Общая стоимость открытой позиции
        private decimal _volume = 0; // Объём открытой позиции
        private string _secCodeValue; // Название инструмента на бирже
        private DateTime _dateTime = DateTime.Now; //Время открытия позиции
        private Random random = new Random();

        #endregion


        // =====================================Конструктор класса==========================================

        public StarTrading()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 4000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
        
        //======================================Сообщения для событий=======================================

        #region TextOfEvent

        private string _newVolume = ("\tНовый объём позиции: ");
        private string _positionType = ("\tТекущее направлении позиии: ");
        private string _priceOfLoat = ("\tТекущая цена лота: ");
        private string _priceOfPosition = ("\tОбщая стоимость открытой позиции: ");
        private string _depo = ("\tОстаток счёта: ");
        private string _secCode = ("\tНаименование эмитентна: ");
        private string _classCode = ("\tТип ценных бумаг: Акции обычные");
        private string _timeNow = ("\tВремя открытия позиции: ");
        private string _portfoloiNumber = ("\tНомер клиентского портфеля: 1");

        #endregion


        //===============Метод для формирования занчений позиций и отправки событий=========================

        #region Method

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {

                //Задаём общий объём позиции и отправляем событие
                _volume = random.Next(-20, +20);
           
                // Определяем направление позиции и отправляем событие
                if (_volume > 0)
                {
                    MyEnumOfTrades positiontype = MyEnumOfTrades.Long;
                    _positionTypeValue = Convert.ToString(positiontype);
               
                }
                else
                {
                    MyEnumOfTrades positiontype = MyEnumOfTrades.Short;
                    _volume = Math.Abs(_volume);
                    _positionTypeValue = Convert.ToString(positiontype);
                }

                // Определяем стоимость одного лота случайным образом и отправляем событие
                _priceOflot = random.Next(100, 200);
           

                // Определяем общуую стоимость позции и отправляем событие
                _price = _priceOflot * _volume;
            
                // Определяем остаток депо и отправляем события
                _deposit -= _price;
           
                //Определяем тип  актива и отправляем событие
                if (_priceOflot > 170)
                    _secCodeValue = Convert.ToString(SecCode.Sber);
                if (_priceOflot > 135 && _priceOflot < 170)
                    _secCodeValue = Convert.ToString(SecCode.Lukoil);
                if (_priceOflot < 135)
                    _secCodeValue = Convert.ToString(SecCode.Gazp);

                

        //=======События для внешних обработчиков в формате СТРИНГ для вывода на экран и записи=============

                #region EVENTS

                NewPositionEvent?.Invoke("\tОткрыта Новая Позиция!!!");
                NewPositionEvent?.Invoke("\t===============================================================");
                NewPositionEvent?.Invoke(_newVolume + Convert.ToString(_volume));
                NewPositionEvent?.Invoke(_positionType + (_positionTypeValue));
                NewPositionEvent?.Invoke(_priceOfLoat + Convert.ToString(_priceOflot));
                NewPositionEvent?.Invoke(_priceOfPosition + Convert.ToString(_price));
                NewPositionEvent?.Invoke(_depo + Convert.ToString(_deposit));
                NewPositionEvent?.Invoke(_secCode + Convert.ToString(_secCodeValue));
                NewPositionEvent?.Invoke(_classCode);
                NewPositionEvent?.Invoke(_timeNow + Convert.ToString(_dateTime));
                NewPositionEvent?.Invoke(_portfoloiNumber);
                NewPositionEvent?.Invoke("\t===============================================================");

                NewPositionData?.Invoke(Convert.ToString(_dateTime) + "\t" + _secCodeValue + "\t" + Convert.ToString(_volume) + "\t" + Convert.ToString(_price));

            #endregion


        }
        #endregion


        //=========================================Делегаты и события=======================================
        #region Delegate&Event

        public event Positioninfo NewPositionEvent;
        public event NewPositionCount NewPositionData;

        public delegate void Positioninfo(string str1);
        public delegate void NewPositionCount(string newValue);
        #endregion
    }
}
