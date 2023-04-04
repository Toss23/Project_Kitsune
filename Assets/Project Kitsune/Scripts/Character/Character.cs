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
        Abilities.OnCastReloaded += CastAbility;
    }

    public void Update(float deltaTime)
    {
        Attributes.Life.Regenerate(deltaTime);
        Abilities.UpdateCastTime(deltaTime);
    }

    private void CastAbility(IAbility ability, int level)
    {
        
    }

    private void Death()
    {
        Attributes.Life.Regeneration = 0;
    }
}