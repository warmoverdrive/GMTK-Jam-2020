using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    Animator animator;
    [Range(0, 1)] [SerializeField] float audioVolume = 0.5f;
    [SerializeField] AudioClip bounceSFX;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(bounceSFX, Camera.main.transform.position, audioVolume);
        animator.SetTrigger("bounce");
    }
}
