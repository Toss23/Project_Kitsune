public class ForestWindAura : Ability
{
    protected override void OnCollisionEnterWithEnemy(IUnit caster, IUnit target)
    {
        target.Curses.Add(new Curse(CursesInfo.List.Forest, Properties["Effect"], Properties["Duration"]));
    }

    protected override void OnCollisionStayWithEnemy(IUnit caster, IUnit target)
    {
        
    }

    protected override void OnCreate()
    {
        
    }

    protected override void OnUpdate(float deltaTime)
    {
        
    }
}