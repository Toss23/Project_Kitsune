public class AttributesContainer
{
    public Life Life { get; }
    public Life MagicShield { get; }
    public Level Level { get; }
    public Damage Damage { get; }
    public Armour Armour { get; }
    public Movespeed Movespeed { get; }
    public ActionSpeed ActionSpeed { get; }

    public AttributesContainer(Unit unit)
    {
        Life = new Life(unit.UnitInfo.Life, unit.UnitInfo.LifeRegeneration);
        MagicShield = new Life(unit.UnitInfo.MagicShield, unit.UnitInfo.MagicShieldRegeneration);
        Level = new Level();
        Damage = new Damage(unit.UnitInfo.Damage, unit.UnitInfo.CritChance, unit.UnitInfo.CritMultiplier);
        Armour = new Armour(unit.UnitInfo.Armour);
        Movespeed = new Movespeed(unit.UnitInfo.Movespeed);
        ActionSpeed = new ActionSpeed();
    }

    public void ResetToDefault()
    {
        Life.ResetToDefault();
        MagicShield.ResetToDefault();
        Damage.ResetToDefault();
        Armour.ResetToDefault();
        Movespeed.ResetToDefault();
        ActionSpeed.ResetToDefault();
    }

    public void Update(float deltaTime)
    {
        Life.Regenerate(deltaTime);
        MagicShield.Regenerate(deltaTime);
    }
}