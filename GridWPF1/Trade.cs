using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace GridWPF1
{
    public class Trade
    {
        public decimal Price = 0;
        public string SecCode = "";
        public string ClassCode = "";
        public string Portfolio = "";
        public Side Side;

        public decimal Last;

        public DateTime DateTime = DateTime.MinValue;

        /// <summary>
        /// Объем сделки
        /// </summary>

        public decimal Volume { get; set; } = 0;
    }
}
