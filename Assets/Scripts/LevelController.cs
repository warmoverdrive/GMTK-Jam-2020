using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Actor Prefab")]
    [SerializeField] ActorMovement actor;
    [Header("Level Parameters")]
    [SerializeField] int startingBudget = 100;
    [SerializeField] int startDelayTime = 5;
    [SerializeField] int totalActorsToSpawn = 20;
    [SerializeField] float timeBetweenSpawn = 1f;
    [SerializeField] int goalActorsToEscape = 10;
    [Header("Result Screens")]
    [SerializeField] Canvas winScreen;
    [SerializeField] Canvas loseScreen;

    Entrance entrance;

    public DelayCountdownDisplay delayCountdownDisplay;
    public SpawnsRemainingDisplay spawnsRemainingDisplay;
    public BudgetDisplay budgetDisplay;
    public LiveActorCounter activeCounter;
    public EscapedCounter escapedCounter;
    public SceneHandler sceneHandler;

    float delayTimeLeft;
    int currentBudget;
    int currentActorsSpawned = 0;
    int actorsEscaped = 0;

    //TODO make private
    public bool goalMet = false;
    public bool goalImpossible = false;
    public bool isSpawning = false;
    
    
    void Start()
    {
        entrance = FindObjectOfType<Entrance>();
        if (!entrance) { Debug.LogError("No Entrance Found!"); return; }

        sceneHandler = GetComponentInChildren<SceneHandler>();
        activeCounter = GetComponentInChildren<LiveActorCounter>();
        escapedCounter = GetComponentInChildren<EscapedCounter>();
        budgetDisplay = GetComponentInChildren<BudgetDisplay>();
        delayCountdownDisplay = FindObjectOfType<DelayCountdownDisplay>();
        spawnsRemainingDisplay = FindObjectOfType<SpawnsRemainingDisplay>();

        currentBudget = startingBudget;
        budgetDisplay.UpdateBudgetDisplay(currentBudget);
        spawnsRemainingDisplay.UpdateSpawnRemainingDisplay(totalActorsToSpawn);
       
        delayTimeLeft = startDelayTime;
    }

    IEnumerator SpawnActors()
    {
        isSpawning = true;
        yield return new WaitForSeconds(1);
        
        while(totalActorsToSpawn > 0)
        {
            entrance.SpawnActor(actor);

            currentActorsSpawned++;
            totalActorsToSpawn--;

            spawnsRemainingDisplay.UpdateSpawnRemainingDisplay(totalActorsToSpawn);
            activeCounter.UpdateLiveActorCounter(currentActorsSpawned);
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            StopAllCoroutines();
            sceneHandler.LoadCurrentScene();
        }
        else if (!isSpawning)
        {
            delayTimeLeft -= Time.deltaTime;

            int displayTimeLeft = Mathf.RoundToInt(delayTimeLeft);
            delayCountdownDisplay.UpdateCountdownDisplay(displayTimeLeft);

            if(displayTimeLeft == 0)
            {
                Destroy(delayCountdownDisplay.gameObject);
                StartCoroutine(SpawnActors());
            }
        }
    }

    private void RemoveActor()
    {
        currentActorsSpawned--;
        activeCounter.UpdateLiveActorCounter(currentActorsSpawned);

        if (!goalMet)
        {
            if(!goalImpossible && 
                totalActorsToSpawn + currentActorsSpawned < goalActorsToEscape)
            {
                LoseLevel();
            }
        }
    }
    
    public void ActorEscape()
    {
        RemoveActor();        
        actorsEscaped++;
        escapedCounter.UpdateEscapedCounter(actorsEscaped);
        
        if(actorsEscaped == goalActorsToEscape)
        {
            WinLevel();
        }
    }

    public void ActorDeath()
    {
        RemoveActor();
    }

    private void WinLevel()
    {
        goalMet = true;
        winScreen.gameObject.SetActive(true);
    }

    private void LoseLevel()
    {
        if (goalMet) return;
        goalImpossible = true;
        loseScreen.gameObject.SetActive(true);
        Time.timeScale = 0.25f;
    }

    public void SpendBudget(int toolCost)
    {
        currentBudget -= toolCost;
        budgetDisplay.UpdateBudgetDisplay(currentBudget);
    }

    public int GetCurrentBudget() { return currentBudget; }
    public int GetTotalActorsToSpawn(){ return totalActorsToSpawn; }
    public int GetGoalActorsToEscape() { return goalActorsToEscape; }
    public bool IsGoalMet() { return goalMet; }
    public bool IsGoalImpossible() { return goalImpossible; }
}
