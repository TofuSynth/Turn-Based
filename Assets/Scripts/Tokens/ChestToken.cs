using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tokens/Chest Token")]
public class ChestToken : BoolStateScriptableObject
{
    public bool IsOpened {
        get { return SavedState; }
        set { SavedState = value; }
    }
}


