using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolSpawner : MonoBehaviour
{
    [SerializeField] LayerMask ignoreMe;
    Tool tool;
    GameObject toolParent;

    //will be used for win/lose states for spawning tools
    LevelController levelController; 

    //Tool Parent is an organization structure for the hierarchy to reduct clutter
    const string TOOL_PARENT_NAME = "Tools";

    void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        CreateToolParent();
    }

    private void CreateToolParent()
    {
        toolParent = GameObject.Find(TOOL_PARENT_NAME);
        if (!toolParent) toolParent = new GameObject(TOOL_PARENT_NAME);
    }

    public void SetSelectedTool(Tool selectedTool) { tool = selectedTool; }

    private void OnMouseDown()
    {
        if (!tool) return;
        if (levelController.goalImpossible || levelController.goalMet) return;
        if (IsInputBlocked()) return;

        else AttemptToPlaceToolAt(GetGridClicked());
    }

    private bool IsInputBlocked()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return true;

        RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), 
            Vector2.zero, 1000f, ~ignoreMe);

        if (hitInfo.collider == null) return false;
        else return true;
    }

    private void AttemptToPlaceToolAt(Vector2 gridPos)
    {
        int currentBudget = levelController.GetCurrentBudget();
        int toolCost = tool.GetToolCost();

        if (currentBudget >= toolCost)
        {
            SpawnTool(gridPos);
            levelController.SpendBudget(toolCost);
        }
        else return; //TODO warn player out of money
    }

    private Vector2 GetGridClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        return SnapToGrid(worldPos);
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);

        return new Vector2(newX, newY);
    }

    private void SpawnTool(Vector2 roundedPos)
    {
        Tool newTool = Instantiate(tool, roundedPos, Quaternion.identity);

        newTool.transform.parent = toolParent.transform;
    }
}
