using UnityEngine;

[CreateAssetMenu(fileName = "Passive Ability Data", menuName = "Ability/Passive Ability Data")]
public class PassiveAbilityData : AbilityData
{
    [Header("Passive")]
    [SerializeField] private bool _haveAura;
    [SerializeField] private GameObject _auraObject;
}