using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour, ITimerView
{
    [SerializeField] private TMP_Text _timerText;

    public void SetTime(int seconds)
    {
        int minutes = Mathf.FloorToInt(seconds / 60f);
        seconds = seconds % 60;

        string minutesText = minutes < 10 ? "0" + minutes : "" + minutes;
        string secondsText = seconds < 10 ? "0" + seconds : "" + seconds;

        _timerText.text = minutesText + ":" + secondsText;
    }
}
