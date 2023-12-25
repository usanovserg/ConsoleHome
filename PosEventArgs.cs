using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_6
{
    public class PosEventArgs : EventArgs
    {
        public int PosValue { get; private set; }
        public PosEventArgs(int posValue)
        {
            PosValue = posValue;
        }
    }
}
