using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdminView : MonoBehaviour, IAdminView
{
    [Header("Title")]
    [SerializeField] private GameObject _content;
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _escButton;

    [Header("Functions")]
    [SerializeField] private Button _levelUpButton;
    [SerializeField] private Button _maxLevelButton;
    [SerializeField] private Button _expGainButton;
    [SerializeField] private TMP_Text _expGainText;
    [SerializeField] private Button _abilitiesUpButton;
    [SerializeField] private Button _abilityUpButton;
    [SerializeField] private Button _abilityNumberButton;
    [SerializeField] private TMP_Text _abilityNumberText;
    [SerializeField] private Button _immuneButton;
    [SerializeField] private TMP_Text _immuneText;

    public GameObject Content => _content;
    public Button OpenButton => _openButton;
    public Button EscButton => _escButton;

    public Button LevelUpButton => _levelUpButton;
    public Button MaxLevelButton => _maxLevelButton;
    public Button ExpGainButton => _expGainButton;
    public TMP_Text ExpGainText => _expGainText;
    public Button AbilitiesUpButton => _abilitiesUpButton;
    public Button AbilityUpButton => _abilityUpButton;
    public Button AbilityNumberButton => _abilityNumberButton;
    public TMP_Text AbilityNumberText => _abilityNumberText;
    public Button ImmuneButton => _immuneButton;
    public TMP_Text ImmuneText => _immuneText;

    public void ShowPanel()
    {
        _content?.SetActive(true);
        _openButton.gameObject?.SetActive(false);
    }

    public void HidePanel()
    {
        _content?.SetActive(false);
        _openButton.gameObject?.SetActive(true);
    }
}