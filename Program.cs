using ConsoleHome;

internal class Program
{
    private static void Main(string[] args)
    {
        NewClass newClass = new NewClass();
        int val1 = NewClass.ReadInput("Введите первое слагаемое: ");
        int val2 = NewClass.ReadInput("Введите второе слагаемое: ");

        Console.WriteLine($"Сумма введенных чисел равна {val1 + val2}");
    }
}