using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EscapedCounter : MonoBehaviour
{
    int escapedGoal;

    LevelController levelController;
    TextMeshProUGUI text;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        levelController = FindObjectOfType<LevelController>();

        escapedGoal = levelController.GetGoalActorsToEscape();

        UpdateEscapedCounter(0);
    }

    public void UpdateEscapedCounter(int actorsEscaped)
    {
        text.text = actorsEscaped + "/" + escapedGoal;
    }
}
