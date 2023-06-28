using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Curse
{
    public enum CurseType
    {
        Weakness, Forest, Shadow, Exposure
    }

    public static Dictionary<CurseType, GameObject> CursesSpite = new Dictionary<CurseType, GameObject>()
    {
        { CurseType.Weakness, Resources.Load<GameObject>("Curses/Weakness") },
        { CurseType.Forest, Resources.Load<GameObject>("Curses/Forest") },
        { CurseType.Shadow, Resources.Load<GameObject>("Curses/Shadow") },
        { CurseType.Exposure, Resources.Load<GameObject>("Curses/Exposure") }
    };

    public CurseType Type;
    public float Duration;
    public float Effect;

    public Curse(CurseType type, float duration, float effect)
    {
        Type = type;
        Duration = duration;
        Effect = effect;
    }
}