using UnityEngine;
using UnityEngine.UI;

public class AbilityView : MonoBehaviour
{
    [SerializeField] private GameObject _abilityPrefab;

    private GameObject[] _abilities;

    private void Awake()
    {
        
    }

    public void InitAbilities(IAbility[] abilities)
    {
        _abilities = new GameObject[4];

        for (int i = 0; i < _abilities.Length; i++)
        {
            GameObject ability = Instantiate(_abilityPrefab);
            ability.transform.SetParent(transform);
            
            Image abilityImage = ability.GetComponent<Image>();
            abilityImage.sprite = null;

            _abilities[i] = ability;        
        }
    }
}