using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AbilityCardView : MonoBehaviour, IAbilityCardView
{
    public event Action<IAbility> OnClick;

    [SerializeField] private TMP_Text _nameText;

    private IAbility _ability;

    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClickButton);
    }

    public void SetAbility(IAbility ability)
    {
        _ability = ability;
        _nameText.text = ability.Info.Description;
    }

    private void OnClickButton()
    {
        if (_ability != null)
            OnClick?.Invoke(_ability);
    }
}