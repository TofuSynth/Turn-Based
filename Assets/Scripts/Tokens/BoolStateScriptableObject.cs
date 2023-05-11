using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolStateScriptableObject : ScriptableObject
{
    protected bool SavedState
    {
        get
        {
            return SaveData.GetBool("conditional" + this.GetInstanceID());
        }
        set
        {
            SaveData.SetBool("conditional" + this.GetInstanceID(),true);
        }
    }
    
    
}

