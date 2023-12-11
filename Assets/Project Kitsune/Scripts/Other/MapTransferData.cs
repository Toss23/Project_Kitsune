using System;
using UnityEngine;

public class MapTransferData
{
    public Configs.Map SelectedMap { get; private set; }
    public Configs.Character SelectedCharacter { get; private set; }

    public MapTransferData(Configs.Map selectedMap, Configs.Character selectedCharacter)
    {
        SelectedMap = selectedMap;
        SelectedCharacter = selectedCharacter;
    }

    public bool Save()
    {
        PlayerData playerData = new PlayerData();

        if (playerData.SetValue(PlayerData.Key.Map, (int)SelectedMap) == false)
        {
            return false;
        }

        if (playerData.SetValue(PlayerData.Key.Character, (int)SelectedCharacter) == false)
        {
            return false;
        }

        return true;
    }

    /*
    public static MapTransferData Load()
    {
        PlayerData playerData = new PlayerData();

        int map = (int)playerData.GetValue(PlayerData.Key.Map);
        int character = (int)playerData.GetValue(PlayerData.Key.Character);

        return new MapTransferData((Configs.Map)map, (Configs.Character)character);
    }*/
}
