using System.Collections.Generic;
using UnityEngine;

public static class CursesInfo
{
    public enum List
    {
        Weakness, Forest, Shadow, Exposure
    }

    public static readonly Dictionary<List, GameObject> Sprites = new Dictionary<List, GameObject>()
    {
        { List.Weakness, Resources.Load<GameObject>("Curses/Weakness") },
        { List.Forest, Resources.Load<GameObject>("Curses/Forest") },
        { List.Shadow, Resources.Load<GameObject>("Curses/Shadow") },
        { List.Exposure, Resources.Load<GameObject>("Curses/Exposure") }
    };

    public static readonly Weakness Weakness = new Weakness();
    public static readonly Forest Forest = new Forest();
    public static readonly Shadow Shadow = new Shadow();
    public static readonly Exposure Exposure = new Exposure();
}

public class Weakness
{
    public float OutputDamageMultiplier = 0.5f;     // 50% reduce Deal Damage
    public float InputDamageMultiplier = 2f;        // 200% increased Damage Taken
}

public class Forest
{
    public float ActionSpeedMultiplier = 0.5f;        // 50% reduce Action Second
}

public class Shadow
{
    
}

public class Exposure
{
    
}