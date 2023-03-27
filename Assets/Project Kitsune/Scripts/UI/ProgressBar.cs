using UnityEngine;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject _bar;
    [SerializeField] private TMP_Text _text;

    [Header("Values")]
    [SerializeField] private bool _useStartValues = false;
    [SerializeField][Range(0, 100)] private int _startPercent = 100;
    [SerializeField] private string _startText = "100 / 100";

    private Transform _barTransform;

    private void Start()
    {
        if (_bar != null)
            _barTransform = _bar.transform;

        if (_useStartValues)
            SetPercentAndText(_startPercent, _startText);
    }

    public void SetPercentAndText(int value, string text)
    {
        if (_barTransform != null)
            _barTransform.localScale = new Vector3(value / 100f, 1, 1);
        
        if (_text != null)
            _text.text = text;
    }
}