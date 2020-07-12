using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    int score = 0;


    private void Start()
    {
        print("finding score handler");
        var scoreHandler = FindObjectOfType<ScoreHandler>();
        print(scoreHandler);
        score = scoreHandler.GetScore();
        print(score);
        Destroy(scoreHandler.gameObject);

        if (!scoreText) return;
        scoreText.text = score.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            FindObjectOfType<SceneHandler>().LoadNextScene();
        }
    }
}
