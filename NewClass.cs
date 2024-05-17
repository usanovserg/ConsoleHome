using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class NewClass
    {
        public NewClass()
        {
            Console.WriteLine("Hello from NewClass constructor!");
        }
        public static int ReadInput(string? prompt = null)
        {
            if (prompt != null)
                Console.Write(prompt);

            string? str = Console.ReadLine();
            int.TryParse(str, out int value);

            return value;
        }
    }
}
