#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tofu.TurnBased.SceneManagement
{
    public class DoorToken : ScriptableObject
    {
        [SerializeField, HideInInspector] private string targetDoorName;


        public string TargetDoorName
        {
            get { return targetDoorName; }
        }
#if UNITY_EDITOR
        [SerializeField] private GameObject targetDoor;

        private void OnValidate()
        {
            if (!targetDoor) return;
            targetDoorName = targetDoor.name;
        }

        [MenuItem("Assets/Create/Tokens/Door Token")]

        private static void Create()
        {

            GameObject door = Selection.activeObject as GameObject;
            if (door == null)
            {
                Debug.Log("Please select the Door you wish to create a token for");
                return;
            }

            string[] guids = AssetDatabase.FindAssets("t:" + typeof(DoorToken));
            for (int i = 0; i < guids.Length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                var token = AssetDatabase.LoadAssetAtPath<DoorToken>(assetPath);
                if (door.targetDoor == door)
                {
                    Debug.Log("Token already exists for this door");
                    Selection.activeObject = token;
                    return;
                }
            }

 
            string path = AssetDatabase.GetAssetPath(door.GetInstanceID());
            if (path.Contains("."))
            {
                path = path.Remove(path.LastIndexOf("."));
            }


            var instance = ScriptableObject.CreateInstance<SceneToken>();
            instance.targetDoor = door;
            AssetDatabase.CreateAsset(instance, path + ".asset");


#endif
        }
    }
}