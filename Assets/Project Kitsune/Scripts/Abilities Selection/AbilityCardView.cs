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
                _nameText.text = ability.AbilityData.Name;
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
                AbilityData abilityData = ability.AbilityData;

                string description = abilityData.Description;

                Dictionary<string, float> replace = new Dictionary<string, float>();

                replace.Add("#Scale", abilityData.Scale.Get(level));
                replace.Add("#Duration", abilityData.Duration.Get(level));

                if (ability.AbilityData.GetAbilityType() == AbilityData.Type.Base 
                    | ability.AbilityData.GetAbilityType() == AbilityData.Type.Range)
                {
                    BaseAbilityData baseAbilityData = (BaseAbilityData)ability.AbilityData;
                    replace.Add("#Damage", baseAbilityData.Damage.Get(level));
                    replace.Add("#Multiplier", baseAbilityData.DamageMultiplier.Get(level));
                    replace.Add("#CastPerSecond", baseAbilityData.CastPerSecond.Get(level));
                    replace.Add("#CritChance", baseAbilityData.CritChance.Get(level));
                    replace.Add("#CritMultiplier", baseAbilityData.CritMultiplier.Get(level));
                    replace.Add("#DotRate", baseAbilityData.DotRate.Get(level));
                }

                if (ability.AbilityData.GetAbilityType() == AbilityData.Type.Range)
                {
                    RangeAbilityData rangeAbilityData = (RangeAbilityData)ability.AbilityData;
                    replace.Add("#Speed", rangeAbilityData.Speed);
                    replace.Add("#Count", rangeAbilityData.Count.Get(level));
                    replace.Add("#TiltAngle", rangeAbilityData.TiltAngle.Get(level));
                }

                foreach (AbilityProperty property in ability.AbilityData.AbilityProperties)
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