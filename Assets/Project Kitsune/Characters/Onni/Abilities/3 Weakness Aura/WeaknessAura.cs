public class WeaknessAura : Ability
{
    protected override void OnCollisionEnterWithEnemy(Unit caster, Unit target)
    {
        target.Curses.Add(new Curse(CursesInfo.List.Weakness, Properties["Effect"], Properties["Duration"]));
    }

    protected override void OnCollisionStayWithEnemy(Unit caster, Unit target)
    {
        
    }

    protected override void OnCreateAbility(Unit caster)
    {
        
    }

    protected override void OnUpdateAbility(Unit caster, float deltaTime)
    {
        
    }

    protected override void OnDestroyAbility(Unit caster)
    {

    }
}