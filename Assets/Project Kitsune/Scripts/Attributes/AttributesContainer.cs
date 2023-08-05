public class AttributesContainer
{
    public Life Life { get; }
    public Life MagicShield { get; }
    public Level Level { get; }
    public Damage Damage { get; }
    public Armour Armour { get; }
    public Movespeed Movespeed { get; }

    private IUnit _unit;

    public AttributesContainer(IUnit unit)
    {
        _unit = unit;
        Life = new Life(unit.UnitInfo.Life, unit.UnitInfo.LifeRegeneration);
        MagicShield = new Life(unit.UnitInfo.MagicShield, unit.UnitInfo.MagicShieldRegeneration);
        Level = new Level();
        Damage = new Damage(unit.UnitInfo.Damage, unit.UnitInfo.CritChance, unit.UnitInfo.CritMultiplier);
        Armour = new Armour(unit.UnitInfo.Armour);
        Movespeed = new Movespeed(unit.UnitInfo.Movespeed);
    }

    public void ResetToDefault()
    {
        Life.ResetToDefault();
        MagicShield.ResetToDefault();
        Damage.ResetToDefault();
        Armour.ResetToDefault();
        Movespeed.ResetToDefault();
    }

    public void Update(float deltaTime)
    {
        Life.Regenerate(deltaTime);
        MagicShield.Regenerate(deltaTime);

        if (_unit.Curses.Have(CursesInfo.List.Forest))
        {
            Curse forest = _unit.Curses.Find(CursesInfo.List.Forest);
            Movespeed.Multiplier = 1 - CursesInfo.Forest.MovespeedMultiplier * forest.Effect / 100;
        }
        else
        {
            Movespeed.Multiplier = 1;
        }
    }
}