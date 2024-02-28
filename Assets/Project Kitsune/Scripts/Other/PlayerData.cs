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
        Map, Character
    }

    private Dictionary<Key, Type> _keyType = new Dictionary<Key, Type>()
    {
        { Key.Map, typeof(string) },
        { Key.Character, typeof(string) }
    };

    public bool FirstInit { get; private set; } = false;

    public PlayerData()
    {
        if (PlayerPrefs.HasKey("FirstInit") == false)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("FirstInit", 1);
            UpdateHash();
            FirstInit = true;
        }
        else
        {
            if (CheckHash() == false)
            {
                OnCheatDetected();
            }
        }
    }

    public bool SetValue<T>(Key key, T value)
    {
        if (CheckHash() == true)
        {
            if (typeof(T) == _keyType[key])
            {
                if (typeof(T) == typeof(int))
                {
                    PlayerPrefs.SetInt(key.ToString(), int.Parse(value.ToString()));
                }
                else if (typeof(T) == typeof(string))
                {
                    PlayerPrefs.SetString(key.ToString(), value.ToString());
                }
                else if (typeof(T) == typeof(float))
                {
                    PlayerPrefs.SetFloat(key.ToString(), float.Parse(value.ToString()));
                }
                else if (typeof(T) == typeof(bool))
                {
                    PlayerPrefs.SetInt(key.ToString(), int.Parse(value.ToString() == "true" ? "1" : "0"));
                }
                else
                {
                    Debug.LogError("[PlayerData] Not support typeof(" + typeof(T) + ") with key: " + key.ToString());
                    return false;
                }

                UpdateHash();
                return true;
            }
            else
            {
                Debug.LogError("[PlayerData] Not support typeof(" + typeof(T) + ") with key: " + key.ToString());
                return false;
            }
        }
        else
        {
            OnCheatDetected(); 
            return false;
        }
    }

    public bool GetValue<T>(Key key, out T value)
    {
        value = default(T);

        if (CheckHash() == true)
        {
            if (typeof(T) == _keyType[key])
            {
                if (typeof(T) == typeof(int))
                {
                    value = (T)(object)PlayerPrefs.GetInt(key.ToString());
                }
                else if (typeof(T) == typeof(string))
                {
                    value = (T)(object)PlayerPrefs.GetString(key.ToString());
                }
                else if (typeof(T) == typeof(float))
                {
                    value = (T)(object)PlayerPrefs.GetFloat(key.ToString());
                }
                else if (typeof(T) == typeof(bool))
                {
                    value = (T)(object)(PlayerPrefs.GetInt(key.ToString()) == 1);
                }
                else
                {
                    Debug.LogError("[PlayerData] Not support typeof(" + typeof(T) + ") with key: " + key.ToString());
                    return false;
                }

                return true;
            }
            else
            {
                Debug.LogError("[PlayerData] Not support typeof(" + typeof(T) + ") with key: " + key.ToString());
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void OnCheatDetected()
    {
        Debug.LogError("[PlayerData] Incorrect data detected");
        OnCheat?.Invoke();
        DeleteAll();
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
            string value = "";

            if (_keyType[(Key)key] == typeof(string))
            {
                value = PlayerPrefs.GetString(((Key)key).ToString());
            }
            else if (_keyType[(Key)key] == typeof(int))
            {
                value = PlayerPrefs.GetInt(((Key)key).ToString()).ToString();
            }
            else if (_keyType[(Key)key] == typeof(float))
            {
                value = PlayerPrefs.GetFloat(((Key)key).ToString()).ToString();
            }
            else if (_keyType[(Key)key] == typeof(bool))
            {
                value = PlayerPrefs.GetInt(((Key)key).ToString()).ToString();
            }
            else
            {
                Debug.Log("[PlayerData] Can't generate hash with key: " + (Key)key + ", typeof(" + _keyType[(Key)key] + ")");
            }

            input += value;
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
