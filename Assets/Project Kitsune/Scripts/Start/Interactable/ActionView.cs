using UnityEngine;
using UnityEngine.UI;

public class ActionView : MonoBehaviour, IActionView
{
    private Button _actionButton;

    public Button ActionButton => _actionButton;

    public void Init()
    {
        _actionButton = GetComponent<Button>();
        Hide();
    }

    public void Show()
    {
        _actionButton.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _actionButton.gameObject.SetActive(false);
    }
}