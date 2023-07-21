using System;
using System.Collections.Generic;

public class AbilitiesSelection
{
    public event Action<IAbility[], int[]> OnAbilitiesListGenerated;
    public event Action OnAbilityUpCanceled;

    private IUnit _character;
    private int _pointsToLevelUp;
    private bool _generating;

    public AbilitiesSelection(IUnit character)
    {
        _character = character;
        _pointsToLevelUp = 0;
        _generating = false;
    }

    public void AbilityLevelUp()
    {
        _pointsToLevelUp++;
        GenerateAbilitiesList();
    }

    public void CheckRequirementAbilityUp()
    {
        if (_pointsToLevelUp > 0)
        {
            _generating = false;
            GenerateAbilitiesList();
        }
        else
        {
            OnAbilityUpCanceled?.Invoke();
        }
    }

    private void GenerateAbilitiesList()
    {
        if (_generating == false)
        {
            _generating = true;
            _pointsToLevelUp--;

            AbilitiesContainer abilities = _character.Abilities;

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
                OnAbilityUpCanceled?.Invoke();
            else
                OnAbilitiesListGenerated?.Invoke(abilitiesForCards, levels);
        }
    }
}