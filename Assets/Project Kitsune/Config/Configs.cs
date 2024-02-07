using System.Collections.Generic;

public class Configs
{
    public enum Map
    {
        Start = 0, Game = 1
    }

    public enum Character
    {
        Kitsune = 0, Onni = 1
    }

    public static readonly Dictionary<Character, UnitInfo> CharacterInfo = new Dictionary<Character, UnitInfo>()
    {
        { Character.Kitsune, null },
        { Character.Onni, null }
    };

    public static readonly int[] Experience = new int[]
    {
        0, 5, 9, 15, 24, 34, 47, 63, 80, 100, 123, 148, 176, 207, 240, 276, 315, 357, 403,
        451, 502, 557, 615, 676, 741, 809, 881, 957, 1036, 1120, 1207, 1298, 1394, 1493,
        1597, 1706, 1819, 1936, 2058, 2185, 2317, 2454, 2596, 2743, 2896, 3054, 3217, 3387,
        3562, 3742, 3929, 4122, 4321, 4527, 4739, 4958, 5183, 5416, 5655, 5902, 6156, 6418,
        6687, 6964, 7249, 7541, 7843, 8152, 8471, 8797, 9133, 9478, 9832, 10196, 10569, 10952,
        11345, 11749, 12162, 12586, 13021
    };
}
