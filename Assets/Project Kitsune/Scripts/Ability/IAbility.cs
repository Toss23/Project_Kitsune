public interface IAbility
{
    public enum DamageType
    {
        Hit, DamageOverTime
    }

    public enum Type
    {
        Melee, Projectile
    }

    public int Level { get; }
    public bool UseCharacterDamage { get; }
    public bool UseCharacterCrit { get; }
    public float[] Damage { get; }
    public float[] DamageMultiplier { get; }
    public float[] CastPerSecond { get; }
    public float[] CritChance { get; }
    public float[] CritMultiplier { get; }

    public string Description { get; }

    public void SetLevel(int level);
}