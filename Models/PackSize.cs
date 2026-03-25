using System.ComponentModel;

namespace FridgeBuddy.Models;

public enum PackSize
{
    [Description("Single")]
    Single = 1,
    [Description("4-pack")]
    FourPack = 4,
    [Description("6-pack")]
    SixPack = 6,
    [Description("12-pack")]
    TwelvePack = 12,
    [Description("18-pack")]
    EighteenPack = 18,
    [Description("20-pack")]
    TwentyPack = 20,
    [Description("24-pack")]
    TwentyFourPack = 24,
    [Description("30-pack")]
    ThirtyPack = 30
}