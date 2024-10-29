using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerWallRide : MonoBehaviour
{

    [SerializeField] bool isTouchingWall;
    [SerializeField] float slide;
    [SerializeField] float maxWallStickTime = 1f;
    float wallStickTimer;
    bool wasTouchingWall;
    PlayerController playerController;
    Rigidbody2D rb2D;
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb2D = playerController.rb;
    }
    private void Update()
    {
        if (isTouchingWall)
        {
            if (!wasTouchingWall)
            {

                wallStickTimer = maxWallStickTime;
            }
            wallStickTimer -= Time.deltaTime;
            if (wallStickTimer <= 0)
            {
                rb2D.constraints = RigidbodyConstraints2D.None;
                rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb2D.velocity = new Vector2(rb2D.velocity.x, Mathf.Max(rb2D.velocity.y, -slide));
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb2D.constraints = RigidbodyConstraints2D.None;
                rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb2D.velocity = new Vector2(rb2D.velocity.x, playerController.Jump());
            }
        }
        wasTouchingWall = isTouchingWall;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall") && playerController.isJumping)
        {
            isTouchingWall = true;
            rb2D.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            rb2D.constraints = RigidbodyConstraints2D.None;
            rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            isTouchingWall = false;
        }
    }
}
