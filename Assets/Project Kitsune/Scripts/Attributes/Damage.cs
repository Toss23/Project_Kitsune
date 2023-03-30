using UnityEngine;

public class Damage : Attribute
{
    public float FinalCrit
    {
        get
        {
            if (Random.Range(0f, 1f) >= CritChance.Final / 100)
                return Final * (1 + CritMultiplier.Final / 100);
            else
                return Final;
        }
    }

    public CritChance CritChance { get; }
    public CritMultiplier CritMultiplier { get; }

    public Damage(float baseValue, float critChance, float critMultiplier)
    {
        BaseValue = baseValue;
        CritChance = new CritChance(critChance);
        CritMultiplier = new CritMultiplier(critMultiplier);

        Value = 0;
        Minimum = 0;
        Maximum = 1000;
    }
}