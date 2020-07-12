using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI storyTextBox;
    [SerializeField] string[] storyText;

    [Range(0, 1)] [SerializeField] float audioVolume = 0.5f;
    [SerializeField] AudioClip selectSFX;


    SceneHandler sceneHandler;

    int currentStoryIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        sceneHandler = FindObjectOfType<SceneHandler>();

        if (!storyTextBox) Debug.LogError("No Text Box Assigned!");
        if (storyText.Length == 0) Debug.LogError("No story text written!");

        storyTextBox.text = storyText[currentStoryIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            currentStoryIndex++;
            if (currentStoryIndex == storyText.Length) sceneHandler.LoadNextScene();

            AudioSource.PlayClipAtPoint(selectSFX, Camera.main.transform.position, audioVolume);
            storyTextBox.text = storyText[currentStoryIndex];
        }
    }
}
