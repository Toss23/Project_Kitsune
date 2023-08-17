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

            if (ability.AbilityData.GetAbilityType() == AbilityData.Type.Range)
            {
                RangeAbilityData rangeAbilityData = (RangeAbilityData)ability.AbilityData;

                count = rangeAbilityData.Count.Get(level);
                angle = rangeAbilityData.TiltAngle.Get(level);

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
                    abilityObject.name = ability.AbilityData.Name + " <" + level + ">";

                    // Locate
                    Transform abilityTransform = abilityObject.gameObject.transform;

                    if (ability.AbilityData.GetAbilityType() != AbilityData.Type.Passive)
                    {
                        abilityTransform.Rotate(new Vector3(0, 0, _unitPresenter.UnitView.Angle + startAngle + deltaAngle * i));
                    }

                    Vector3 position = _unitPresenter.UnitView.AbilityPoints.Points[abilityIndex].transform.position;
                    abilityTransform.position = position;

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
        }
    }
}