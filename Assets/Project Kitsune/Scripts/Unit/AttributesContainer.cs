public class AttributesContainer
{
    public Life Life { get; }
    public Level Level { get; }
    public Damage Damage { get; }
    public Armour Armour { get; }

    public AttributesContainer(UnitInfo info)
    {
        Life = new Life(info.Life, info.Regeneration);
        Level = new Level();
        Damage = new Damage(info.Damage, info.CritChance, info.CritMultiplier);
        Armour = new Armour(info.Armour);
    }

    public void Update(float deltaTime)
    {
        Life.Regenerate(deltaTime);
    }
}