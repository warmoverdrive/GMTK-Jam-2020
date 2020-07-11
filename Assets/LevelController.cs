using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] ActorMovement actor;
    [SerializeField] int totalActorsToSpawn = 20;
    [SerializeField] float timeBetweenSpawn = 1f;
    [SerializeField] int goalActorsToEscape = 10;

    Entrance entrance;
    //Exit exit;

    int currentActorsSpawned = 0;
    int actorsEscaped = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        entrance = FindObjectOfType<Entrance>();
        if (!entrance) { Debug.LogError("No Entrance Found!"); return; }

        StartCoroutine(SpawnActors());
        //exit = FindObjectOfType<Exit>();
    }

    IEnumerator SpawnActors()
    {
        while(currentActorsSpawned < totalActorsToSpawn)
        {
            entrance.SpawnActor(actor);
            currentActorsSpawned++;
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }
}
