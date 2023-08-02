using UnityEngine;
using UnityEngine.UI;

public class AdaptiveUI : MonoBehaviour
{
    private enum ScaleMode
    {
        Both, Height, Width
    }

    [Header("Main")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private ScaleMode scaleMode = ScaleMode.Height;
    [SerializeField] private bool updateEveryFrame = true;

    private CanvasScaler canvasScaler;
    private RectTransform rectTransform;

    private Vector2 referenceResolution;
    private Vector2 currentResolution;
    private Vector2 scale;
    private float currentScale;

    private void Awake()
    {
        canvasScaler = canvas.GetComponent<CanvasScaler>();
        rectTransform = canvas.GetComponent<RectTransform>();
    }

    private void Start()
    {
        Resize();
    }

    private void Update()
    {
        if (updateEveryFrame) Resize();
    }

    private void Resize()
    {
        referenceResolution = canvasScaler.referenceResolution;
        currentResolution = rectTransform.rect.size;
        scale = currentResolution / referenceResolution;

        switch (scaleMode)
        {
            case ScaleMode.Both:
                currentScale = scale.x < scale.y ? scale.x : scale.y;
                break;
            case ScaleMode.Height:
                currentScale = scale.y;
                break;
            case ScaleMode.Width:
                currentScale = scale.x;
                break;
        }

        transform.localScale = Vector3.one * currentScale;
    }
}