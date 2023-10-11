using UnityEngine;

[RequireComponent(typeof(UIView))]
public class UIPresenter : MonoBehaviour
{
    private IUIView _uiView;

    public IUIView UIView => _uiView;

    public void Init()
    {
        _uiView = GetComponent<UIView>();
        _uiView.Init();
    }
}