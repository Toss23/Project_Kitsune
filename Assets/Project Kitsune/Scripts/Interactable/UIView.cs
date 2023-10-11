using UnityEngine;
using UnityEngine.UI;

public class UIView : MonoBehaviour, IUIView
{
    [SerializeField] private GameObject _content;
    [SerializeField] private Button _closeButton;

    public void Init()
    {
        _closeButton.onClick.AddListener(Hide);
        Hide();
    }

    public void Show()
    {
        _content.SetActive(true);
    }

    public void Hide()
    {
        _content.SetActive(false);
    }
}
