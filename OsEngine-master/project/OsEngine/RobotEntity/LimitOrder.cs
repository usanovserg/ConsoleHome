using OsEngine.Entity;
using OsEngine.Robots.FrontRunner.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsEngine.RobotEntity
{
    public class LimitOrder : BaseVM
    {
        public decimal PriceOrder
        {
            get => _priceOrder;
            set
            {
                _priceOrder = value;
                OnPropertyChanged(nameof(PriceOrder));
            }
        }
        decimal _priceOrder;

        public decimal Volume
        {
            get => _volume;
            set
            {
                _volume = value;
                OnPropertyChanged(nameof(Volume));
            }
        }
        decimal _volume;

        public int NUmberUser
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged(nameof(NUmberUser));
            }
        }
        int _number;

        public OrderStateType Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        OrderStateType _status;
    }
}
