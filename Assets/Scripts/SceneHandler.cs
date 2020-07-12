using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        print(currentSceneIndex);
    }

    public void LoadCurrentScene() 
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextScene()
    {
        currentSceneIndex += 1;

        if (SceneManager.sceneCount > currentSceneIndex) SceneManager.LoadScene(currentSceneIndex);
        else LoadFirstScene();
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }
}
