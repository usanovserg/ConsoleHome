using OsEngine.Entity;
using OsEngine.Market;
using OsEngine.RobotEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSEngine.RobotEntity
{
    public class ConfigRobot
    {
        public ConfigRobot() { }
        public string Header { get; set; }
        public ServerType ServerType { get; set; }
        public string SecurityName { get; set; }
        public string SecurityClass { get; set; }
        public Portfolio SelectedPortfolio { get; set; } //DZ 3-30 номер счета
        public List<Order> Orders { get; set; }
        public List<MyTrade> MyTrades { get; set; }
        public List<LimitOrder> LimitOrders { get; set; } //DZ 3-31 номер счета
    }
}