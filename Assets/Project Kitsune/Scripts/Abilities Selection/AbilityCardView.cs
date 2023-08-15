using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(Button))]
public class AbilityCardView : MonoBehaviour, IAbilityCardView
{
    public event Action<IAbility> OnClick;

    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _descriptionText;

    private IAbility _ability;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClickButton);
    }

    public void SetAbility(IAbility ability, int level)
    {
        if (ability != null)
        {
            _ability = ability;

            level++;

            if (_nameText != null)
            {
                _nameText.text = ability.Info.Name;
            }

            if (_iconImage != null)
            {
                // WIP
            }

            if (_levelText != null)
            {
                _levelText.text = "Level: " + level;
            }

            if (_descriptionText != null)
            {
                string description = ability.Info.Description;

                Dictionary<string, float> replace = new Dictionary<string, float>();
                replace.Add("#Damage", ability.Info.Damage[level]);
                replace.Add("#Multiplier", ability.Info.DamageMultiplier[level]);
                replace.Add("#CastPerSecond", ability.Info.CastPerSecond[level]);
                replace.Add("#CritChance", ability.Info.CritChance[level]);
                replace.Add("#CritMultiplier", ability.Info.CritMultiplier[level]);
                replace.Add("#ProjectileCount", ability.Info.ProjectileCount[level]);
                replace.Add("#ProjectileAngle", ability.Info.ProjectileAngle[level]);
                replace.Add("#DotRate", ability.Info.DotRate[level]);
                replace.Add("#DotDuration", ability.Info.Duration[level]);
                replace.Add("#Radius", ability.Info.Scale[level]);

                foreach (AbilityProperty property in ability.Info.AbilityProperties)
                {
                    replace.Add("#" + property.Name, property.Values[level]);
                }

                foreach (KeyValuePair<string, float> entry in replace)
                {
                    description = description.Replace(entry.Key, "<color=red>" + entry.Value + "</color>");
                }

                _descriptionText.text = description;
            }
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