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
    /// </summary>
    public static float CalculateAbilityDamage(Damage damage, IAbility ability, int abilityLevel)
    {
        float finalDamage = ability.Damage[abilityLevel];

        if (ability.UseCharacterDamage)
            finalDamage += damage.Final;

        finalDamage *= ability.DamageMultiplier[abilityLevel];

        float critChance = ability.CritChance[abilityLevel];
        float critMultiplier = ability.CritMultiplier[abilityLevel];

        if (ability.UseCharacterCrit)
        {
            critChance += damage.CritChance.Final;
            critMultiplier += damage.CritMultiplier.Final;
        }

        if (Random.Range(0f, 1f) <= critChance / 100f)
            finalDamage *= critMultiplier / 100f;

        return finalDamage;
    }
}