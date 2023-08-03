using UnityEngine;

public class Damage : Attribute
{
    public CritChance CritChance { get; }
    public CritMultiplier CritMultiplier { get; }

    public Damage(float baseValue, float critChance, float critMultiplier)
    {
        CritChance = new CritChance(critChance);
        CritMultiplier = new CritMultiplier(critMultiplier);

        Value = baseValue;
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
        float finalDamage = ability.Info.Damage[abilityLevel] + ability.AbilityModifier.Damage;

        if (ability.Info.UseCharacterDamage)
            finalDamage += damage.Value;

        finalDamage *= (ability.Info.DamageMultiplier[abilityLevel] + ability.AbilityModifier.Multiplier) / 100;

        float critChance = ability.Info.CritChance[abilityLevel] + ability.AbilityModifier.CritChance;
        float critMultiplier = ability.Info.CritMultiplier[abilityLevel] + ability.AbilityModifier.CritMultiplier;

        if (ability.Info.UseCharacterCrit)
        {
            critChance += damage.CritChance.Value;
            critMultiplier += damage.CritMultiplier.Value - 100;
        }

        if (Random.Range(0f, 1f) <= critChance / 100)
            finalDamage *= critMultiplier / 100;

        return finalDamage;
    }
}