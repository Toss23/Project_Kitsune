using UnityEngine;

[CreateAssetMenu(fileName = "Melee Ability Data", menuName = "Ability/Melee Ability Data")]
public class MeleeAbilityData : AbilityData
{
    [Header("Melee")]
    [SerializeField] private float _animationTime;
}