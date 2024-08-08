using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OsEngine.Entity;
using OsEngine.Logging;
using OsEngine.Market.Connectors;

namespace OsEngine.Robots._MyRobots.Services;

public static class Extensions
{
    private static readonly CultureInfo _culture = CultureInfo.GetCultureInfo("ru-RU");

    public static decimal ToDecimal(this string value)
    {
        if (value == null)
        {
            return 0;
        }
        if (value.Contains("E"))
        {
            return Convert.ToDecimal(value.ToDouble());
        }
        try
        {
            return Convert.ToDecimal(value.Replace(",",
                    CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator),
                CultureInfo.InvariantCulture);
        }
        catch
        {
            return Convert.ToDecimal(value.ToDouble());
        }
    }

    public static double ToDouble(this string value)
    {
        return Convert.ToDouble(value.Replace(",",
                CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator),
            CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// remove zeros from the decimal value at the end
    /// </summary>
    public static string ToStringWithNoEndZero(this decimal value)
    {
        string result = value.ToString(_culture);

        if (result.Contains(","))
        {
            result = result.TrimEnd('0');

            if (result.EndsWith(","))
            {
                result = result.TrimEnd(',');
            }
        }

        return result;
    }

    public static int GetDecimalDigitsCount(decimal number)
    {
        string[] str = number.ToString(CultureInfo.InvariantCulture).Split(',');
        return str.Length == 2 ? str[1].Length : 0;
    }

    //===================== Заменяем . на , при вводе данных ==================================

    public static string PointChanged(string str)
    {
        if (str.Contains("."))
        {
            string s = str.Replace(".", ",");
            return s;
        }

        return str;
    }

    public static string GetEnumDescription(Enum enumValue)
    {
        // Получение атрибута Description
        var attribute = enumValue.GetType()
            .GetField(enumValue.ToString())
            .GetCustomAttributes(typeof(DescriptionAttribute), false)
            .SingleOrDefault() as DescriptionAttribute;

        return attribute == null ? enumValue.ToString() : attribute.Description;
    }

    public static (IEnumerable<T>, T, T) GetLastElementsAndMinMax<T>(List<T> data, int count)
    {
        // Проверяем, что список не пуст и что запрашиваемое количество элементов меньше или равно размеру списка
        if (data == null || data.Count <= 0 || count <= 0 || count > data.Count)
        {
            throw new ArgumentException("Data collection cannot be null or empty, and count should be positive and less than or equal to the size of the collection.");
        }

        // Получаем индекс, до которого нужно взять элементы
        int lastIndex = data.Count - count;

        // Извлекаем подмножество элементов
        var lastElements = data.GetRange(lastIndex, count);

        // Находим минимальное и максимальное значения среди возвращаемых элементов
        T minElement = lastElements.Min();
        T maxElement = lastElements.Max();

        return (lastElements, minElement, maxElement);
    }

    

    public static string RoundPrice(decimal price, Security security, Side side)
    {   
        try
        {
            if (security.PriceStep == 0) return string.Empty;

            if (security.Decimals > 0)
            {
                price = Math.Round(price, security.Decimals);

                decimal minStep = 0.1m;

                for (int i = 1; i < security.Decimals; i++)
                {
                    minStep = minStep * 0.1m;
                }

                while (price % security.PriceStep != 0)
                {
                    price = price - minStep;
                }
            }
            else
            {
                price = Math.Round(price, 0);
                while (price % security.PriceStep != 0)
                {
                    price--;
                }
            }

            if (side == Side.Buy &&
                security.PriceLimitHigh != 0 && price > security.PriceLimitHigh)
            {
                price = security.PriceLimitHigh;
            }

            if (side == Side.Sell &&
                security.PriceLimitLow != 0 && price < security.PriceLimitLow)
            {
                price = security.PriceLimitLow;
            }

            string priceOut = null;
            string priceTemp = price.ToString(CultureInfo.InvariantCulture);
            if (priceTemp.Contains(','))
            {
                priceOut = priceTemp.Replace(',', '.');
            }

            return priceOut ?? priceTemp;
        }
        catch (Exception error)
        {
            //SetNewLogMessage(error.ToString(), LogMessageType.Error);
        }

        return string.Empty ;
    }
}

