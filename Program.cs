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
        /*
         * В программе созданн класс StartTrading иметирует некое подобие сделок на бирже и
         * отправляет данные через события на внешние обрабочтики для визуализации и хранения, 
         * для этого создана два события.
         * 
         */

        static void Main(string[] args)
        {
            StarTrading _start = new StarTrading();

            List<string> _positionData = new List<string>();
           
            _start.NewPositionEvent += Console.WriteLine;
           
            _start.NewPositionData += MyVoid;

            _start.NewPositionData += MyVoid2;

            // Метод для обработки наиболее важных данных позиции и записи в список

            void MyVoid(string count1)
            {

            _positionData.Add(Convert.ToString(count1));

            }

            // Запись в файл некторых полученных данных
                        
            void MyVoid2(string count1)
            {
                using (StreamWriter myStream = new StreamWriter("Position Data.txt", true)) 
                { 
                    myStream.WriteLine(count1); 
                }


            }


            Console.ReadLine();
                     

        }

      


    }

}
