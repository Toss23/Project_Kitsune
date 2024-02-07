public class MapData
{
    public Configs.Map SelectedMap;
    public Configs.Character SelectedCharacter;

    private PlayerData _playerData;

    public MapData(Configs.Map selectedMap, Configs.Character selectedCharacter)
    {
        _playerData = new PlayerData();

        SelectedMap = selectedMap;
        SelectedCharacter = selectedCharacter;

        Save();
    }

    public MapData()
    {
        _playerData = new PlayerData();

        Load();
    }

    public bool Save()
    {
        if (_playerData.SetValue(PlayerData.Key.Map, (int)SelectedMap) == false)
        {
            return false;
        }

        if (_playerData.SetValue(PlayerData.Key.Character, (int)SelectedCharacter) == false)
        {
            return false;
        }

        return true;
    }

    public void Load()
    {
        if (_playerData.HasKey(PlayerData.Key.Map))
        {
            _playerData.GetValue(PlayerData.Key.Map, out int map);
            SelectedMap = (Configs.Map)map;
        }
        else
        {
            SelectedMap = Configs.Map.Start;
        }

        if (_playerData.HasKey(PlayerData.Key.Character))
        {
            _playerData.GetValue(PlayerData.Key.Character, out int character);
            SelectedCharacter = (Configs.Character)character;
        }
        else
        {
            SelectedCharacter = Configs.Character.Kitsune;
        }
    }
}
