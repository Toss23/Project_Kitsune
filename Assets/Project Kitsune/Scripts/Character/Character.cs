public class Character : ICharacter
{
    public Controlable Controlable { get; }
    public CharacterAttributes Attributes { get; }
    public AbilitiesState Abilities { get; }

    public Character(CharacterInfo characterInfo)
    {
        Controlable = new Controlable();
        Controlable.Speed = 3;

        Attributes = new CharacterAttributes(characterInfo);
        Attributes.Life.OnMinimum += Death;

        Abilities = new AbilitiesState(characterInfo.Abilities);
    }

    public void Update(float deltaTime)
    {
        Attributes.Life.Regenerate(deltaTime);
        Abilities.UpdateCastTime(deltaTime);
    }

    public void RegisterAbility(IAbility ability)
    {
        ability.OnHit += OnHitAbility;
    }

    private void OnHitAbility(IAbility ability, IEnemy enemy)
    {
        
    }

    private void Death()
    {
        Attributes.Life.Regeneration = 0;
    }
}