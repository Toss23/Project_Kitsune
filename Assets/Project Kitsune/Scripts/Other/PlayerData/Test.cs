using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        PlayerData sd = new PlayerData();
        if (sd.SetValue(PlayerData.Key.Map, "1") == true)
        {
            Debug.Log(sd.GetValue(PlayerData.Key.Map));
        }

        sd.AddValue(PlayerData.Key.Map, "1");
        Debug.Log(sd.GetValue(PlayerData.Key.Map));
    }
}
