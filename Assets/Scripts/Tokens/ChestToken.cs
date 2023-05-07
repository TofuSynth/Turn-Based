using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tokens/Chest Token")]
public class ChestToken : ScriptableObject
{
    public bool isOpened
    {
        get
        {
            return PlayerPrefs.GetInt("chest" + this.GetInstanceID()) == 1;
        }
        set
        {
            PlayerPrefs.SetFloat("chest" + this.GetInstanceID(), value?1:0);
            PlayerPrefs.Save();
        }
    }
    
    [SerializeField] public string test = "potion";
}
