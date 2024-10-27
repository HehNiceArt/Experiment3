using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Andrei Dominic Quirante
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("SPEED")]
    [SerializeField] float aceleration;
    [SerializeField] float decceleration;
    [SerializeField] float maxSpeed;
    Vector2 playerVelocity;

    [Space(10f)]
    [Header("JUMP")]
    [SerializeField] float jumpHeight;
    [Range(1f, 2f)]
    [SerializeField] float jumpPeakLength;
    [Range(0f, 100f)]
    [SerializeField, Tooltip("In Percentage 5")] float jumpMoveModifier;

    [Space(10f)]
    [Header("MASS")]
    [SerializeField] float playerMass;
    [SerializeField] float gravityMultiplier;
    [SerializeField, Tooltip("In Percentage %")] float highGravityModifier;
    [SerializeField] float gravity = 9.81f;

    [Space(10f)]
    [Header("BOOLEANS")]
    [SerializeField] bool isGrounded = true;
    [SerializeField] bool isJumping = false;
    Rigidbody2D rb;
    float currentGravity;
    float move;
    float initialVelocity;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        currentGravity = gravity * gravityMultiplier;
        rb.gravityScale = gravityMultiplier * gravity;
    }
    private void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        JumpMovement();
    }
    private void FixedUpdate()
    {
        HorizontalMovement();
        CapMovementSpeed();
    }
    float Movement()
    {
        if (isJumping)
        {
            float inAirMovement = rb.velocity.x * (jumpMoveModifier * 0.01f);
            return inAirMovement;
        }
        else
        {
            return rb.velocity.x;
        }
    }
    void HorizontalMovement()
    {
        if (move > 0)
        {
            float righInput = Movement() + (maxSpeed * aceleration * Time.fixedDeltaTime);
            rb.velocity = new Vector2(righInput, rb.velocity.y);
            playerVelocity = rb.velocity;
        }
        else if (move < 0)
        {
            float leftInput = Movement() + (-maxSpeed * aceleration * Time.fixedDeltaTime);
            rb.velocity = new Vector2(leftInput, rb.velocity.y);
            playerVelocity = rb.velocity;
        }
        else
        {
            if (rb.velocity.x != 0)
            {
                float deccelerationAmount = decceleration * Time.fixedDeltaTime;
                if (rb.velocity.x > 0)
                {
                    rb.velocity = new Vector2(Mathf.Max(Movement() + deccelerationAmount, 0), rb.velocity.y);
                    playerVelocity = rb.velocity;
                }
                else
                {
                    rb.velocity = new Vector2(Mathf.Min(Movement() - deccelerationAmount, 0), rb.velocity.y);
                    playerVelocity = rb.velocity;
                }
            }
        }
    }
    void CapMovementSpeed()
    {
        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }
        else if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }
    }
    void JumpMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            isGrounded = false;
            rb.velocity = new Vector2(Movement(), Jump());
        }
        else if (rb.velocity.y > 0)
        {
            rb.gravityScale = currentGravity;
        }
        else if (rb.velocity.y < initialVelocity)
        {
            float lowGravity = currentGravity * (highGravityModifier * 0.01f);
            rb.gravityScale = lowGravity;
        }
        if (rb.velocity.y == 0)
        {
            rb.gravityScale = currentGravity;
        }
    }
    float Jump()
    {
        initialVelocity = 2 * jumpHeight / jumpPeakLength;
        playerVelocity = new Vector2(Movement(), initialVelocity);
        return Mathf.Abs(initialVelocity);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
        }
    }
    //TODO
    //Jump Cooldown
    //Jump Input Buffering
    //Coyote Jump
    //Edge Correction
}