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
        int[] abilitiesIdsForCards = { -1, -1 };

        for (int i = 0; i < abilities.List.Length; i++)
        {
            if (abilities.List[i] != null && abilities.Levels[i] < abilities.MaxLevels[i])
                canLevelUp.Add(i);
        }

        bool canLearnUlt = false;
        bool allAbilityLearned = true;

        if (canLevelUp.Contains(4))
        {
            canLevelUp.Remove(4);
            canLearnUlt = true;
        }

        if (canLevelUp.Count > 0)
        {
            allAbilityLearned = false;
        }

        if (canLevelUp.Count > 0)
        {
            abilitiesIdsForCards[0] = UnityEngine.Random.Range(0, canLevelUp.Count - 1);
            canLevelUp.Remove(abilitiesIdsForCards[0]);
        }

        if (canLevelUp.Count > 0)
        {
            abilitiesIdsForCards[1] = UnityEngine.Random.Range(0, canLevelUp.Count - 1);
        }

        if (allAbilityLearned)
        {
            if (canLearnUlt)
                abilitiesIdsForCards[0] = 4;
        }
        else
        {
            if (canLearnUlt && UnityEngine.Random.Range(0, 99) <= 9)
                abilitiesIdsForCards[1] = 4;
        }

        IAbility[] abilitiesForCards = new IAbility[2];
        abilitiesForCards[0] = abilitiesIdsForCards[0] != -1 ? abilities.List[abilitiesIdsForCards[0]] : null;
        abilitiesForCards[1] = abilitiesIdsForCards[1] != -1 ? abilities.List[abilitiesIdsForCards[1]] : null;

        if (abilitiesIdsForCards[0] == -1 & abilitiesIdsForCards[1] == -1)
            OnSelectedAbility?.Invoke(null);
        else
            OnAbilitiesListGenerated?.Invoke(abilitiesForCards);
    }

    public void OnSelected(IAbility ability)
    {
        OnSelectedAbility?.Invoke(ability);
    }
}