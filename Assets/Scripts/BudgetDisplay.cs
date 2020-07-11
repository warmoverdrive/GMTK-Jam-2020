using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BudgetDisplay : MonoBehaviour
{
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateBudgetDisplay(int newBudget)
    {
        text.text = "#" + newBudget.ToString();
    }
}
