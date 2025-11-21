using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using OsEngine.Commands;
using OsEngine.Robots.FrontRunner.Models;

namespace OsEngine.Robots.FrontRunner.ViewModels
{
    public class VM:BaseVM
    {
        public VM(FrontRunnerBot bot) 
        { 
           _bot = bot; 
        }

        #region Fields ================================================================

        private FrontRunnerBot _bot;


        #endregion

        #region Properties ==============================================================
        /// <summary>
        /// Крупный объем заявок в стакане 
        /// </summary>
        public decimal BigVolume
        {
            get => _bot.BigVolume;
            set
            {
                _bot.BigVolume = value;
                OnPropertyChanged(nameof(BigVolume));
            }
        }
        /// <summary>
        /// Отступ от (контрольного объема)
        /// </summary>
        public int Offset
        {
            get => _bot.Offset;
            set
            {
                _bot.Offset = value;
                OnPropertyChanged(nameof(Offset));
            }
        }
        /// <summary>
        /// TakeProfit в шагах цены
        /// </summary>
        public int Take
        {
            get => _bot.Take;
            set
            {
                _bot.Take = value;
                OnPropertyChanged(nameof(Take));
            }
        }
        /// <summary>
        /// Лот
        /// </summary>
        public decimal Lot
        {
            get => _bot.Lot;
            set
            {
                _bot.Lot = value;
                OnPropertyChanged(nameof(Lot));
            }
        }
        
        public Edit Edit
        {
            get => _bot.Edit;
            set
            {
                _bot.Edit = value;
                OnPropertyChanged(nameof(Edit));
            }
        }        

        #endregion

        #region Commands ==============================================================
        private DelegateCommand commandStart;

        public ICommand CommandStart
        {
            get
            {
                if (commandStart == null)
                {
                    commandStart = new DelegateCommand(Start);
                }
                return commandStart;
            }
        }
        #endregion

        #region Methods ================================================================

        private void Start(object obg)
        {
            if (Edit == Edit.Start)
            {
                Edit = Edit.Stop;
            }
            else
            {
                Edit = Edit.Start;
            }

        }

        #endregion
    }

}
