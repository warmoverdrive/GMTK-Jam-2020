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

    LiveActorCounter activeCounter;
    EscapedCounter escapedCounter;

    int currentActorsSpawned = 0;
    int actorsEscaped = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        activeCounter = FindObjectOfType<LiveActorCounter>();
        escapedCounter = FindObjectOfType<EscapedCounter>();
        entrance = FindObjectOfType<Entrance>();
        if (!entrance) { Debug.LogError("No Entrance Found!"); return; }

        StartCoroutine(SpawnActors());
    }

    IEnumerator SpawnActors()
    {
        while(currentActorsSpawned < totalActorsToSpawn)
        {
            entrance.SpawnActor(actor);
            currentActorsSpawned++;
            activeCounter.UpdateLiveActorCounter(currentActorsSpawned);
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }

    private void RemoveActor()
    {
        currentActorsSpawned--;
        activeCounter.UpdateLiveActorCounter(currentActorsSpawned);
    }
    
    public void ActorEscape()
    {
        RemoveActor();        
        actorsEscaped++;
        escapedCounter.UpdateEscapedCounter(actorsEscaped);
        
        if(actorsEscaped == goalActorsToEscape)
        {
            print("goal reached");
            // End Level
        }
    }

    public void ActorDeath()
    {
        RemoveActor();
    }

    public int GetTotalActorsToSpawn(){ return totalActorsToSpawn; }
    public int GetGoalActorsToEscape() { return goalActorsToEscape; }
}
