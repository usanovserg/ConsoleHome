using ConsoleHome.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{                                                                                         // В текущем файле все данные о позиции "упаковываются" в один объект;
                                                                                          // этот объект можно расширять в дальнейшем (добавлять новые данные);
    /// <summary> Данные, передаваемые при изменении позиции. </summary>
    public class PositionChangedEventArgs
    {
        ///  <summary> Сведения об изменении позиции (переменная).  </summary>
        public PositionChangeType ChangeType { get; }

                                                                                          // Основные данные о позиции;
        ///   <summary> Объем позиции (лот) после её изменения. </summary>
        public decimal NewVolume { get; }
        
        ///   <summary> Средняя цена по которой открыта позиция. </summary>
        public decimal AveragePrice { get; }

        ///   <summary> Текущее гарантийное обеспечение для позиции. </summary>
        public decimal InitialMargin { get; }

        ///   <summary> Зафиксированный финансовый результат по частично закрытой позиции (по последней сделке). </summary>
        public decimal PnL { get; }

        ///   <summary> Зафиксированный финансовый результат накопительным итогом (по всей позиции). </summary>
        public decimal TotalPnL { get; }

                                                                                          // Данные о сделке;
        public SideTransaction TradeSide { get; }

        ///   <summary> Объем сделки (лот). </summary>
        public decimal TradeVolume { get; }

        ///   <summary> Цена совершения сделки. </summary>
        public decimal TradePrice { get; }

                                                                                          // Время сделки;
        ///   <summary> Время совершения сделки. </summary>
        public string DealTime { get; }


                                                                                          // Конструктор передаваемых сведений о позиции (передаваемых по событию);
        public PositionChangedEventArgs(
            PositionChangeType changeType,
            decimal newVolume,
            decimal averagePrice,
            decimal initialMargin,
            decimal pnl,
            decimal totalPnl,
            SideTransaction tradeSide,
            decimal tradeVolume,
            decimal tradePrice,
            string dealTime)
        {
            ChangeType = changeType;                                                      // Присвоение значений переменным (в конструкторе);
            NewVolume = newVolume;
            AveragePrice = averagePrice;
            InitialMargin = initialMargin;
            PnL = pnl;
            TotalPnL = totalPnl;
            TradeSide = tradeSide;
            TradeVolume = tradeVolume;
            TradePrice = tradePrice;
            DealTime = dealTime;
        }
    }



    //public class PositionChangedEventArgs
    //{
    //    public PositionChangeType ChangeType { get; }                                        // Данные об изменении (не изменении) позиции;
    //    public decimal NewVolume { get; }                                                    // Данные об объеме позиции (lots);
    //    // Перечень данных для передачи по событию можно расширить (здесь);

    //    public PositionChangedEventArgs(PositionChangeType changeType, decimal newVolume)
    //    {
    //        ChangeType = changeType;
    //        NewVolume = newVolume;
    //    }
    //}
}
