using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI scoreText;

    public Camera mainCamera;
    public Canvas canvas;

    void Awake()
    {
        SetUpSingleton();

        UpdateScore(0);
        canvas = GetComponent<Canvas>();
    }

    public void UpdateScore(int scoreAdded) 
    {
        score += scoreAdded;
        scoreText.text = score.ToString();
    }

    private void Update()
    {
        if(mainCamera == null)
        {
            mainCamera = FindObjectOfType<Camera>();

            canvas.worldCamera = mainCamera;
        }
    }

    public int GetScore() { return score; }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
