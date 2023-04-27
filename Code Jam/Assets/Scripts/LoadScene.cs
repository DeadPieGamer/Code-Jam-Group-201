using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void Loadscene(string sceneName)
    { 
        SceneManager.LoadScene(sceneName);
    }

    public void Loadscene(int sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextScene()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        // If there are still scenes, load the next one. Otherwise, load the first scene
        if (scene + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(scene + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    } 
    
}
