public class ForestWindAura : Ability
{
    protected override void OnCollisionEnterWithEnemy(IUnit caster, IUnit target)
    {
        target.Curses.Add(new Curse(CursesInfo.List.Forest, Properties["Effect"], Properties["Duration"]));
    }

    protected override void OnCollisionStayWithEnemy(IUnit caster, IUnit target)
    {
        
    }

    protected override void OnCreateAbility(IUnit caster)
    {
        
    }

    protected override void OnUpdateAbility(IUnit caster, float deltaTime)
    {
        
    }

    protected override void OnDestroyAbility(IUnit caster)
    {

    }
}