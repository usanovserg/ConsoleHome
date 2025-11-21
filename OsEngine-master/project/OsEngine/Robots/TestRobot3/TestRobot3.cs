using OsEngine.Entity;
using OsEngine.OsTrader.Panels;
using OsEngine.OsTrader.Panels.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsEngine.Robots.TestRobot3
{
    [Bot("TestRobot3")]
    public class TestRobot3 : BotPanel
    {

        public TestRobot3(string name, StartProgram startProgram) : base(name, startProgram)
        {
            TabCreate(BotTabType.Simple);

            TabCreate(BotTabType.Simple);

            CreateParameter("Lot", 115, 1, 20, 2);
            CreateParameter("Stop", 55, 1, 20, 2);
            CreateParameter("Take", 35, 1, 20, 2);
            CreateParameter("Mode", "Edit", new[] {"Edit", "Trading"});
        }

        public override string GetNameStrategyType()
        {
            return "TestRobot3";
        }

        public override void ShowIndividualSettingsDialog()
        {
            TestRobot3Window testRobot3Window = new TestRobot3Window(this);
            
            testRobot3Window.Show();
        }
    }
}
