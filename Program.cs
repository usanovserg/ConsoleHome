using System;



namespace ConsoleHome
{
    internal class Program
    {
        static void Main()
        {
            // Экземпляр класса Position 
            Position position = new Position();

            // Регистрация метода ReportChange через свойство 
            position.PositionChangeHandler = ReportChange;

            Console.ReadLine();
        }

        // Метод будет выводить подробное сообщение об изменении позиции
        public static void ReportChange(String exchange, String securityCode, String typeOrder, Decimal price, UInt32 volume)
        {
            Console.WriteLine($"Изменение позиции: {exchange} | {securityCode} | {typeOrder} | {price} | {volume}");
        }
    }
}

// Урок 1.6. Программа генерации сделок. Добавлен вывод сообщений об изменении позиции.
// .NET 8
// 06.06.2024
