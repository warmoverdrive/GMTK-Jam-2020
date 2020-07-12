using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] float audioVolume = 0.5f;
    [SerializeField] AudioClip selectSFX;

    int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadCurrentScene() 
    {
        AudioSource.PlayClipAtPoint(selectSFX, Camera.main.transform.position, audioVolume);
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextScene()
    {
        currentSceneIndex += 1;

        AudioSource.PlayClipAtPoint(selectSFX, Camera.main.transform.position, audioVolume);
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings ) SceneManager.LoadScene(currentSceneIndex);
        else LoadFirstScene();
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioSource.PlayClipAtPoint(selectSFX, Camera.main.transform.position, audioVolume);
            Application.Quit();
        }
    }
}
