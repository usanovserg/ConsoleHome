using System.Windows.Forms;
using OsEngine.Charts.CandleChart.Indicators;
using OsEngine.Entity;
using OsEngine.OsTrader.Panels;
using OsEngine.OsTrader.Panels.Attributes;
using OsEngine.OsTrader.Panels.Tab;

namespace OsEngine.Robots._MyRobots.BotMvvmTest
{
    [Bot("BotMvvmTest")]
    public class BotMvvmTest  :BotPanel
    {
        public BotTabSimple _tab;
        public StrategyParameterDecimal Volume;
        public StrategyParameterCheckBox OnOff;

        public BotMvvmTest(string name, StartProgram startProgram) : base(name, startProgram)
        {
            TabCreate(BotTabType.Simple);
            TabCreate(BotTabType.Simple);
            _tab = TabsSimple[0];

            OnOff = CreateParameterCheckBox("OnOff", false); 
            Volume = CreateParameter("Volume", 1m, 1, 10, 1); 
        }

        public override string GetNameStrategyType()
        {
            return nameof(BotMvvmTest);
        }

        public override void ShowIndividualSettingsDialog()
        {
            if (OnOff.CheckState == CheckState.Unchecked) return;
            
            BotMvvmTestUI botMvvmTestUIwindow = new(this);
            botMvvmTestUIwindow.Show();
        }
    }
}
