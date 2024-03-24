using UnityEngine;

[RequireComponent(typeof(AdminView))]
public class AdminPresenter : MonoBehaviour
{
    private CharacterPresenter _characterPresenter;

    private IAdminView _adminView;

    private int _abilityNumber = -1;

    public void Init(CharacterPresenter characterPresenter)
    {
        _characterPresenter = characterPresenter;
        _adminView = GetComponent<AdminView>();
        Enable();
        _adminView.HidePanel();
        ChangeAbilityToUp();
    }

    public void Enable()
    {
        _adminView.OpenButton.onClick.AddListener(_adminView.ShowPanel);
        _adminView.EscButton.onClick.AddListener(_adminView.HidePanel);

        _adminView.LevelUpButton.onClick.AddListener(_characterPresenter.Unit.AttributesContainer.Level.LevelUp);
        _adminView.MaxLevelButton.onClick.AddListener(() => _characterPresenter.Unit.AttributesContainer.Level.AddExperience(1000000));
        _adminView.ExpGainButton.onClick.AddListener(SetExpGain);
        _adminView.AbilitiesUpButton.onClick.AddListener(AllAbilitiesMaxLevel);
        _adminView.AbilityNumberButton.onClick.AddListener(ChangeAbilityToUp);
        _adminView.AbilityUpButton.onClick.AddListener(() => _characterPresenter.Unit.AbilitiesContainer.LevelUp(_abilityNumber));
        _adminView.AbilityUpButton.onClick.AddListener(UpdateAbilityUpText);
        _adminView.ImmuneButton.onClick.AddListener(Immune);
    }

    public void Disable()
    {
        _adminView.OpenButton.onClick.RemoveListener(_adminView.ShowPanel);
        _adminView.EscButton.onClick.RemoveListener(_adminView.HidePanel);

        _adminView.MaxLevelButton.onClick.RemoveListener(_characterPresenter.Unit.AttributesContainer.Level.LevelUp);
        _adminView.MaxLevelButton.onClick.RemoveListener(() => _characterPresenter.Unit.AttributesContainer.Level.AddExperience(1000000));
        _adminView.ExpGainButton.onClick.RemoveListener(SetExpGain);
        _adminView.AbilitiesUpButton.onClick.RemoveListener(AllAbilitiesMaxLevel);
        _adminView.AbilityNumberButton.onClick.RemoveListener(ChangeAbilityToUp);
        _adminView.AbilityUpButton.onClick.RemoveListener(() => _characterPresenter.Unit.AbilitiesContainer.LevelUp(_abilityNumber));
        _adminView.AbilityUpButton.onClick.RemoveListener(UpdateAbilityUpText);
        _adminView.ImmuneButton.onClick.RemoveListener(Immune);
    }

    private void SetExpGain()
    {
        bool canGain = _characterPresenter.Unit.AttributesContainer.Level.CanGainExperience;
        _characterPresenter.Unit.AttributesContainer.Level.CanGainExperience = !canGain;
        _adminView.ExpGainText.text = "Experience Gain: " + (!canGain ? "Enable" : "Disable");
    }

    private void AllAbilitiesMaxLevel()
    {
        AbilitiesContainer abilitiesContainer = _characterPresenter.Unit.AbilitiesContainer;
        for (int i = 0; i < abilitiesContainer.List.Length; i++)
        {
            for (int j = 0; j < abilitiesContainer.MaxLevels[i] - abilitiesContainer.Levels[i]; j++)
            {
                abilitiesContainer.LevelUp(i);
            }
        }
    }

    private void ChangeAbilityToUp()
    {
        _abilityNumber++;
        if (_abilityNumber > 5)
        {
            _abilityNumber = 0;
        }
        UpdateAbilityUpText();
    }

    private void UpdateAbilityUpText()
    {
        AbilitiesContainer abilities = _characterPresenter.Unit.AbilitiesContainer;
        if (abilities.List[_abilityNumber] != null)
        {
            _adminView.AbilityNumberText.text = abilities.List[_abilityNumber].AbilityData.Name + "\n(" + abilities.Levels[_abilityNumber] + " Level)";
        }
    }

    private void Immune()
    {
        bool isImmune = _characterPresenter.Unit.IsImmune;
        _characterPresenter.Unit.Immune(!isImmune);
        _adminView.ImmuneText.text = "Immune Damage: " + (!isImmune ? "Enable" : "Disable");
    }
}