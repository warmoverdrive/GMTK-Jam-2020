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

    //Cached Reference
    Rigidbody2D rigidBody;

    //States
    bool hasFlipped = false;
    bool isGrounded = true;
    bool isDead = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
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
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Tools"))
        {
            isGrounded = false;
        }
    }

    private void Move()
    {
        if (isDead || !isGrounded) return;
        Vector2 force = new Vector2(movementSpeed * Time.deltaTime, rigidBody.velocity.y);

        rigidBody.velocity = force;
    }

    private void FlipActor()
    {
        hasFlipped = !hasFlipped;
        movementSpeed = -movementSpeed;
        transform.localScale = new Vector2(Mathf.Sign(movementSpeed), transform.localScale.y);
    }

    private void KillActor()
    {
        FindObjectOfType<LevelController>().ActorDeath();
        isDead = true;
        DeathKick();
        var particleSystem = Instantiate(deathParticles, transform.position, transform.rotation);
        Destroy(particleSystem, 500);
        //Destroy(gameObject); //TODO add animation/particles etc
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
