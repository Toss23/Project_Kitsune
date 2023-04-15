using UnityEngine;

public class AbilityPoints : MonoBehaviour
{
    [SerializeField] private UnitInfo _unitInfo;
    [SerializeField] private GameObject[] _points;
    [SerializeField] private GameObject[] _pointsAura;

    public UnitInfo UnitInfo => _unitInfo;
    public GameObject[] Points => _points;
    public GameObject[] PointsAura => _pointsAura;
}