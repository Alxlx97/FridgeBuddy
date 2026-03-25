using System.ComponentModel;

namespace FridgeBuddy.Models;

public enum ServingSize
{
    [Description("222 ml")]
    Ml222 = 222,
    [Description("355 ml")]
    Ml355 = 355,
    [Description("473 ml")]
    Ml473 = 473,
    [Description("500 ml")]
    Ml500 = 500,
    [Description("750 ml")]
    Ml750 = 750,
    [Description("1L")]
    Ml1000 = 1000,
    [Description("2L")]
    Ml2000 = 2000
}