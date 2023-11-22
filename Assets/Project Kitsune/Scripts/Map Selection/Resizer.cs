using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(Image))]
public class Resizer : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    [SerializeField][Range(0f, 1f)] private float _verticalSize = 0;
    [SerializeField][Range(0f, 1f)] private float _horizontalSize = 0;
    [SerializeField] private int _verticaleDelta = 0;
    [SerializeField] private int _horizontalDelta = 0;

    private void Update()
    {
        if (_parent != null)
        {
            Image image = GetComponent<Image>();
            RectTransform parentRect = (RectTransform)_parent.transform;

            if (_verticalSize != 0)
            {
                image.rectTransform.sizeDelta = new Vector2(parentRect.rect.width * _verticalSize + _verticaleDelta, image.rectTransform.sizeDelta.y);
            }

            if (_horizontalSize != 0)
            {
                image.rectTransform.sizeDelta = new Vector2(image.rectTransform.sizeDelta.x, parentRect.rect.height * _horizontalSize + _horizontalDelta);
            }
        }
    }
}
