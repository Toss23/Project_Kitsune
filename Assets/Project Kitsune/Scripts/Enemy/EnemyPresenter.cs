using UnityEngine;

[RequireComponent(typeof(EnemyView))]
public class EnemyPresenter : UnitPresenter
{
    protected override Unit CreateUnit() => new Enemy(_info, this, _rigidbody);
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
        ((Enemy)_unit).Follower.Disable();
        GameLogic.Instance.Character.Unit.Attributes.Level.AddExperience(Unit.UnitInfo.ExperienceGain);
    }
}