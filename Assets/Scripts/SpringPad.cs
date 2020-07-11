using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPad : MonoBehaviour
{
    [SerializeField] float springForce = 10f;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherRigidbody = other.GetComponent<Rigidbody2D>();
        if (!otherRigidbody) return;

        otherRigidbody.velocity = new Vector2(otherRigidbody.velocity.x, -1);
        otherRigidbody.AddForce(new Vector2(0, springForce));
        animator.SetTrigger("spring");
    }
}
