using UnityEngine;

[RequireComponent(typeof(EnemyView))]
public class EnemyPresenter : UnitPresenter
{
    protected override Unit CreateUnit() => new Enemy(_context, _info, this, _rigidbody);
    protected override IUnitView CreateUnitView() => GetComponent<EnemyView>();

    protected override void OnEnablePresenter()
    {
        Follower follower = ((Enemy)_unit).Follower;
        follower.IsMoving += _unitView.SetMovingAndAngle;
    }

    protected override void OnDisablePresenter()
    {
        Follower follower = ((Enemy)_unit).Follower;
        follower.IsMoving -= _unitView.SetMovingAndAngle;
    }

    protected override void Death()
    {
        ((Enemy)_unit).Follower.Disable();
        _context.Character.Unit.AttributesContainer.Level.AddExperience(Unit.UnitInfo.ExperienceGain);
        ((GameContext)_context).KillCounterPresenter.KillCounter.AddKill();

        base.Death();
    }
}