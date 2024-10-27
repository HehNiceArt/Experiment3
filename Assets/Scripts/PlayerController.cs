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

    [Space(10f)]
    [Header("MASS")]
    [SerializeField] float playerMass;
    [SerializeField] float gravityMultiplier;
    [SerializeField] bool isGrounded = true, isJumping;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        float currentGravity = rb.gravityScale;
        rb.gravityScale = currentGravity * gravityMultiplier;
    }
    void Update()
    {
        rb.mass = playerMass;
        HorizontalMovement();
        CapMovementSpeed();
    }
    void HorizontalMovement()
    {
        //Left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.RightArrow))
        {
            float righInput = rb.velocity.x + (-maxSpeed * aceleration * Time.deltaTime);
            rb.velocity = new Vector2(righInput, 0);
        }
        //Right
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow))
        {
            float leftInput = rb.velocity.x + (maxSpeed * aceleration * Time.deltaTime);
            rb.velocity = new Vector2(leftInput, 0);
        }
        else
        {
            if (rb.velocity.x != 0)
            {
                float deccerationAmount = decceleration * Time.deltaTime;
                if (rb.velocity.x > 0)
                {
                    rb.velocity = new Vector2(Mathf.Max(rb.velocity.x - deccerationAmount, 0), 0);
                }
                else
                {
                    rb.velocity = new Vector2(Mathf.Min(rb.velocity.x + deccerationAmount, 0), 0);
                }
            }
        }
    }
    void CapMovementSpeed()
    {
        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, 0);
        }
        else if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector2(-maxSpeed, 0);
        }
    }
    void JumpMovement()
    {

    }
}
