using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.SceneManagement;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReferenceTest : MonoBehaviour
{
    [SerializeField] private SceneToken sceneReference;

    void Start()
    {
        Debug.Log(sceneReference.TargetSceneName);
    }
}
