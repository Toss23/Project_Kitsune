using TMPro;
using UnityEngine;

public class KillCounterView : MonoBehaviour, IKillCounterView
{
    [SerializeField] private TMP_Text _counterText;

    public void SetCount(int count)
    {
        string countText = "" + count;

        if (count < 10)
        {
            countText = "0" + countText;
        }

        if (count < 100)
        {
            countText = "0" + countText;
        }

        if (count < 1000)
        {
            countText = "0" + countText;
        }

        if (count < 10000)
        {
            countText = "0" + countText;
        }

        _counterText.text = countText;
    }
}
