using ConsoleHome;

Position position = new Position();

position.PositionIschanged += PositionChangedAlert;

//=====================================================Methods=====================================

# region Methods

static void PositionChangedAlert(decimal volume, decimal new_pos)
{
    if (Math.Abs(volume) < 5)
    {
        Console.WriteLine($"Позиция изменилась на {volume} лота, Остаток - {new_pos}");
    }
    else
    {
        Console.WriteLine($"Позиция изменилась на {volume} лотов, Остаток - {new_pos}");
    }
}

# endregion

Console.ReadLine();
