public class CharacterAttributes
{
    public Life Life { get; }
    public Level Level { get; }

    public CharacterAttributes()
    {
        Life = new Life(100, 100);
        Level = new Level();
    }

    public void Update(float deltaTime)
    {
        Life.Regenerate(deltaTime);
    }
}