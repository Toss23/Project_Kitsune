using UnityEngine;

[RequireComponent(typeof(EnemyView))]
public class EnemyPresenter : UnitPresenter
{
    protected override IUnit CreateUnit() => new Enemy(_info, _rigidbody);
    protected override IUnitView CreateUnitView() => GetComponent<EnemyView>();

    protected override void OnEnablePresenter()
    {
        Follower follower = ((Enemy)_unit).Follower;
        follower.OnMove += _unitView.SetAngle;
        follower.IsMoving += _unitView.IsMoving;
    }

    protected override void OnDisablePresenter()
    {
        Follower follower = ((Enemy)_unit).Follower;
        follower.OnMove -= _unitView.SetAngle;
        follower.IsMoving -= _unitView.IsMoving;
    }

    protected override void OnDeath()
    {
        GameLogic.Instance.Character.Unit.Attributes.Level.AddExperience(Unit.UnitInfo.ExperienceGain);
    }
}