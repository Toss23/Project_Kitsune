using System.Collections.Generic;
using UnityEngine;

public class AbilityCaster : MonoBehaviour, IAbilityCaster
{
    private IUnitPresenter _unitPresenter;

    public void Init(IUnitPresenter unitPresenter)
    {
        _unitPresenter = unitPresenter;
    }

    public void CreateAbility(IAbility ability, int abilityIndex, int level)
    {
        if (ability != null & _unitPresenter != null)
        {
            int count = 1;
            float angle = 0;
            float deltaAngle = 0;
            float startAngle = 0;

            AbilityModifier abilityModifier = _unitPresenter.Unit.ModifiersContainer.AbilityModifiers[abilityIndex];

            if (ability.Info.AbilityType == AbilityInfo.Type.Projectile)
            {
                count = ability.Info.ProjectileCount[level];
                angle = ability.Info.ProjectileAngle[level];

                count += abilityModifier.ProjectileCount;
                angle += abilityModifier.ProjectileAngle;

                if (count >= 2)
                {
                    deltaAngle = angle / (count - 1);
                    startAngle -= angle / 2f;
                }
            }

            for (int i = 0; i < count; i++)
            {
                if (_unitPresenter.UnitView.AbilityPoints.Points[abilityIndex] != null)
                {
                    // Create
                    Ability abilityObject = Instantiate((Ability)ability);
                    abilityObject.name = ability.Info.Name + " <" + level + ">";

                    // Locate
                    Transform abilityTransform = abilityObject.gameObject.transform;

                    if (ability.Info.AbilityType != AbilityInfo.Type.Field)
                        abilityTransform.Rotate(new Vector3(0, 0, _unitPresenter.UnitView.Angle + startAngle + deltaAngle * i));

                    Vector3 position = _unitPresenter.UnitView.AbilityPoints.Points[abilityIndex].transform.position;
                    abilityTransform.position = position;

                    // Fuse with Point
                    if (ability.Info.AbilityType == AbilityInfo.Type.Melee
                        || ability.Info.AbilityType == AbilityInfo.Type.Field)
                        abilityObject.FuseWith(_unitPresenter.UnitView.AbilityPoints.Points[abilityIndex].transform);

                    // Init
                    UnitType target = (_unitPresenter.UnitType == UnitType.Character) ? UnitType.Enemy : UnitType.Character;
                    abilityObject.Init(level, _unitPresenter.Unit, target, abilityModifier);
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
                auraObject.transform.parent = _unitPresenter.UnitView.AbilityPoints.PointsAura[abilityIndex].transform;
                auraObject.transform.position = _unitPresenter.UnitView.AbilityPoints.PointsAura[abilityIndex].transform.position;
                auraObject.transform.localScale = ability.Info.AuraObject.transform.localScale;
            }
        }
    }
}