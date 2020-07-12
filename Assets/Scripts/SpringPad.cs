using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpringPad : MonoBehaviour
{
    [SerializeField] float springForce = 10f;
    [Range(0, 1)] [SerializeField] float audioVolume = 0.5f;
    [SerializeField] AudioClip springSFX;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherRigidbody = other.GetComponent<Rigidbody2D>();
        if (!otherRigidbody) return;

        AudioSource.PlayClipAtPoint(springSFX, Camera.main.transform.position, audioVolume);

        otherRigidbody.velocity = new Vector2(otherRigidbody.velocity.x, Mathf.Epsilon);
        otherRigidbody.AddForce(new Vector2(0, springForce));
        animator.SetTrigger("spring");
    }
}
