using UnityEngine;

public class AbilityCaster : MonoBehaviour, IAbilityCaster
{
    private IUnitPresenter _unitPresenter;

    public void Init(IUnitPresenter unitPresenter)
    {
        _unitPresenter = unitPresenter;
    }

    public void CreateAbility(IAbility ability, int point, int level)
    {
        if (ability != null & _unitPresenter != null)
        {
            int count = 1;
            float deltaAngle = 0;
            float startAngle = 0;

            if (ability.Info.AbilityType == AbilityInfo.Type.Projectile)
            {
                count = ability.Info.ProjectileCount[level];
                deltaAngle = ability.Info.ProjectileSpliteAngle[level];
                startAngle = -count * deltaAngle / 2f;
            }

            for (int i = 0; i < count; i++)
            {
                if (_unitPresenter.UnitView.AbilityPoints.Points[point] != null)
                {
                    // Create
                    Ability abilityObject = Instantiate((Ability)ability);
                    abilityObject.name = ability.Info.Name + " <" + level + ">";

                    // Locate
                    Transform abilityTransform = abilityObject.gameObject.transform;

                    if (ability.Info.AbilityType != AbilityInfo.Type.Field)
                        abilityTransform.Rotate(new Vector3(0, 0, _unitPresenter.UnitView.Angle + startAngle + deltaAngle * i));

                    Vector3 position = _unitPresenter.UnitView.AbilityPoints.Points[point].transform.position;
                    abilityTransform.position = position;

                    // Fuse with Point
                    if (ability.Info.AbilityType == AbilityInfo.Type.Melee
                        || ability.Info.AbilityType == AbilityInfo.Type.Field)
                        abilityObject.FuseWith(_unitPresenter.UnitView.AbilityPoints.Points[point].transform);

                    // Init
                    abilityObject.Init(_unitPresenter.Unit, level, (_unitPresenter.UnitType == UnitType.Character) ? Target.Enemy : Target.Character);
                    _unitPresenter.Unit.RegisterAbility(abilityObject);
                }
                else
                {
                    Debug.Log("[Error] Ability Point (" + i + ") is not found!");
                }
            }

            if (ability.Info.HaveAura)
            {
                GameObject auraObject = Instantiate(ability.Info.AuraObject);
                auraObject.transform.parent = _unitPresenter.UnitView.AbilityPoints.PointsAura[point].transform;
                auraObject.transform.position = _unitPresenter.UnitView.AbilityPoints.PointsAura[point].transform.position;
                auraObject.transform.localScale = ability.Info.AuraObject.transform.localScale;
            }
        }
    }
}