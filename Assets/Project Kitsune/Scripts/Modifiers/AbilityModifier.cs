[System.Serializable]
public class AbilityModifier
{
    public int AbilityIndex { get; }

    public float Damage = 0;
    public float Multiplier = 0;
    public float CastPerSecond = 0;
    public float CritChance = 0;
    public float CritMultiplier = 0;

    public int ProjectileCount = 0;
    public float ProjectileTiltAngle = 0;

    public float DotRate = 0;
    public float Duration = 0;

    public float Scale = 0;

    public AbilityModifier(int abilityIndex)
    {
        AbilityIndex = abilityIndex;
    }

    public void Add(AbilityModifier abilityModifier)
    {
        Damage += abilityModifier.Damage;
        Multiplier += abilityModifier.Multiplier;
        CastPerSecond += abilityModifier.CastPerSecond;
        CritChance += abilityModifier.CritChance;
        CritMultiplier += abilityModifier.CritMultiplier;

        ProjectileCount += abilityModifier.ProjectileCount;
        ProjectileTiltAngle += abilityModifier.ProjectileTiltAngle;

        DotRate += abilityModifier.DotRate;
        Duration += abilityModifier.Duration;

        Scale += abilityModifier.Scale;
    }

    public void Subtract(AbilityModifier abilityModifier)
    {
        Damage -= abilityModifier.Damage;
        Multiplier -= abilityModifier.Multiplier;
        CastPerSecond -= abilityModifier.CastPerSecond;
        CritChance -= abilityModifier.CritChance;
        CritMultiplier -= abilityModifier.CritMultiplier;

        ProjectileCount -= abilityModifier.ProjectileCount;
        ProjectileTiltAngle -= abilityModifier.ProjectileTiltAngle;

        DotRate -= abilityModifier.DotRate;
        Duration -= abilityModifier.Duration;

        Scale -= abilityModifier.Scale;
    }
}