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
        follower.IsMoving += CancelAttackOnMove;
    }

    protected override void OnDisablePresenter()
    {
        Follower follower = ((Enemy)_unit).Follower;
        follower.OnMove -= _unitView.SetAngle;
        follower.IsMoving -= _unitView.IsMoving;
        follower.IsMoving -= CancelAttackOnMove;
    }

    protected override void Death()
    {
        ((Enemy)_unit).Follower.Disable();
        GameLogic.Instance.Character.Unit.Attributes.Level.AddExperience(Unit.UnitInfo.ExperienceGain);

        base.Death();
    }

    private void CancelAttackOnMove(bool moving)
    {
        if (moving == true)
        {
            _unit.Abilities.CancelAttack();
        }
    }
}