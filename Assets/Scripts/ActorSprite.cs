using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorSprite : MonoBehaviour
{
    private void TriggerActorRemoval()
    {
        GetComponentInParent<ActorMovement>().ExitRemoveActor();
    }
}
