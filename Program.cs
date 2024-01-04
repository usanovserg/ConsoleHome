// See https://aka.ms/new-console-template for more information
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Timers;

namespace Lesson_6
{

    class Program
    {

        static void Main(string[] args)
        {
            StartTrading _newTrade = new StartTrading();
            

            Console.ReadLine();

        }
      
       
    }



    public delegate void PositionValue(decimal value1, decimal value2, decimal value3, DateTime value4);

}
    
   
    

