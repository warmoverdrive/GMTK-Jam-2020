using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    [SerializeField] int toolCost = 1;

    public int GetToolCost() { return toolCost; }
}
