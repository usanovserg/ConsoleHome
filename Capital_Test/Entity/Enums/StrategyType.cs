using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Capital_Test.Enums;

public enum StrategyType
{
    [Description("IsCheckedFix")]
    FIX = 0,

    [Description("IsCheckedCap")]
    CAPITALIZATON = 1,

    [Description("IsCheckedProg")]
    PROGRESS = 2,

    [Description("IsCheckedDown")]
    DOWNGRADE = 3
}

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());

        DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

        return attribute == null ? value.ToString() : attribute.Description;
    }
}
