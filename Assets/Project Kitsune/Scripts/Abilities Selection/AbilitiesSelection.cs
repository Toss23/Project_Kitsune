using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesSelection
{
    public event Action<IAbility[]> OnAbilitiesListGenerated;
    public event Action<IAbility> OnSelectedAbility;

    private IUnit _unit;

    public AbilitiesSelection(IUnit unit)
    {
        _unit = unit;
    }

    public void GenerateAbilitiesList()
    {
        AbilitiesState abilities = _unit.Abilities;

        List<int> canLevelUp = new List<int>();
        int[] abilityIds = { -1, -1 };

        for (int i = 0; i < abilities.List.Length; i++)
        {
            if (abilities.List[i] != null && abilities.Levels[i] < abilities.MaxLevels[i])
                canLevelUp.Add(i);
        }

        if (canLevelUp.Contains(4))
            canLevelUp.Remove(4);

        if (canLevelUp.Count > 0)
        {
            abilityIds[0] = UnityEngine.Random.Range(0, canLevelUp.Count - 1);
            canLevelUp.Remove(abilityIds[0]);
        }

        if (canLevelUp.Count > 0)
        {
            abilityIds[1] = UnityEngine.Random.Range(0, canLevelUp.Count - 1);
        }

        IAbility[] abilitiesForCards = new IAbility[2];
        abilitiesForCards[0] = abilityIds[0] != -1 ? abilities.List[abilityIds[0]] : null;
        abilitiesForCards[1] = abilityIds[1] != -1 ? abilities.List[abilityIds[1]] : null;

        if (abilityIds[0] == -1 & abilityIds[1] == -1)
            OnSelectedAbility?.Invoke(null);
        else
            OnAbilitiesListGenerated?.Invoke(abilitiesForCards);
    }

    public void OnSelected(IAbility ability)
    {
        OnSelectedAbility?.Invoke(ability);
    }
}