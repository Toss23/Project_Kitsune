public class ForestWindAura : HitAbility
{
    protected override void OnHitEnemy(Unit enemy)
    {
        base.OnHitEnemy(enemy);

        enemy.CursesContainer.Add(new Curse(Curses.List.Forest, Properties["Effect"], Properties["CurseDuration"]));
    }
}