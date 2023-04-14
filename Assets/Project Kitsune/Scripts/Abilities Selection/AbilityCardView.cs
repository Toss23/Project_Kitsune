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
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClickButton);
    }

    public void SetAbility(IAbility ability)
    {
        if (ability != null)
        {
            _ability = ability;
            _nameText.text = ability.Info.Description;
        }
        else
        {
            _nameText.text = "None";
        }
    }

    private void OnClickButton()
    {
        if (_ability != null)
            OnClick?.Invoke(_ability);
    }
}