using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StartOsE.Logging;

namespace StartOsE.Market.Servers.Quik
{
    public class QuikCon
    {
        private QuikDde _quikDde;

        public void Disconnect()
        {
            if (this._quikDde == null)
                return;
            this._quikDde.StopDdeInQuik();
           // this._quikDde.RemoveAll();
            this._quikDde = (QuikDde)null;
        }

        public void Connect(string name)
        {
            if (this._quikDde != null)
                return;
            this._quikDde = new QuikDde(name);
            //this._quikDde.LogMessageEvent += new Action<string, LogMessageType>(this._quikDde_LogMessageEvent);
            //this._quikDde.StatusChangeEvent += new Action<string>(this._quikDde_StatusChangeEvent);
            //this._quikDde.UpdateGlass += new Action<long, object[,], string>(this._quikDde_UpdateGlass);
            //this._quikDde.UpdatePortfoliosSpot += new Action<long, object[,]>(this._quikDde_UpdatePortfolios);
            //this._quikDde.UpdatePositionSpot += new Action<long, object[,]>(this._quikDde_UpdatePosition);
            //this._quikDde.UpdateSecurity += new Action<long, object[,]>(this._quikDde_UpdateSecurity);
            //this._quikDde.UpdateTrade += new Action<long, object[,]>(this._quikDde_UpdateTrade);
            //this._quikDde.UpdatePortfoliosDerivative += new Action<long, object[,]>(this._quikDde_UpdatePortfoliosDerivative);
            //this._quikDde.UpdatePortfoliosSpotNumber += new Action<long, object[,]>(this._quikDde_UpdatePortfoliosSpotNumber);
            //this._quikDde.UpdatePositionDerivative += new Action<long, object[,]>(this._quikDde_UpdatePositionDerivative);
            //this._quikDde.UpdateUnknownTable += new Action<long, object[,], string>(this._quikDde_UpdateUnknownTable);
            //this._quikDde.StopTableUpdated += new Action<long, object[,]>(this._quikDde_StopTableUpdated);
            //this._quikDde.Register();
        }

        private void _quikDde_StopTableUpdated(long arg1, object[,] arg2)
        {
            if (this.Message == null)
                return;
            this.Message("stopOrders", arg1, arg2);
        }

        private void _quikDde_UpdateUnknownTable(long arg1, object[,] arg2, string arg3)
        {
            if (this.Message == null)
                return;
            this.Message("unknown_" + arg3, arg1, arg2);
        }

        public event Action<string, long, object[,]> Message;

        private void _quikDde_StatusChangeEvent(string obj)
        {
            if (this.Message == null)
                return;
            this.Message("status_" + obj, 1L, new object[1, 1]);
        }

        private void _quikDde_LogMessageEvent(string arg1, string arg2)
        {
            if (this.Message == null)
                return;
            this.Message("message_" + arg1, 1L, new object[1, 1]);
        }

        private void _quikDde_UpdateGlass(long arg1, object[,] arg2, string arg3)
        {
            if (this.Message == null)
                return;
            this.Message("marketDepth_" + arg3, arg1, arg2);
        }

        private void _quikDde_UpdatePositionDerivative(long arg1, object[,] arg2)
        {
            if (this.Message == null)
                return;
            this.Message("positionDerivative", arg1, arg2);
        }

        private void _quikDde_UpdatePosition(long arg1, object[,] arg2)
        {
            if (this.Message == null)
                return;
            this.Message("positionSpot", arg1, arg2);
        }

        private void _quikDde_UpdateSecurity(long arg1, object[,] arg2)
        {
            if (this.Message == null)
                return;
            this.Message("security", arg1, arg2);
        }

        private void _quikDde_UpdatePortfolios(long arg1, object[,] arg2)
        {
            if (this.Message == null)
                return;
            this.Message("portfolioSpot", arg1, arg2);
        }

        private void _quikDde_UpdatePortfoliosSpotNumber(long arg1, object[,] arg2)
        {
            if (this.Message == null)
                return;
            this.Message("portfolioSpotNumber", arg1, arg2);
        }

        private void _quikDde_UpdatePortfoliosDerivative(long arg1, object[,] arg2)
        {
            if (this.Message == null)
                return;
            this.Message("portfolioDerivative", arg1, arg2);
        }

        private void _quikDde_UpdateTrade(long arg1, object[,] arg2)
        {
            if (this.Message == null)
                return;
            this.Message("trade", arg1, arg2);
        }
    }

}
