using UnityEngine;

[CreateAssetMenu(fileName = "Dot Ability Data", menuName = "Ability/Dot Ability Data")]
public class DotAbilityData : AbilityData
{
    [Header("Dot")]
    [SerializeField] private float[] _damageRate;
    [SerializeField] private bool _haveDuration;
    [SerializeField] private float[] _duration;

    public float[] DamageRate => _damageRate;
    public bool HaveDuration => _haveDuration;
    public float[] Duration => _duration;
}
