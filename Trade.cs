using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome;

public class Trade
{

    /// <summary>
    /// Код инструмента
    /// </summary>
    public string SecCode = "";

    /// <summary>
    /// Код класса
    /// </summary>
    public string ClassCode = "";

    /// <summary>
    /// Номер счета
    /// </summary>
    public string Depo = "76Rt899p";

    /// <summary>
    /// Комментарий
    /// </summary>
    public string Comment = "";

    /// <summary>
    /// Номер сделки
    /// </summary>
    public decimal NumTrade;

    /// <summary>
    /// ID транзакции
    /// </summary>
    public decimal ID_Transaction;

    public DateTime DateTime;

    /// <summary>
    /// Объем сделки
    /// </summary>
    public decimal Volume;

    /// <summary>
    /// Тип сделки
    /// </summary>
    public Operation Operation;

    /// <summary>
    /// Цена сделки
    /// </summary>
    public decimal Price;

    /// <summary>
    /// Размер позиции
    /// </summary>
    public static decimal Quantity = 0;
}

public enum Operation
{
    Long,
    Short
}