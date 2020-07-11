using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnsRemainingDisplay : MonoBehaviour
{
    TextMesh text;
    MeshRenderer meshRenderer;

    void Awake()
    {
        text = GetComponent<TextMesh>();
        meshRenderer = GetComponent<MeshRenderer>();

        meshRenderer.sortingLayerName = "Doors";
        meshRenderer.sortingOrder = 3;
    }

    public void UpdateSpawnRemainingDisplay(int spawnsRemaining)
    {
        text.text = spawnsRemaining.ToString();
        if(spawnsRemaining == 0) { Destroy(gameObject, 1); }
    }
}
