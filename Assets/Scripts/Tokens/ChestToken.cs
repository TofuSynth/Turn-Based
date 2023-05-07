using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tokens/Chest Token")]
public class ChestToken : ScriptableObject
{
    public bool isOpened = false;

    [SerializeField] public string test = "potion";


}
