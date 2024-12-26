using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Connector
    {
        public delegate void newTradeEvent();
        public event newTradeEvent NewTradeEvent;    //без event это делегат, с ним стает событием

        public List<Trade> Trades = new List<Trade>();


        //============================================================= Methods ========================================
        #region Methods

        private void NewTrade(Trade trade)
        {
            Trades.Add(trade);
            NewTradeEvent();
        }

        public void Connect()
        {
            Console.WriteLine("Connect is ExChange");
        }

        /*public void AddDelegate(newTradeEvent method)     //это для делегата, для события(event) это не надо
        {
            NewTradeEvent = method;
        }
        */

        void Save()
        {
            using (StreamWriter writer = new StreamWriter("params.txt", false))  //можно писать так илил использовать try, catch , в params.txt записываем имя файла или путь к нему. 
            {                                                                    //false переписываем файл с нуля, true пишем в конце
                 writer.WriteLine(priceUp.ToString());
                 writer.WriteLine(stepLevel.ToString());
                 writer.WriteLine(priceDown.ToString());
            }
        }

        static void Load()
        {
            try
            {
                StreamReader reader = new StreamReader("params.txt");
                int index = 0;

                while (true)
                {
                    string line = reader.ReadLine();

                    index++;

                    if (index == 1)
                    {
                        //priceUp = decimal.Parse(line);
                    }

                    if (line == null) break;
                }
            }
            catch (Exception e)
            {

            }
        }

        #endregion
    }
}

