using System;
using UnityEngine;

public class AbilitiesSelectionView : MonoBehaviour, IAbilitiesSelectionView
{
    public event Action<IAbility> OnSelected;

    [SerializeField] private GameObject _content;
    [SerializeField] private AbilityCardView[] _abilityCards;

    private void Awake()
    {
        Hide();
        foreach (IAbilityCardView abilityCard in _abilityCards)
            abilityCard.OnClick += OnClickCard;
    }

    public void Build(IAbility[] abilities)
    {
        IAbilityCardView[] abilityCards = _abilityCards;
        for (int i = 0; i < abilities.Length; i++)
            abilityCards[i].SetAbility(abilities[i]);

        Show();
    }

    public void Show()
    {
        _content.SetActive(true);
    }

    public void Hide()
    {
        _content.SetActive(false);
    }

    private void OnClickCard(IAbility ability)
    {
        Hide();
        OnSelected?.Invoke(ability);
    }
}