using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Reflection;

public class ToolButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Tool toolPrefab;

    Image image;
    Color selectedColor = Color.white;
    Color deselectedColor = new Color(1f, 1f, 1f, 0.35f);

    ToolSpawner toolSpawner;
    ToolButton[] buttons;

    void Start()
    {
        toolSpawner = FindObjectOfType<ToolSpawner>();
        
        SetCostLabel();
        image = GetComponent<Image>();
        image.color = deselectedColor;
        buttons = FindObjectsOfType<ToolButton>();
    }

    private void SetCostLabel()
    {
        var text = GetComponentInChildren<TextMeshProUGUI>();
        if (!text) Debug.LogWarning("text object for " + name + " not found.");

        else { text.text = "#"+ toolPrefab.GetToolCost().ToString(); }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        foreach (ToolButton button in buttons)
        {
            button.GetComponent<Image>().color = deselectedColor;
        }

        image.color = selectedColor;

        toolSpawner.SetSelectedTool(toolPrefab);
    }
}
