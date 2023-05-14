using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

    public class BoolStateObject
    {
        [SerializeField, ReadOnly] private string key;

        public bool State
        {
            get { return SaveData.GetBool(key); }
            set { SaveData.SetBool(key, value); }
        }

        public void SetSaveKey(string prefix, int uniqueID, string subID)
        {
            key = prefix + uniqueID + "." + subID;
        }
    }