using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_6
{
    class Program
    {
        static void Main(string[] args)
        {

            Position position = new Position();
            position.Moving += Position_newPositionInform;


            Console.ReadLine();

            ;
        }

        private static void Position_newPositionInform(object sender, PosEventArgs e)
        {
            Console.WriteLine($"Изменилась позиция: {e.PosValue}\n");
        }
    }
}
