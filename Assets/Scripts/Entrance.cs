using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public void SpawnActor(ActorMovement actor)
    {
        Instantiate(actor, transform.position, transform.rotation);
    }
}
