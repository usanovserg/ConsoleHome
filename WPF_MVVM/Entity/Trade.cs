using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM.Entity
{
    public class Trade
    {
        public decimal Volume
        {
            get => _volume;
            set
            {
                _volume = value;
                // Автоматическое определение направления сделки
                if (_volume > 0)
                {
                    Side = Side.Long;
                }
                else if (_volume < 0)
                {
                    Side = Side.Short;
                }
            }
        }
        decimal _volume;

        public decimal Price; // Цена сделки

        public Side Side = Side.None; // Направление (Long/Short/None)

        public DateTime DateTime = DateTime.MinValue; // Время сделки

        public string SecName; // Название ценной бумаги
    }
}
 