using UnityEngine;

public class AbilityPoints : MonoBehaviour, IAbilityPoints
{
    [SerializeField] private UnitInfo _unitInfo;
    [SerializeField] private GameObject[] _points;

    public UnitInfo UnitInfo => _unitInfo;
    public GameObject[] Points => _points;
}