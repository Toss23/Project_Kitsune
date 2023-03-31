public interface IAbility
{
    public bool UseCharacterDamage { get; }
    public bool UseCharacterCrit { get; }
    public float Damage { get; }
    public float DamageMultiplier { get; }
    public float CritChance { get; }
    public float CritMultiplier { get; }
}