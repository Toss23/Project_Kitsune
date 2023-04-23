using System;
using System.Collections.Generic;

public class AbilitiesSelection
{
    public event Action<IAbility[], int[]> OnAbilitiesListGenerated;
    public event Action<IAbility> OnAbilityUpped;

    private IUnit _unit;
    private int _abilityLevelUpCount = 0;
    private bool _generating = false;

    public AbilitiesSelection(IUnit unit)
    {
        _unit = unit;
    }

    public void AbilityLevelUp()
    {
        _abilityLevelUpCount++;
        GenerateAbilitiesList();
    }

    public void CheckRequirementAbilityUp()
    {
        if (_abilityLevelUpCount > 0)
        {
            _generating = false;
            _abilityLevelUpCount--;
            GenerateAbilitiesList();
        }
    }

    private void GenerateAbilitiesList()
    {
        if (_generating == false)
        {
            _generating = true;

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
                int random = UnityEngine.Random.Range(0, canLevelUp.Count);
                abilitiesIdsForCards[0] = canLevelUp[random];
                canLevelUp.Remove(canLevelUp[random]);
            }

            if (canLevelUp.Count > 0)
            {
                int random = UnityEngine.Random.Range(0, canLevelUp.Count);
                abilitiesIdsForCards[1] = canLevelUp[random];
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
            int[] levels = new int[2];
            abilitiesForCards[0] = abilitiesIdsForCards[0] != -1 ? abilities.List[abilitiesIdsForCards[0]] : null;
            levels[0] = abilitiesIdsForCards[0] != -1 ? abilities.Levels[abilitiesIdsForCards[0]] : 0;
            abilitiesForCards[1] = abilitiesIdsForCards[1] != -1 ? abilities.List[abilitiesIdsForCards[1]] : null;
            levels[1] = abilitiesIdsForCards[1] != -1 ? abilities.Levels[abilitiesIdsForCards[1]] : 0;

            if (abilitiesIdsForCards[0] == -1 & abilitiesIdsForCards[1] == -1)
                OnAbilityUpped?.Invoke(null);
            else
                OnAbilitiesListGenerated?.Invoke(abilitiesForCards, levels);
        }
    }

    public void OnSelected(IAbility ability)
    {
        OnAbilityUpped?.Invoke(ability);
    }
}