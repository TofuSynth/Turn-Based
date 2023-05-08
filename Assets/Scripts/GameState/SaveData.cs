using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor.Internal;
using UnityEngine;

public static class SaveData
{
    public static bool GetBool(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key) == 1;
        }
        else
        {
            return false;
        }
    }

    public static void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
        PlayerPrefs.Save();
    }
}
