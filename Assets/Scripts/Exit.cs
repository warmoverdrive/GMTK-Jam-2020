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
        if (collision.GetComponent<ActorMovement>())
        {
            ExitActor(collision.gameObject);
            levelController.ActorEscape();
        }
        else return;
    }

    private void ExitActor(GameObject actor) 
    {
        Destroy(actor); //TODO trigger exit animation
    }
    
}
