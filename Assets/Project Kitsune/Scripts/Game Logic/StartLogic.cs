using System;
using UnityEngine;

public class StartLogic : BaseLogic
{
    [SerializeField] private CharacterPresenter _character;

    protected override IUnitPresenter SetCharacter() => _character;

    protected override void LoadGame()
    {
        _character.Init(this, UnitType.Character);
        _character.Character.Abilities.SetActive(false);
        Debug.Log("[GL] Character initialized...");
    }

    protected override void OnContinue()
    {
        
    }

    protected override void OnPause()
    {
        
    }
}