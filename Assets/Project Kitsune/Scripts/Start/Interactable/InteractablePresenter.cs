using UnityEngine;

[RequireComponent(typeof(InteractableView))]
public class InteractablePresenter : MonoBehaviour
{
    [SerializeField] private UIPresenter _uiPresenter;

    private IInteractableView _interactableView;
    private IActionView _actionView;
    private IUIView _uiView;

    public void Init(StartContext context)
    {
        _interactableView = GetComponent<InteractableView>();
        _actionView = context.ActionView;
        _uiView = _uiPresenter.UIView;

        Enable();
    }

    private void Enable()
    {      
        _interactableView.OnTriggerEnter += _actionView.Show;
        _interactableView.OnTriggerEnter += EnableActionButton;
        _interactableView.OnTriggerExit += _actionView.Hide;
        _interactableView.OnTriggerExit += DisableActionButton;
    }

    private void Disable()
    {
        _interactableView.OnTriggerEnter -= _actionView.Show;
        _interactableView.OnTriggerEnter -= EnableActionButton;
        _interactableView.OnTriggerExit -= _actionView.Hide;
        _interactableView.OnTriggerExit -= DisableActionButton;
    }

    private void EnableActionButton()
    {
        _actionView.ActionButton.onClick.AddListener(_uiView.Show);
    }

    private void DisableActionButton()
    {
        _actionView.ActionButton.onClick.RemoveListener(_uiView.Show);
    }
}
