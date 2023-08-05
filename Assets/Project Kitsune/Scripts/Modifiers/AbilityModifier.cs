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
    public float ProjectileAngle = 0;

    public float DotRate = 0;
    public float DotDuration = 0;

    public float Radius = 0;

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
        ProjectileAngle += abilityModifier.ProjectileAngle;

        DotRate += abilityModifier.DotRate;
        DotDuration += abilityModifier.DotDuration;

        Radius += abilityModifier.Radius;
    }

    public void Subtract(AbilityModifier abilityModifier)
    {
        Damage -= abilityModifier.Damage;
        Multiplier -= abilityModifier.Multiplier;
        CastPerSecond -= abilityModifier.CastPerSecond;
        CritChance -= abilityModifier.CritChance;
        CritMultiplier -= abilityModifier.CritMultiplier;

        ProjectileCount -= abilityModifier.ProjectileCount;
        ProjectileAngle -= abilityModifier.ProjectileAngle;

        DotRate -= abilityModifier.DotRate;
        DotDuration -= abilityModifier.DotDuration;

        Radius -= abilityModifier.Radius;
    }
}