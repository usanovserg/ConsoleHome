using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{

            /// <summary> Направление позиции (Long; None; Short). </summary>
            public enum TypeTrade
            {
                Short = -1,
                None = 0,
                Long = 1
            }

            /// <summary> Направление сделки (Transaction): Sell (-1); None (0); Bay (1). </summary>
            public enum TypeTransaction
            {
                Sell = -1,
                None = 0,
                Buy = 1
            }

            /// <summary> Сведения об изменении позиции: NotChanged (не изменилась); Changed (изменилась). </summary>
            public enum PositionChangeType
            {
                NotChanged,
                Changed                 
            }

}

