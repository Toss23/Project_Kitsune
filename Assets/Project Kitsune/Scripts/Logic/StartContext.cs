using System.Collections.Generic;
using UnityEngine;

public class StartContext : BaseLogic
{
    [SerializeField] private CharacterPresenter _character;
    [SerializeField] private ActionView _actionView;
    [SerializeField] private List<InteractablePresenter> _interactablePresenters;
    [SerializeField] private List<UIPresenter> _uiPresenters;

    protected override IUnitPresenter SetCharacter() => _character;

    public IActionView ActionView => _actionView;

    protected override void OnLoadGame()
    {
        _character.Init(this, UnitType.Character);
        _character.Character.Abilities.SetActive(false);
        Debug.Log("[Context] Initialized: Character");

        ActionView.Init();
        Debug.Log("[Context] Initialized: ActionView");

        if (_uiPresenters != null)
        {
            foreach (UIPresenter ui in _uiPresenters)
            {
                ui.Init();
                Debug.Log("[Context] Initialized: " + ui.name);
            }
        }

        if (_interactablePresenters != null)
        {
            foreach (InteractablePresenter interactable in _interactablePresenters)
            {
                interactable.Init(this);
                Debug.Log("[Context] Initialized: " + interactable.name);
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