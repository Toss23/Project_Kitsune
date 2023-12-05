using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Collections.Generic;

public class PlayerData
{
    public event Action OnCheat;

    public enum Key
    {
        Map
    }

    private enum Types
    {
        intValue, floatValue, stringValue, boolValue
    }

    private Dictionary<Key, Types> KeyType = new Dictionary<Key, Types>()
    {
        { Key.Map, Types.stringValue }
    };

    public PlayerData()
    {
        if (PlayerPrefs.HasKey("FirstInit") == false)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("FirstInit", 1);
            UpdateHash();
        }
        else
        {
            if (CheckHash() == false)
            {
                OnCheat?.Invoke();
                PlayerPrefs.DeleteAll();
            }
        }
    }

    public bool SetValue(Key key, object value)
    {
        if (CheckHash() == true)
        {
            if (value.GetType() == typeof(int))
            {
                if (KeyType[key] != Types.intValue)
                {
                    return false;
                }
            }

            if (value.GetType() == typeof(float))
            {
                if (KeyType[key] != Types.floatValue)
                {
                    return false;
                }
            }

            if (value.GetType() == typeof(string))
            {
                if (KeyType[key] != Types.stringValue)
                {
                    return false;
                }
            }

            if (value.GetType() == typeof(bool))
            {
                if (KeyType[key] != Types.boolValue)
                {
                    return false;
                }
            }

            switch (KeyType[key])
            {
                case Types.intValue:
                    PlayerPrefs.SetInt(key.ToString(), (int)value);
                    break;
                case Types.floatValue:
                    PlayerPrefs.SetFloat(key.ToString(), (float)value);
                    break;
                case Types.stringValue:
                    PlayerPrefs.SetString(key.ToString(), (string)value);
                    break;
                case Types.boolValue:
                    PlayerPrefs.SetInt(key.ToString(), (bool)value ? 1 : 0);
                    break;
            }

            UpdateHash();
            return true;
        }
        else
        {
            OnCheat?.Invoke();
            PlayerPrefs.DeleteAll();
            return false;
        }
    }

    public bool AddValue(Key key, object value)
    {
        if (CheckHash() == true)
        {
            var a = GetValue(key);

            if (a != null)
            {
                switch (KeyType[key])
                {
                    case Types.intValue:
                        SetValue(key, (int)a + (int)value);
                        break;
                    case Types.floatValue:
                        SetValue(key, (float)a + (float)value);
                        break;
                    case Types.stringValue:
                        SetValue(key, (string)a + (string)value);
                        break;
                    case Types.boolValue:
                        Debug.Log("[SaveData] Cant add bool and bool values! (" + key + ")");
                        break;
                }
            }
            else
            {
                SetValue(key, value);
            }
        }
        else
        {
            OnCheat?.Invoke();
            PlayerPrefs.DeleteAll();
            return false;
        }

        return false;
    }

    public object GetValue(Key key)
    {
        if (CheckHash() == true)
        {
            if (HasKey(key) == true)
            {
                switch (KeyType[key])
                {
                    case Types.intValue:
                        return PlayerPrefs.GetInt(key.ToString());
                    case Types.floatValue:
                        return PlayerPrefs.GetFloat(key.ToString());
                    case Types.stringValue:
                        return PlayerPrefs.GetString(key.ToString());
                    case Types.boolValue:
                        return PlayerPrefs.GetInt(key.ToString()) == 1;
                }
            }
            else
            {
                return null;
            }
        }
        else
        {
            OnCheat?.Invoke();
            PlayerPrefs.DeleteAll();
            return null;
        }

        return null;
    }

    public bool HasKey(Key key)
    {
        return PlayerPrefs.HasKey(key.ToString());
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    private string GenerateHash()
    {
        string input = "";

        for (int key = 0; key < Enum.GetNames(typeof(Key)).Length; key++)
        {
            switch (KeyType[(Key)key])
            {
                case Types.intValue:
                    input += PlayerPrefs.GetInt(((Key)key).ToString()).ToString();
                    break;
                case Types.floatValue:
                    input += PlayerPrefs.GetFloat(((Key)key).ToString()).ToString();
                    break;
                case Types.stringValue:
                    input += PlayerPrefs.GetString(((Key)key).ToString()).ToString();
                    break;
                case Types.boolValue:
                    input += PlayerPrefs.GetInt(((Key)key).ToString()).ToString();
                    break;
            }
        }

        input += SystemInfo.deviceUniqueIdentifier;

        SHA256 sha = SHA256.Create();
        byte[] inputBytes = Encoding.ASCII.GetBytes(input);
        byte[] hash = sha.ComputeHash(inputBytes);

        string result = "";
        for (int i = 0; i < hash.Length; i++)
        {
            result += hash[i];
        }

        return result;
    }

    private bool CheckHash()
    {
        if (PlayerPrefs.HasKey("Hash") == false)
        {
            return false;
        }

        string hash = PlayerPrefs.GetString("Hash");
        return hash.Equals(GenerateHash());
    }

    private void UpdateHash()
    {
        PlayerPrefs.SetString("Hash", GenerateHash());
    }

}
