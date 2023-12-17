using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    /// <summary>
    /// Информация о сделке
    /// </summary>
    public class Trade
    {
        public enum Direction
        {
            Buy,
            Sell
        }

        public Direction direction = Direction.Buy;             // направление сделки

        public decimal price = 0;                               // цена

        public decimal qty = 0;                                 // кол-во (в лотах)

        public decimal value = 0;                               // сумма

        public string account = TradingAccount.Account;         // торговый счет

        public uint clientCode = TradingAccount.ClientCode;     // код клиента

        public DateTime dateTime;                               // дата

        public string secCode = "";                             // код инструмента

        public string classCode = "";                           // код класса инструмента

        //number - номер сделки; order - номер заявки; brokerRef - комментарий сделки;
        //exchangeComission - комиссия биржи; brokerComission - комиссия брокера;
        //currency - валюта сделки; id - идентификатор транзакции

    }
}
