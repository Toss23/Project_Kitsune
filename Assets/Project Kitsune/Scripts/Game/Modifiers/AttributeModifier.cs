[System.Serializable]
public class AttributeModifier
{
    public float Life;
    public float LifeRegeneration;
    public float MagicShield;
    public float MagicShieldRegeneration;
    public float Armour;

    public float Damage;
    public float CritChance;
    public float CritMultiplier;

    public float Movespeed;

    public float ActionSpeed;

    public AttributeModifier()
    {
        Life = 0;
        LifeRegeneration = 0;
        MagicShield = 0;
        MagicShieldRegeneration = 0;
        Armour = 0;

        Damage = 0;
        CritChance = 0;
        CritMultiplier = 0;

        Movespeed = 0;

        ActionSpeed = 0;
    }
}