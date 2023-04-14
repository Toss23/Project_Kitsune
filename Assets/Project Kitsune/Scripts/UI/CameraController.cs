using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    private float z;

    private void Awake()
    {
        z = transform.position.z;
    }

    private void Update()
    {
        Vector3 position = _target.transform.position;
        position.z = z;
        transform.position = position;
    }
}