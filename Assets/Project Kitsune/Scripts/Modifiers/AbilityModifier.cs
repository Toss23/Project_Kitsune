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
}