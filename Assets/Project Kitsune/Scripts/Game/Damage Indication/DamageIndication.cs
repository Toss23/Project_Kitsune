using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class DamageIndication : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _duration;

    private TMP_Text text;
    private float _timer;

    public void Init(float value)
    {
        text = GetComponent<TMP_Text>();
        text.text = value.ToString();
        GetComponent<MeshRenderer>().sortingLayerName = "UI";
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, _speed, 0) * Time.deltaTime);

        _timer += Time.deltaTime;
        if (_timer >= _duration)
        {
            Destroy(gameObject);
        }
    }
}