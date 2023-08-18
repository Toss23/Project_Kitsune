public class ForestWindAura : HitAbility
{
    protected override void OnHitEnemy(Unit enemy)
    {
        base.OnHitEnemy(enemy);

        enemy.Curses.Add(new Curse(CursesInfo.List.Forest, Properties["Effect"], Properties["CurseDuration"]));
    }
}