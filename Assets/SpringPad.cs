using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPad : MonoBehaviour
{
    [SerializeField] float springForce = 10f;


    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherRigidbody = other.GetComponent<Rigidbody2D>();
        if (!otherRigidbody) return;

        otherRigidbody.AddForce(new Vector2(0, springForce));
    }
}
