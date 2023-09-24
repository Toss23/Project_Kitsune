using UnityEngine;

public class UIGroup : MonoBehaviour, IUIGroup
{
    [SerializeField] private GameObject _content;

    public void Show()
    {
        _content.SetActive(true);
    }

    public void Hide()
    {
        _content.SetActive(false);
    }
}