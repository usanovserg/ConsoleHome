/*
 * Your rights to use code governed by this license https://github.com/AlexWan/StartOsE/blob/master/LICENSE
 * Ваши права на использование кода регулируются данной лицензией http://o-s-a.net/doc/license_simple_engine.pdf
*/

using System;
using StartOsE.Logging;

namespace StartOsE.OsTrader.Panels.Tab
{
    /// <summary>
    /// _tab interface for robot panel
    /// </summary>
    public interface IIBotTab
    {
        /// <summary>
        /// source type
        /// </summary>
        BotTabType TabType { get; }

        /// <summary>
        /// Remove tab and all child structures
        /// </summary>
        void Delete();

        /// <summary>
        /// Clear
        /// </summary>
        void Clear();

        /// <summary>
        /// Stop drawing this robot
        /// </summary>
        void StopPaint();

        /// <summary>
        /// Unique robot name
        /// </summary>
        string TabName { get; set; }

        /// <summary>
        /// _tab number
        /// </summary>
        int TabNum { get; set; }

        /// <summary>
        /// are events sent to the top from the tab?
        /// </summary>
        bool EventsIsOn { get; set; }

        /// <summary>
        /// are events sent to the top from the tab?
        /// </summary>
        bool EmulatorIsOn { get; set; }

        /// <summary>
        /// Time of the last update of the candle
        /// </summary>
        DateTime LastTimeCandleUpdate { get; set; }

        /// <summary>
        /// Source removed
        /// </summary>
        event Action TabDeletedEvent;

        /// <summary>
        /// New log message event
        /// </summary>
        event Action<string, LogMessageType> LogMessageEvent;
    }
}
