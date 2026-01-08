using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    /// <summary> Данные, передаваемые при изменении позиции. </summary>
    public class PositionChangedEventArgs
    {
        public PositionChangeType ChangeType { get; }                                        // Данные об изменении (не изменении) позиции;
        public decimal NewVolume { get; }                                                    // Данные об объеме позиции (lots);
        // Перечень данных для передачи по событию можно расширить (здесь);

        public PositionChangedEventArgs(PositionChangeType changeType, decimal newVolume)
        {
            ChangeType = changeType;
            NewVolume = newVolume;
        }
    }
}
