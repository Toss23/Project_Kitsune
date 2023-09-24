using UnityEngine;

public class Interactable : Clickable3D
{
    [SerializeField] private UIGroup _uiGroup;

    public IUIGroup UIGroup => _uiGroup;

    public override void OnClick()
    {
        Debug.Log("Clicked");
        if (UIGroup != null)
        {
            UIGroup.Show();
        }
    }
}
