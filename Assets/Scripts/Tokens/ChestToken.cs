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
            return SaveData.GetBool("chest" + this.GetInstanceID());
        }
        set
        {
            SaveData.SetBool("chest" + this.GetInstanceID(),true);
        }
    }
    
    [SerializeField] public string test = "potion";
}

