using System.Collections.Generic;

namespace StartOsE.Market.Servers.MoexAlgopack.Entity
{
    public class ResponseDepth
    {
        public Orderbook orderbook { get; set; }
    }

    public class Orderbook
    {
        public List<string> columns { get; set; }
        public List<List<string>> data { get; set; }
    }
}
