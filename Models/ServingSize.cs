using System.ComponentModel;

namespace BeerTracker.Models;

public enum ServingSize
{
    [Description("355 ml")]
    Ml355 = 355,
    [Description("500 ml")]
    Ml500 = 500,
    [Description("750 ml")]
    Ml750 = 750,
    [Description("1L")]
    Ml1000 = 1000
}