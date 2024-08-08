using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBotMVVM.Services;

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
}

