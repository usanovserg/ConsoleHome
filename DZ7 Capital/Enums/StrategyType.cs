using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital.Enums
{
    public enum StrategyType
    {
        /// <summary>
        /// фиксированный размер лота
        /// </summary>
        FIX,
        /// <summary>
        /// с капитализацией прибыли
        /// </summary>
        CAPITALIZATION,
        /// <summary>
        /// прогрессивное увеличение лота
        /// </summary>
        PROGRESS,
        /// <summary>
        /// уменьшение лота после убытков
        /// </summary>
        DOWNGRADE
    }
}
