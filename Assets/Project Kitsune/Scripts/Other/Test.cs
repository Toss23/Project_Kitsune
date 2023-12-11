using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        PlayerData data = new PlayerData();

        data.SetValue(PlayerData.Key.Character, "123");
        data.SetValue(PlayerData.Key.Map, 1);
        data.GetValue(PlayerData.Key.Character, out string value);

        Debug.Log(value);
    }
}
