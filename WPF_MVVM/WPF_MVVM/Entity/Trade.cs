using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM.Entity;

public class Trade
{
    public decimal Volume
    {
        get => _volume;

        set
        {
            _volume = value;

            if (Volume != 0)
            {
                Side = Volume > 0 ? Side.Long : Side.Short;
            }
            else
            {
                return;
            }
        }
    }
    private decimal _volume;

    public decimal Price;
    public Side Side = Side.None;
    public DateTime DateTime = DateTime.MinValue;

}
