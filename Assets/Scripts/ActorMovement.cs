using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ActorMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [Header("Death Parameters")]
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] float deathKickMin = 1, deathKickMax = 20;
    [SerializeField] float deathAngVel = 20;
    [Header("audio")]
    [Range(0,1)][SerializeField] float audioVolume = 0.5f;
    [SerializeField] AudioClip[] deathSFX;
    [SerializeField] AudioClip spawnSFX;
    [SerializeField] AudioClip escapeSFX;


    //Cached Reference
    Rigidbody2D rigidBody;
    Animator animator;

    //States
    bool hasFlipped = false;
    bool isGrounded = true;
    bool isDead = false;
    bool isFlying = false;
    bool isFalling = false;
    bool hasExited = false;

    void Start()
    {
        AudioSource.PlayClipAtPoint(spawnSFX, Camera.main.transform.position, audioVolume);
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasExited)
        {
            Move();
            CheckAirborneState();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var colliderLayer = collider.gameObject.layer;

        if (colliderLayer == LayerMask.NameToLayer("Ground") ||
            colliderLayer == LayerMask.NameToLayer("Actors"))
        {
            FlipActor();
        }
        else if (colliderLayer == LayerMask.NameToLayer("Hazards") && !isDead)
        {
            KillActor();
        }
        else return;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Tools"))
        {
            isGrounded = true;
            animator.SetBool("isGrounded", isGrounded);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Tools"))
        {
            isGrounded = false;
            animator.SetBool("isGrounded", isGrounded);
        }
    }

    private void Move()
    {
        if (isDead || !isGrounded) return;
        Vector2 force = new Vector2(movementSpeed * Time.deltaTime, rigidBody.velocity.y);

        rigidBody.velocity = force;
    }

    private void CheckAirborneState()
    {
        if (!isGrounded)
        {
            switch (Mathf.Sign(rigidBody.velocity.y))
            {
                case 1:
                    isFlying = true;
                    isFalling = false;
                    break;
                case -1:
                    isFlying = false;
                    isFalling = true;
                    break;
            }
        }
        else
        {
            isFlying = false;
            isFalling = false;
        }

        SetAnimatorAirborneBools();
    }

    private void SetAnimatorAirborneBools()
    {
        animator.SetBool("isFlying", isFlying);
        animator.SetBool("isFalling", isFalling);
    }

    private void FlipActor()
    {
        hasFlipped = !hasFlipped;
        movementSpeed = -movementSpeed;
        transform.localScale = new Vector2(Mathf.Sign(movementSpeed), transform.localScale.y);
    }

    public void TriggerActorExit()
    {
        AudioSource.PlayClipAtPoint(escapeSFX, Camera.main.transform.position, audioVolume);
        hasExited = true;
        animator.SetBool("isExit", true);
        DisablePhysics();
    }

    private void DisablePhysics()
    {
        rigidBody.velocity = new Vector2(0, 0);
    }

    public void ExitRemoveActor()
    {
        Destroy(gameObject);
    }

    private void KillActor()
    {
        AudioSource.PlayClipAtPoint(deathSFX[Random.Range(0, deathSFX.Length)], Camera.main.transform.position, audioVolume + 0.5f);

        FindObjectOfType<LevelController>().ActorDeath();
        isDead = true;

        animator.SetBool("isDead", isDead);

        DeathKick();
        var particleSystem = Instantiate(deathParticles, transform.position, transform.rotation);
        Destroy(particleSystem, 500);
    }

    private void DeathKick()
    {
        rigidBody.freezeRotation = false;
        rigidBody.sharedMaterial.friction = 1;

        rigidBody.velocity = new Vector2(
            Random.Range(deathKickMin,deathKickMax) / 4, 
            Random.Range(deathKickMin, deathKickMax));

        rigidBody.angularVelocity = deathAngVel;
    }
}
