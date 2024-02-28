using System.Collections.Generic;
using UnityEngine;

public class StartContext : BaseContext
{
    [SerializeField] private ActionView _actionView;
    [SerializeField] private List<InteractablePresenter> _interactablePresenters;
    [SerializeField] private List<UIPresenter> _uiPresenters;

    protected override IUnitPresenter SetCharacter() => CharacterPresenter;

    public IActionView ActionView => _actionView;

    protected override void OnLoadGame()
    {
        CharacterPresenter.Character.AbilitiesContainer.SetActive(false);

        ActionView.Init();
        Message("Initialized: ActionView");

        if (_uiPresenters != null)
        {
            foreach (UIPresenter ui in _uiPresenters)
            {
                ui.Init();
                Message("Initialized: " + ui.name);
            }
        }

        if (_interactablePresenters != null)
        {
            foreach (InteractablePresenter interactable in _interactablePresenters)
            {
                interactable.Init(this);
                Message("Initialized: " + interactable.name);
            }
        }
    }

    protected override void OnContinue()
    {
        
    }

    protected override void OnPause()
    {
        
    }
}