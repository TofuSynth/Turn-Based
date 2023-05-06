using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tofu.TurnBased.SceneManagement
{
    public class SceneTransitionSevice : ServiceBase<SceneTransitionSevice>
    {
        public ScriptableObject targetScene;
        public ScriptableObject targetDoor;
        
        public void GoToNewScene(SceneToken token)
        { 
            SceneManager.LoadScene(token.TargetSceneName);
        }
    }
}