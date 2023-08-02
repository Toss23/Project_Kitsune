public class AttributesContainer
{
    public Life Life { get; }
    public Life MagicShield { get; }
    public Level Level { get; }
    public Damage Damage { get; }
    public Armour Armour { get; }
    public Movespeed Movespeed { get; }

    public AttributesContainer(UnitInfo info)
    {
        Life = new Life(info.Life, info.LifeRegeneration);
        MagicShield = new Life(info.MagicShield, info.MagicShieldRegeneration);
        Level = new Level();
        Damage = new Damage(info.Damage, info.CritChance, info.CritMultiplier);
        Armour = new Armour(info.Armour);
        Movespeed = new Movespeed(info.Movespeed);
    }

    public void Update(float deltaTime)
    {
        Life.Regenerate(deltaTime);
    }
}