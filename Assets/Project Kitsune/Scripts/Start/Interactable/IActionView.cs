using UnityEngine.UI;

public interface IActionView
{
    public Button ActionButton { get; }
    public void Init();
    public void Show();
    public void Hide();
}