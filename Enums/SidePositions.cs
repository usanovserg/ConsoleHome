using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome.Enums
{
        /// <summary> Направление позиции (Long(1); None(0); Short(-1)). </summary>
        public enum SidePosition
        {
            Short = -1,
            None = 0,
            Long = 1
        }
}
