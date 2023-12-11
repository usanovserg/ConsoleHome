// See https://aka.ms/new-console-template for more information



using System;

public class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");

        while (true)
        {
            Console.WriteLine("Введите число: ");
            string? str = Console.ReadLine();


            if (int.TryParse(str, out int num) == true)
            {
                if (num != 0)
                {
                    decimal count = 9 / num;

                    Console.WriteLine("result = " + count);
                }
                else Console.WriteLine("На ноль делить нельзя!");
            }
            else  Console.WriteLine("введите корректное значение");
        }

    }
}




