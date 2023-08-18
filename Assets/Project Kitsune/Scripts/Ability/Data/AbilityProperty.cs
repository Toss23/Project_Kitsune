using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityProperty
{
    [SerializeField] private string _name;
    [SerializeField] private float[] _values;

    public string Name => _name;
    public ArrayData<float> Values => new ArrayData<float>(_values);

    public static Dictionary<string, float> ListToDictionary(int level, AbilityProperty[] abilityProperties)
    {
        Dictionary<string, float> result = new Dictionary<string, float>();

        foreach (AbilityProperty abilityProperty in abilityProperties)
        {
            result.Add(abilityProperty.Name, abilityProperty.Values.Get(level));
        }

        return result;
    }
}