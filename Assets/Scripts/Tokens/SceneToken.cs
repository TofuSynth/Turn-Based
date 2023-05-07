#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tofu.TurnBased.SceneManagement
{
    public class SceneToken : ScriptableObject
    {
        [SerializeField, HideInInspector] private string targetSceneName;

        public string TargetSceneName
        {
            get { return targetSceneName; }
        }

#if UNITY_EDITOR
        [SerializeField] private SceneAsset targetScene;

        //This is only ever called in the editor, but it's called whenever things change and should catch
        //any renaming of the scene files - I believe it is called on everything when we hit play, and on save
        private void OnValidate()
        {
            if (!targetScene) return;
            targetSceneName = targetScene.name;
        }

        //Logic to create the Scene Token itself - should be used when rightclicking a Scene file
        [MenuItem("Assets/Create/Tokens/Scene Token")]
        private static void Create()
        {
            //Grabbing the currently selected object in the unity UI. Casting it as SceneAsset will either work,
            //or it'll return null if it fails.
            SceneAsset scene = Selection.activeObject as SceneAsset;
            if (scene == null)
            {
                Debug.Log("Please select the Scene you wish to create a token for");
                return;
            }

            //Here I'm grabbing all the other ScriptableObjects in the project of type SceneToken and checking
            //if they already reference our desired scene - we shouldn't have more than one token for a scene
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(SceneToken));
            for (int i = 0; i < guids.Length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                var token = AssetDatabase.LoadAssetAtPath<SceneToken>(assetPath);
                if (token.targetScene == scene)
                {
                    Debug.Log("Token already exists for this scene");
                    Selection.activeObject = token;
                    return;
                }
            }

            //Getting the path of our selected Scene, so we can use it for our new token
            string path = AssetDatabase.GetAssetPath(scene.GetInstanceID());
            if (path.Contains("."))
            {
                path = path.Remove(path.LastIndexOf("."));
            }

            //Create the SceneToken, set its scene, save it at the path
            var instance = ScriptableObject.CreateInstance<SceneToken>();
            instance.targetScene = scene;
            AssetDatabase.CreateAsset(instance, path + ".asset");
        }
#endif
    }
}
