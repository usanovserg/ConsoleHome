using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Transactions;

namespace ConsoleHome
{
    public class Program
    {
        static void Main(string[] args)
        {
            Position position = new Position();

            position.ChengePosEvent += DisplayMessage;

            Console.ReadLine();
        }
        //============================================= Fields =============================================
        #region Fields

        #endregion

        //============================================= Methods =============================================
        #region Methods
        static void DisplayMessage(string message) => Console.WriteLine(message);
        #endregion
    }
}
