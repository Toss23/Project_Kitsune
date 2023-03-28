using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class FpsCounter : MonoBehaviour
{
    [SerializeField] private int _maxFps = 60;

    private TMP_Text _text;
    private int _fpsCounter = 0;
    private int _fps = 0;
    private float _timer = 0;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        Application.targetFrameRate = _maxFps;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        _fpsCounter++;

        while (_timer >= 1)
        {
            _timer--;
            _fps = _fpsCounter;
            _fpsCounter = 0;
        }

        _text.text = "FPS: " + _fps; 
    }
}