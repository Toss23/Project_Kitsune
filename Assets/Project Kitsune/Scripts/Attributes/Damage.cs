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

    protected override bool ClampOnChange() => false;

    public override void ResetToDefault()
    {
        base.ResetToDefault();
        CritChance.ResetToDefault();
        CritMultiplier.ResetToDefault();
    }

    /// <summary>
    /// ADamage - Ability Damage
    /// CDamage - Character Damage
    /// Final Damage = (ADamage + CDamage) * ADamageMultiplier * Crit
    /// Crit Multiplier default value = 100%
    /// </summary>
    public static float CalculateAbilityDamage(Damage damage, IAbility ability, int abilityLevel)
    {
        if (ability.AbilityData.GetAbilityType() == AbilityData.Type.Passive)
        {
            return 0;
        }

        BaseAbilityData baseAbilityData = (BaseAbilityData)ability.AbilityData;

        float finalDamage = baseAbilityData.Damage.Get(abilityLevel) + ability.AbilityModifier.Damage;

        if (baseAbilityData.UseCharacterDamage)
            finalDamage += damage.Value;

        finalDamage *= (baseAbilityData.DamageMultiplier.Get(abilityLevel) + ability.AbilityModifier.Multiplier) / 100;

        float critChance = baseAbilityData.CritChance.Get(abilityLevel) + ability.AbilityModifier.CritChance;
        float critMultiplier = baseAbilityData.CritMultiplier.Get(abilityLevel) + ability.AbilityModifier.CritMultiplier;

        if (baseAbilityData.UseCharacterCrit)
        {
            critChance += damage.CritChance.Value;
            critMultiplier += damage.CritMultiplier.Value - 100;
        }

        if (Random.Range(0f, 1f) <= critChance / 100)
            finalDamage *= critMultiplier / 100;

        return finalDamage;
    }
}