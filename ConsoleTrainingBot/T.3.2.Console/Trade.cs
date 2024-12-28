using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTrainingBot
{
    public enum TypeTrade
    {
        LONG,
        SHORT
    }

    public class Trade
    {

        /// <summary>
        /// Код инструмента 
        /// </summary>
        public string instrument_code;

        /// <summary>
        /// код класса (базового, если есть)
        /// </summary>
        public string class_code;

        /// <summary>
        /// торовый счет
        /// </summary>
        public string trading_account;

        /// <summary>
        /// код клиента
        /// </summary>
        public string client_code;

        /// <summary>
        /// Тип сделки (Лонг, Шорт)
        /// </summary>
        public TypeTrade type_trade;

        /// <summary>
        /// время сделки
        /// </summary>     
        public DateTime data_time = DateTime.MinValue;

        /// <summary>
        /// Цена за одну ценную бумагу
        /// </summary>
        public decimal price = 0;

        /// <summary>
        /// количетсво ценных бумаг в одном лоте
        /// </summary>
        public decimal lot_volume = 0;

        /// <summary>
        /// количетсво лотов в заявке
        /// </summary>
        public decimal lot_quantity = 0;

        /// <summary>
        /// объем сделки (суммарная стоимость)
        /// </summary>
        public decimal volume = 0;

        /*public decimal Volume {
    get { return volume_; }
    set { volume_ = value; }  }*/
public Trade (Trade trade) 
        {
            this.instrument_code = trade.instrument_code;
            this.class_code = trade.class_code;
            this.trading_account = trade.trading_account;
            this.client_code = trade.client_code;
            this.type_trade =trade.type_trade;
            this.data_time = trade.data_time;
            this.price = trade.price;
            this.lot_volume = trade.lot_volume;
            this.lot_quantity = trade.lot_quantity;
            this.volume = trade.volume;
        }

        public Trade()
        {
        }
        public void PrintTrade ()
        {
            Console.WriteLine(
                $"Код инструмента: {instrument_code}; " +
                $"Тип сделки: {type_trade}; " +
                $"Цена: {price}; " +
                $"Количество ценных бумаг: {lot_quantity * lot_volume}; " +
                //$"Код класса: {class_code}; " +
                $"Время сделки: {data_time}; " +
                $"Номер счета: {trading_account}; " +
                $"Объем сделки: {volume}"
                ); 
        }

    }



}
