using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LiveActorCounter : MonoBehaviour
{
    int totalActors;

    LevelController levelController;
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        text = GetComponent<TextMeshProUGUI>();
        totalActors = levelController.GetTotalActorsToSpawn();

        UpdateLiveActorCounter(0);
    }

    public void UpdateLiveActorCounter(int newCount)
    {
        text.text = newCount + "/" + totalActors;
    }
}
