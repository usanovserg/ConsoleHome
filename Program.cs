using ConsoleHome;

Position position = new Position();

position.positionIschanged += PositionChangedAlert;

//=====================================================Methods=====================================

# region Methods

static void PositionChangedAlert(decimal volume)
{
    if (Math.Abs(volume) < 5)
    {
        Console.WriteLine($"Позиция изменилась на {volume} лота");
    }
    else
    {
        Console.WriteLine($"Позиция изменилась на {volume} лотов");
    }
}

# endregion

Console.ReadLine();
