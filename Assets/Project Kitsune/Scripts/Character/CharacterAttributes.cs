public class CharacterAttributes
{
    public Life Life { get; }
    public Level Level { get; }
    public Damage Damage { get; }
    public Armour Armour { get; }

    public CharacterAttributes(CharacterInfo characterInfo)
    {
        Life = new Life(characterInfo.Life, characterInfo.Regeneration);
        Level = new Level();
        Damage = new Damage(characterInfo.Damage, characterInfo.CritChance, characterInfo.CritMultiplier);
        Armour = new Armour(characterInfo.Armour);
    }

    public void Update(float deltaTime)
    {
        Life.Regenerate(deltaTime);
    }
}