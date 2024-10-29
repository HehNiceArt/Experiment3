using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerWallRide : MonoBehaviour
{

    [SerializeField] bool isTouchingWall;
    PlayerController playerController;
    Rigidbody2D rb2D;
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb2D = playerController.rb;
    }
    private void Update()
    {
        if (isTouchingWall && Input.GetKeyDown(KeyCode.Space) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)))
        {
            rb2D.constraints = RigidbodyConstraints2D.None;
            rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb2D.velocity = new Vector2(rb2D.velocity.x, playerController.Jump());
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
            rb2D.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    }
}
