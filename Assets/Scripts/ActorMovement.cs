using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float terminalVelocity = 2f;

    Rigidbody2D rigidBody;
    bool hasFlipped = false;
    public bool isGrounded = true; //todo make private

    Vector2 currentPos;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        currentPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGrounded) Move();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var colliderLayer = collider.gameObject.layer;

        if (colliderLayer == LayerMask.NameToLayer("Ground") ||
            colliderLayer == LayerMask.NameToLayer("Actors"))
        {
            FlipActor();
        }
        else if (colliderLayer == LayerMask.NameToLayer("Hazards"))
        {
            KillActor();
        }
        else return;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Tools")) 
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }

    private void Move()
    {
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
        Destroy(gameObject); //TODO add animation/particles etc
    }
}
