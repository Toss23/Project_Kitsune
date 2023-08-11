using UnityEngine;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject _bar;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private bool _hideOnNull = false;

    private Transform _barTransform;

    private void Awake()
    {
        if (_bar != null)
        {
            _barTransform = _bar.transform;
        }
    }

    public void SetPercentAndText(float value, string text)
    {
        if (_barTransform != null)
        {
            _bar.SetActive(value > 0);

            if (value > 0)
            {
                if (_hideOnNull)
                {
                    _bar.SetActive(true);
                }

                _barTransform.localScale = new Vector3(value / 100f, 1, 1);
            }
            else
            {
                if (_hideOnNull)
                {
                    _bar.SetActive(false);
                }
            }
        }

        if (_text != null)
        {
            _text.text = text;

            if (_hideOnNull)
            {
                _text.gameObject.SetActive(value > 0);
            }
        }
    }
}