using ConsoleTrainingBot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTrainingBot
{


    public struct HashPosition
    {
        string instrument_code;
        string trading_account;

        public HashPosition(string instrument_code_external, string trading_account_external)
        {
            instrument_code = instrument_code_external;
            trading_account = trading_account_external;
        }
    }
    internal class PositionParametrs
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
        /// время последнего изменения позиции
        /// </summary>     
        public DateTime changed_position_time = DateTime.MinValue;

        /// <summary>
        /// Средняя цена за одну ценную бумагу для всей позиции
        /// </summary>
        public decimal position_average_price = 0;

        /// <summary>
        /// количетсво ценных бумаг в позиции
        /// </summary>
        public decimal position_quantity = 0;

        /// <summary>
        /// объем позиции (суммарная стоимость)
        /// </summary>
        public decimal position_volume = 0;


        }

    }


    internal class Portfolio
    {
    ///////////////////////////////////////Fields///////////////////////////////////////////
    #region Fields
    //Dictionary<HashPosition, PositionParametrs> totalPositionList = new Dictionary<HashPosition, PositionParametrs>();
    Dictionary<string, PositionParametrs> totalPositionList = new Dictionary<string, PositionParametrs>();
    decimal cash;
    #endregion



    ///////////////////////////////////////Properties///////////////////////////////////////////
    #region Properties

    #endregion
    public string FilePathToOutput { get; set; }

    ///////////////////////////////////////Constructors//////////////////////////////////////////
    #region Constructors

    #endregion


    ///////////////////////////////////////Methods//////////////////////////////////////////
    #region Methods

    public void SubscribeNewTrade(Position publisher)
    {
        // Подписка на событие и передача метода
        publisher.TradeNotify += AddNewTradeToPositionList;

    }



    public void AddNewTradeToPositionList(object sender, MyEventArgs e)
    {
        Console.WriteLine("принято событие");
        //totalPositionList.Add("proba", new PositionParametrs());
        //totalPositionList["proba121"] = new PositionParametrs();
        //PositionParametrs tmp = totalPositionList["proba121"];
        //PositionParametrs tmp22 = totalPositionList["proba333"];
        
        Trade tradeToAddition = e.Trade;
        string positionToChangeKey = tradeToAddition.instrument_code + "_" + tradeToAddition.trading_account;// new HashPosition(tradeToAddition.instrument_code, tradeToAddition.trading_account);
        
        PositionParametrs positionToChange = new PositionParametrs();
        
        if (totalPositionList.ContainsKey(positionToChangeKey))
        {
            positionToChange = totalPositionList[positionToChangeKey];
        } 
        else
        {
            totalPositionList.Add(positionToChangeKey, new PositionParametrs());
            positionToChange = totalPositionList[positionToChangeKey];
        }
               

        if (tradeToAddition.lot_quantity > 0)
        {
            //проверяем существовала ли позиция ранее
            if (positionToChange.instrument_code == null)
            {
                positionToChange.client_code = tradeToAddition.client_code;
                positionToChange.class_code = tradeToAddition.class_code;
                positionToChange.instrument_code = tradeToAddition.instrument_code;
            }
            positionToChange.trading_account = tradeToAddition.trading_account;
            //меняем время работы с позицией
            positionToChange.changed_position_time = tradeToAddition.data_time;
            //рассчитываем новые параметры позиции
            if (tradeToAddition.type_trade == TypeTrade.LONG) // если лонг
            {
                if (positionToChange.position_quantity >= 0)
                {
                    positionToChange.position_volume += tradeToAddition.volume;
                    positionToChange.position_quantity += tradeToAddition.lot_quantity * tradeToAddition.lot_volume;
                }
                else if (positionToChange.position_quantity < 0)
                {
                    decimal new_quantity = positionToChange.position_quantity + tradeToAddition.lot_quantity * tradeToAddition.lot_volume;
                    decimal new_volume = new_quantity <= 0 ?
                                                    positionToChange.position_average_price * System.Math.Abs(new_quantity) :
                                                    new_quantity * tradeToAddition.price;
                    positionToChange.position_quantity = new_quantity;
                    positionToChange.position_volume = new_volume;
                }
                positionToChange.position_average_price = positionToChange.position_volume / System.Math.Abs(positionToChange.position_quantity);
            }
            else //если ШОРТ
            {
                if (positionToChange.position_quantity <= 0)
                {
                    positionToChange.position_volume += tradeToAddition.volume;
                    positionToChange.position_quantity -= tradeToAddition.lot_quantity * tradeToAddition.lot_volume;
                }
                else if (positionToChange.position_quantity > 0)
                {
                    decimal new_quantity = positionToChange.position_quantity - tradeToAddition.lot_quantity * tradeToAddition.lot_volume;
                    decimal new_volume = new_quantity >= 0 ?
                                                    positionToChange.position_average_price * new_quantity :
                                                    System.Math.Abs(new_quantity) * tradeToAddition.price;
                    positionToChange.position_quantity = new_quantity;
                    positionToChange.position_volume = new_volume;
                }
                positionToChange.position_average_price = positionToChange.position_volume / System.Math.Abs(positionToChange.position_quantity);
            }

            //удаляем позицию, если position_quantity = 0 в итоге
            if (positionToChange.position_quantity == 0)
            {
                totalPositionList.Remove(positionToChangeKey);
            }

            PrintCurrentPositionToFile();
            Console.WriteLine("Позиция изменилась");
        }
    }
    public void PrintCurrentPositionToFile() 
    {
        using (StreamWriter writer = new StreamWriter(FilePathToOutput))
        {
            if (totalPositionList.Any())
        {
            
                writer.WriteLine("Текущий состав портфеля:");
                foreach (var position in totalPositionList)
                {
                    writer.WriteLine($"Торговый счет: {position.Value.trading_account}," +
                        $" Код инструмента: {position.Value.instrument_code}," +
                        $" Средняя цена: {position.Value.position_average_price}," +
                        $" Количество ценных бумаг: {position.Value.position_quantity}," +
                        $" Суммарная стоимость: {position.Value.position_volume}");
                }

           
            
        } else
        {
            writer.WriteLine("Портфель пуст");
        }
        }
    }
    
        #endregion
    }
