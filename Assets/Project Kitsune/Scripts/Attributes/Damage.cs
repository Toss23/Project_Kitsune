using UnityEngine;

public class Damage : Attribute
{
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

    /// <summary>
    /// ADamage - Ability Damage
    /// CDamage - Character Damage
    /// Final Damage = (ADamage + CDamage) * ADamageMultiplier * Crit
    /// Crit Multiplier default value = 100%
    /// </summary>
    public static float CalculateAbilityDamage(Damage damage, IAbility ability, int abilityLevel)
    {
        float finalDamage = ability.Info.Damage[abilityLevel];

        if (ability.Info.UseCharacterDamage)
            finalDamage += damage.Final;

        finalDamage *= ability.Info.DamageMultiplier[abilityLevel] / 100;

        float critChance = ability.Info.CritChance[abilityLevel];
        float critMultiplier = ability.Info.CritMultiplier[abilityLevel];

        if (ability.Info.UseCharacterCrit)
        {
            critChance += damage.CritChance.Final;
            critMultiplier += damage.CritMultiplier.Final - 100;
        }

        if (Random.Range(0f, 1f) <= critChance / 100)
            finalDamage *= critMultiplier / 100;

        return finalDamage;
    }
}