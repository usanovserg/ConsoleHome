using Capital.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Capital_comments.Constants
{
    internal class ColorConstants
    {
        public static Dictionary<StrategyType, SolidColorBrush> GRAPH_COLORS = new Dictionary<StrategyType, SolidColorBrush>() {
            { StrategyType.FIX, Brushes.Red },
            { StrategyType.CAPITALIZATION, Brushes.Blue },
            { StrategyType.PROGRESS, Brushes.Green },
            { StrategyType.DOWNGRADE, Brushes.Fuchsia }
        };

        public static SolidColorBrush GetGraphColor(StrategyType strategyType) 
        {
            return GRAPH_COLORS.GetValueOrDefault(strategyType, Brushes.Black);
        }
    }
}
