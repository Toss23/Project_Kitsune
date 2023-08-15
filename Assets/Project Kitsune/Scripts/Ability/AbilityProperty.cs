using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityProperty
{
    [SerializeField] public string Name;
    [SerializeField] public float[] Values;

    public static Dictionary<string, float> ListToDictionary(int level, AbilityProperty[] abilityProperties)
    {
        Dictionary<string, float> result = new Dictionary<string, float>();

        foreach (AbilityProperty abilityProperty in abilityProperties)
        {
            result.Add(abilityProperty.Name, abilityProperty.Values[level]);
        }

        return result;
    }
}