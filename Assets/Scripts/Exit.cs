using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    LevelController levelController;

    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var actor = collision.GetComponent<ActorMovement>();

        if (actor)
        {
            actor.TriggerActorExit();
            levelController.ActorEscape();
        }
        else return;
    }
    
}
