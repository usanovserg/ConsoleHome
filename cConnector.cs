using ConsoleHome.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class cConnector
    {
        bool[] isConnected = {false, false};
        Assets conectorAsset;
        //Объявление делегата и его переменной
        public delegate void ConnectionDelegate(string message);
        public ConnectionDelegate? ConnectionNotifyHandler { get; set; }
        public delegate void GetNewTradeHandler(cTrade trade);
        public event GetNewTradeHandler NewTradeNotification;

        cExchange? exchange; //объявляем переменную cExchange
        public void Connect(Assets asset)
        {
            exchange = new cExchange(asset);
            if (exchange != null)
            {
                isConnected[0] = true;
                conectorAsset = asset;
                ConnectionNotifyHandler($"Есть контакт для {exchange.AssetName} с посл.ценой {exchange.CurrentPrice} и ср.ATR {exchange.ATR}");
            }
        }

        //метод для запуска трансляции сделок, который по идее дальше должен передать только отобранные сделки для выбранного инструмента
        public void Start()
        {
            if (exchange != null && isConnected[0])
            {
                exchange.NewTrade += HandleNewTrade;
                isConnected[1] = true;
                exchange.Start();
            }    
        }

        List<cTrade> trades = new List<cTrade>(); //список сделок, полученных от cExchange
        public void HandleNewTrade(cTrade trade) 
        {
            //Console.WriteLine($"Прошла сделка по {trade.Asset.ToString()} на {trade.Lot.ToString()} @ {trade.price.ToString()}" );

            if (trade.Asset == conectorAsset)
            {
                //Console.WriteLine($"Моя сделка по {trade.Asset.ToString()} @ {trade.price.ToString()}");
                trades.Add(trade);
                OnNewMyTrade(trade);

            }
        }
        void OnNewMyTrade(cTrade trade)
        { 
            NewTradeNotification?.Invoke(trade);
        }
    }
}
