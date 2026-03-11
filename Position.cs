using ConsoleHome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public enum PositionType
    {
         Buy,
         Sell

    }

    public enum CloseReason
    {
        Unknown,
        RobotSignal,
        StopLoss,
        TakeProfit,
        TrailingStop,
        Manual,
        MarginCall,
        Expiration
    }
    
    public class Position
    {
        // ==================================== Идентификация ====================================
        public long Ticket  // главный ключ, по которому робот находит, модифицирует (двигает стоп-лосс) или закрывает конкретную сделку на стороне брокера
        {
            get; 

            set; 
        }

        public string Symbol // Наименование торгуемого инструмента
        {
            get;

            set;
        }
        public long MagicNumber  // Магический номер это уникальный цифровой идентификатор, который разработчик присваивает своему торговому роботу.
                                 // Это механизм безопасности и изоляции. Он гарантирует, что ваш робот будет управлять только теми деньгами и сделками, которые он сам же и открыл.
        {
            get;

            set;
        }
        public string Comment  // Комментраий. Напиример, метка стратегии.
        {
            get;

            set;
        }

        // ==================================== Параметры сделки ====================================
        public PositionType Type // тип позиции: Лонг / Шорт
        {
            get;

            set;
        }
        public double Volume // Объем позиции для валют
        {
            get;

            set;
        }

        public double Lot // Объем позиции для Акций и Фьючерсов
        {
            get;

            set;
        }

        public double VolumeInitial // Исходный объем (для учета частичного закрытия)
        {
            get;

            set;
        }

        public decimal OpenPrice // Цена открытия
        {
            get;

            set;
        }
        public DateTime OpenTime // Время открытия 
        {
            get;

            set;
        }

        // ==================================== Управление рисками ====================================
        public decimal StopLoss // Ограничение убытков
        {
            get;

            set;
        }
        public decimal TakeProfit // Фиксация прибыли
        {
            get;

            set;
        }

        public decimal TrailingStop // Трейлинг-стоп. Подтягиваемый стоп.
        {
            get;

            set;
        }


        public decimal TrailingStopOffset // Отступ для трейлинг-стопа
        {
            get;

            set;
        }

        // ==================================== Финансы и Издержки ====================================

        public decimal ContractSize // размер Контракта или Лота
        {
            get;

            set;
        }


        public decimal Commission // Накопленная комиссия
        {
            get;

            set;
        }
        public decimal Swap       // Накопленный своп
                                  // Swap — это стоимость удержания позиции во времени
        {
            get;

            set;
        }
        public decimal MarginUsed  // Сколько маржи заморожено
        {
            get;

            set;
        }

        // ==================================== Динамика позиции ====================================
        public decimal CurrentPrice  // текущая цена
        {
            get;

            set;
        }

        // ==================================== Закрытие (заполняется после закрытия) ====================================
        public bool IsClosed // Позиция закрыта.
        {
            get;

            set;
        }
        public DateTime? CloseTime // Время закрытия позиции
        {
            get;

            set;
        }
        public decimal? ClosePrice // Цена закрытия позиции
        {
            get;

            set;
        }
        public CloseReason? CloseReason  // Причина закрытия сделки. Смотри Enum ниже.
        {
            get;

            set;
        }

        // ==================================== Вычисляемая чистая прибыль ====================================
       /* public decimal NetProfit // Общая прибыль
        {
            get
            {
                if (!IsClosed && CurrentPrice == 0) 
                    return 0;

                decimal price = IsClosed ? ClosePrice.Value : CurrentPrice;
                decimal grossProfit = CalculateGrossProfit(price);

                // Формула: Валовая прибыль - Комиссия - Своп
                return grossProfit - Commission - Swap;
            }
        }
       */
        
        private decimal CalculateGrossProfit(decimal closePrice)
        {
            decimal diff;


            if (Type == PositionType.Buy)
            {
                // Для покупки зарабатываем, если цена закрытия выше цены открытия
                diff = closePrice - OpenPrice;
            }
            else if (Type == PositionType.Sell)
            {
                // Для продажи зарабатываем, если цена закрытия ниже цены открытия
                diff = OpenPrice - closePrice;
            }
            else
            {
                return 0; // На случай некорректного типа
            }

            // Используем ContractSize вместо жесткого 100000m
            return diff * (decimal)Volume * (decimal)ContractSize;
        }

        // ==================================== Метод для частичного закрытия ====================================
        public void ClosePartial(double volumeToClose, decimal price)
        {
            if (volumeToClose >= Volume)
            {
                // Полное закрытие
                Volume = 0;
                IsClosed = true;
                CloseTime = DateTime.Now;
                ClosePrice = price;
                CloseReason = 0;
                Console.WriteLine($"✅ Позиция #{Ticket} полностью закрыта по цене {price:F5}");
            }
            else
            {
                // Частичное закрытие
                var closedVolume = volumeToClose;
                Volume -= volumeToClose;

                Console.WriteLine($"⚡ Частичное закрытие #{Ticket}: {closedVolume} лот. по {price:F5}");
                Console.WriteLine($"   Остаток позиции: {Volume} лот.");
            }
        }

        
        private Position() { }


        public static Position CreateNewPosition(
           long ticket,
           string symbol,
           PositionType type,
           double volume,
           decimal price,
           long magic = 0,
           double contractSize = 100000,
           decimal stopLoss = 0,
           decimal takeProfit = 0)
        {
            var position = new Position
            {
                Ticket = ticket,
                Symbol = symbol,
                Type = type,
                Volume = volume,
                VolumeInitial = volume,
                OpenPrice = price,
                CurrentPrice = price,
                OpenTime = DateTime.Now,
                MagicNumber = magic,
                ContractSize = (decimal)contractSize,
                StopLoss = stopLoss,
                TakeProfit = takeProfit,
                Commission = 0,
                Swap = 0
            };

            PrintPositionInfo(position);
            return position;
        }

        // ==================================== Вывод информации в консоль ====================================
        private static void PrintPositionInfo(Position pos)
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"| НОВАЯ ПОЗИЦИЯ ОТКРЫТА");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"| Ticket:      {pos.Ticket}");
            Console.WriteLine($"| Инструмент:  {pos.Symbol}");
            Console.WriteLine($"| Направление: {pos.Type} {(pos.Type == PositionType.Buy ? "LONG" : "SHORT")}");
            Console.WriteLine($"| Объем:       {pos.Volume} лот.");
            Console.WriteLine($"| Цена входа:  {pos.OpenPrice:F5}");
            Console.WriteLine($"| ContractSz:  {pos.ContractSize}");
            Console.WriteLine($"| Время:       {pos.OpenTime:HH:mm:ss}");
            Console.WriteLine($"| Magic:       {pos.MagicNumber}");

            if (pos.StopLoss > 0)
                Console.WriteLine($"| StopLoss:    {pos.StopLoss:F5}");
            if (pos.TakeProfit > 0)
                Console.WriteLine($"| TakeProfit:  {pos.TakeProfit:F5}");

            Console.WriteLine(new string('-', 50));
        }

        

    }
}






