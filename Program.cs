using ConsoleHome;

Position position = new Position();

position.ChangePosition += Position_ChangePosition;

void Position_ChangePosition(decimal size)
{
    if (size > 0)
    {
        Console.WriteLine($"Position changed. New position size = {size}. - LONG");
    }
    else if (size < 0)
    {
        Console.WriteLine($"Position changed. New position size = {-size}. - SHORT");
    }
    else
    {
        Console.WriteLine($"Position closed!");
    }
}

Console.ReadLine();