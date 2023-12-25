using ConsoleHome.Enums;
using System.Timers;
using Timer = System.Timers.Timer;

namespace ConsoleHome
{
    /* 
     Класс для генерации сделок по выбранному инструменту, т.е. создание экземпляров cTrades с присвоением id 
     */


    public class cExchange
    {
        public delegate void TradeHandler(cTrade trade);
        public event TradeHandler NewTrade;


        #region FIELDS
        public string AssetName 
        { get { return _assetName; }
            set { _assetName = value; }
        } string _assetName = "";
        public decimal CurrentPrice 
        {   get { return _currentPrice; }
            set { _currentPrice = value; }
        } decimal _currentPrice = 0;
        public int ATR 
        { get { return _atr; } set { _atr = value; } } int _atr = 0;

        //структура для заполнения значений для инициализации
        public struct Initials
        {
            public Assets nameInit;
            public decimal priceInit;
            public int atrInit;
        }
        Initials init = new Initials();
        //сразу создаём список Initials from Enums_Assets
        List<Initials> listOfAssets = new List<Initials>();

        #endregion

        #region Methods
        //Constructor
        public cExchange(Assets asset)
        {
            foreach (Assets name in Enum.GetValues(typeof(Assets)))
            {
                init.nameInit = name;
                switch (name)
                {
                    case Assets.sber:
                        _currentPrice = 250;
                        _atr = 3;
                        break;
                    case Assets.gazp:
                        _currentPrice = 160;
                        _atr = 3;
                        break;
                    case Assets.gmkn:
                        _currentPrice = 17000;
                        _atr = 3;
                        break;
                    default:
                        break;
                }
                init.priceInit = _currentPrice;
                init.atrInit = _atr;

                listOfAssets.Add(init);
            }
            GetLastInfoByAsset(asset);
            
            
            //ConnectionCreatedNotify.Invoke($"Коннект произведен для инструмента {AssetName}, последняя цена {startPrice}, средний ATR {atr}%");
        }
        #endregion

        //Запуск эмуляции трейдов
        public void Start() 
        {
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Create_New_Trade;
            timer.Start();
        }

        private void Create_New_Trade(object? sender, ElapsedEventArgs e)
        {
            uint totalCountOfAssets = (uint)Enum.GetValues(typeof(Assets)).Length;
            uint randomAsset = (uint)(new Random()).Next((int)totalCountOfAssets);
            Assets curAsset = (Assets)Enum.ToObject(typeof(Assets), randomAsset);          
             
            cTrade trade = new cTrade(DateTime.Now, curAsset);
            GetLastInfoByAsset(curAsset);
            CurrentPrice += Math.Round(((decimal)(new Random()).Next(-ATR, ATR)/100*CurrentPrice),2);
            trade.price = CurrentPrice;
            trade.Lot = new Random().Next(100);

            //вызываем событие, чтобы уведомить подписчиков о новой сделке
            OnNewTrade(trade);
        }

        //здесь описываем метод для вызова события
        public void OnNewTrade(cTrade trade)
        {
            NewTrade?.Invoke(trade);
        }

        void GetLastInfoByAsset(Assets asset)
        {
            init = listOfAssets.Find(s => s.nameInit == asset);
            CurrentPrice = init.priceInit;
            ATR = init.atrInit;
            switch (asset) 
            {
                case Assets.sber:
                    AssetName = "sber";
                    break;
                case Assets.gazp:
                    AssetName = "gazp";
                    break;
                case Assets.gmkn:
                    AssetName = "gmkn";
                    break;
            }
        }
    }
}
