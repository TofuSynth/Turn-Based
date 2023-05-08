using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setup : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("Overworld");
    }
    
}
