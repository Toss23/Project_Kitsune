using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IAdminView
{
    public GameObject Content { get; }
    public Button OpenButton { get; }
    public Button EscButton { get; }

    public Button LevelUpButton { get; }
    public Button MaxLevelButton { get; }
    public Button ExpGainButton { get; }
    public TMP_Text ExpGainText { get; }
    public Button AbilitiesUpButton { get; }
    public Button AbilityUpButton { get; }
    public Button AbilityNumberButton { get; }
    public TMP_Text AbilityNumberText { get; }
    public Button ImmuneButton { get; }
    public TMP_Text ImmuneText { get; }

    public void ShowPanel();
    public void HidePanel();
}
