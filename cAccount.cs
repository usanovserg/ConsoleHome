using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class cAccount
    {
        #region FIELDS
        uint id = 0;                                //номер счёта
        public decimal Cash                         //кол-во доступных денег 
        {
            get { return _cash; } 
            set 
            {
                if(value>=0) _cash = value;
            } 
        }   decimal _cash = 0;
        
        List<string> assets = new List<string>();   //список бумаг на счёте
        #endregion



        #region Methods
        //_Constructor
        public cAccount(uint sum) { id++; this.Cash += sum; } //при создании экземпляра класса, присваиваем id    

        public decimal Add(int sum) 
        {
            if (sum > 0)
            {
                this.Cash += sum;
                Console.WriteLine($"Счёт пополнен на сумму {sum.ToString()} . Баланс счёта: {this.Cash}");
            }
            else 
            {
                Console.WriteLine($"Недопустимый ввод суммы {sum.ToString()} . Баланс счёта: {this.Cash}" );
            }
            return this.Cash;
        }

        //Запрос баланса счёта
        public decimal RequestBalance()
        {
            return Cash;
        }
        #endregion

        //Запрос номера счёта
        public uint RequestID() 
        {
            return id;
        }
    }
}
