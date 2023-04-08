using UnityEngine;

public class AbilityPoints : MonoBehaviour
{
    [SerializeField] private GameObject[] _points;

    public GameObject[] Points => _points;
}