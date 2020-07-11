using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayCountdownDisplay : MonoBehaviour
{
    TextMesh text;
    MeshRenderer meshRenderer;


    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<TextMesh>();
        meshRenderer = GetComponent<MeshRenderer>();

        meshRenderer.sortingLayerName = "Doors";
        meshRenderer.sortingOrder = 3;
    }

    public void UpdateCountdownDisplay(int timeRemaining)
    {
        text.text = timeRemaining.ToString();
    }
}
