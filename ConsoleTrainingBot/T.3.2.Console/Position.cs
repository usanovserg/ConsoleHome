using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleTrainingBot
{
    internal class Position : ITimerInterface
    {        
        ///////////////////////////////////////Fields///////////////////////////////////////////
        #region Fields

        IntervalTimerManager<Position> timer;
        Trade trade = new Trade();

        #endregion

        ///////////////////////////////////////Properties///////////////////////////////////////////
        #region Properties

        #endregion


        ///////////////////////////////////////Constructors//////////////////////////////////////////
        #region Constructors

        public Position(string instrument_code, string class_code, string trading_account, string client_code, decimal lot_volume)
        {

            trade.instrument_code = instrument_code;
            trade.class_code = class_code;
            trade.trading_account = trading_account;
            trade.client_code = client_code;
            trade.lot_volume = lot_volume;

            timer = new IntervalTimerManager<Position>(this, 1000); // раз в секунду

        }

        #endregion


        ///////////////////////////////////////Methods//////////////////////////////////////////
        #region Methods

        public void TimerElapsed(object sender, ElapsedEventArgs e)
        {

            Random random = new Random();
            int num = random.Next(-10, 10);
            if (num > 0)
            {
                trade.type_trade = TypeTrade.LONG;
            }
            else if (num < 0)
            {
                trade.type_trade = TypeTrade.SHORT;
            }


            trade.price = random.Next(95000, 120000); // будем реалистами
            trade.lot_quantity = Math.Abs(num);
            trade.volume = trade.lot_quantity * trade.price* trade.lot_volume;
            trade.data_time = DateTime.Now;
            trade.PrintTrade();
        }

        #endregion


    }



}
