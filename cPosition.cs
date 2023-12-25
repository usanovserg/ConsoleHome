using ConsoleHome.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timer = System.Timers.Timer;

namespace ConsoleHome
{
    public class cPosition
    {

        //объявление делегатов
        public delegate void UpdatePosition(string message);
        public UpdatePosition? UpdateNotify { get; set; }
        public decimal Lot
        {
            get { return _lot; }
            set { _lot = value; }
        }        decimal _lot = 0;

        public decimal PricePosition 
        {
            get { return _pricePosition; }
            set { _pricePosition = value; }
        }        decimal _pricePosition;

        public Assets AssetName
        {
            get { return _assetName; }
            set { _assetName = value; }
        } Assets _assetName;

        decimal tp, sl = 0;


        public void AddPosition(Assets name, decimal price, decimal lot)
        { 
            AssetName = name;
            PricePosition = price;
            Lot = lot;
            tp = PricePosition + PricePosition / 100 * 3; //+3%
            sl = PricePosition - PricePosition / 100 * 1; //-1%
            UpdateNotify($"Новыя позиция в {AssetName} на {Lot} @ {PricePosition} тейк: {tp} стоп {sl}");
        }
    }
}
