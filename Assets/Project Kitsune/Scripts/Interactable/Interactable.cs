using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private UIGroup _uiGroup;

    public IUIGroup UIGroup => _uiGroup;

    public void OnClick()
    {
        if (UIGroup != null)
        {
            UIGroup.Show();
        }
    }
}
