using OsEngine.Entity;
using OsEngine.OsTrader.Panels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.VisualStyles;

namespace OsEngine.Robots.TestRobot3
{
    public class VM //:INotifyPropertyChanged
    {
        public VM(TestRobot3 robot) 
        {
            _robot = robot;
            _lot = (StrategyParameterInt)_robot.Parameters[0];
            _stop = (StrategyParameterInt)_robot.Parameters[1];
            _take = (StrategyParameterInt)_robot.Parameters[2]; 
            _mode = (StrategyParameterString)_robot.Parameters[3]; 
        }

        public string LotName 
        { 
            get { return _lot.Name; } 
        }
        public int LotValue 
        { 
            get { return _lot.ValueInt; } 
            set { _lot.ValueInt = value; } 
        }

        public string StopName 
        { 
            get { return _stop.Name; } 
        }
        public int StopValue
        { 
            get { return _stop.ValueInt; }
            set { _stop.ValueInt = value; }
        }

        public string TakeName 
        { 
            get { return _take.Name; } 
        }
        public int TakeValue 
        { 
            get { return _take.ValueInt; } 
            set { _take.ValueInt = value; } 
        }

        public string ModeName 
        { 
            get { return _mode.Name; } 
        }
        public string ModeValue 
        { 
            get { return _mode.ValueString; }
            set { _mode.ValueString = value; } 
        }

        public List<string> ModeValues
        { 
            get { return _mode.ValuesString; } 
        }

        private StrategyParameterInt _lot;
        private StrategyParameterInt _stop;
        private StrategyParameterInt _take;
        private StrategyParameterString _mode;

        private TestRobot3 _robot;

       // public event PropertyChangedEventHandler PropertyChanged;
    }
}
