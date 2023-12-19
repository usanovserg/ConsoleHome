using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome.Entity
{
    public enum Side // публичный enum, который можно использовать во всех классах проекта
    {
        Sell = -1,

        Buy,

        None,

        Short,

        Long
    }
}
