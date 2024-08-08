using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBotMVVM.Entity.Enums;

public enum RegimeCalcGrid
{
    [Description("Шаг и количество уровней")]
    ByCountLevels = 0,

    [Description("Шаг и последний уровень")] 
    ByStepSize = 1,

    [Description("Кол.уровней и послед.уровень")]
    ByCountLevelsAndLastLevel = 2
}

