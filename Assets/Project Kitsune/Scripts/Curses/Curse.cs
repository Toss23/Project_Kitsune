using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Curse
{
    public CursesInfo.List Name;
    public float Duration;
    public float Effect;

    public Curse(CursesInfo.List name, float duration, float effect)
    {
        Name = name;
        Duration = duration;
        Effect = effect;
    }
}

public static class CursesInfo
{
    public enum List
    {
        Weakness, Forest, Shadow, Exposure
    }

    public static Dictionary<List, GameObject> Spites = new Dictionary<List, GameObject>()
    {
        { List.Weakness, Resources.Load<GameObject>("Curses/Weakness") },
        { List.Forest, Resources.Load<GameObject>("Curses/Forest") },
        { List.Shadow, Resources.Load<GameObject>("Curses/Shadow") },
        { List.Exposure, Resources.Load<GameObject>("Curses/Exposure") }
    };
    public static GameObject CenterSprite = Resources.Load<GameObject>("Curses/Center");

    public static Weakness Weakness = new Weakness();
}

public class Weakness
{
    public float OutputDamageMultiplier = 0.5f;
    public float InputDamageMultiplier = 2f;
}