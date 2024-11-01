using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Andrei Dominic Quirante
public class PlayerWallRide : MonoBehaviour
{
    [SerializeField] bool isTouchingWall;
    [SerializeField] float slide = 1f;
    [SerializeField] float maxWallStickTime = 1.2f;
    float wallStickTimer;
    [SerializeField] bool wasTouchingWall;
    [SerializeField] bool hasStuckToWall;
    [SerializeField] bool wasOnGround;
    [SerializeField] bool onAir;
    PlayerController playerController;
    Rigidbody2D rb2D;
    [SerializeField] Image sliderImage;
    [SerializeField] Material sliderMaterial;
    [SerializeField] float maxStamina = 100f;
    [SerializeField] float staminaDrainRate = 20f;
    [SerializeField] float staminaRegenRate = 10f;
    private float currentStamina;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb2D = playerController.rb;
        currentStamina = maxStamina;

        sliderImage.material = sliderMaterial;
    }
    private void Update()
    {
        wasOnGround = playerController.isOnGround;
        if (playerController.isOnGround)
        {
            isTouchingWall = false;
            hasStuckToWall = false;
            onAir = false;
        }
        if (isTouchingWall)
        {
            if (!wasTouchingWall)
            {
                wallStickTimer = maxWallStickTime;
            }
            wallStickTimer -= Time.deltaTime;
            if (hasStuckToWall && onAir == false)
            {
                currentStamina -= staminaDrainRate * Time.deltaTime;
                currentStamina = Mathf.Max(0f, currentStamina);
                sliderMaterial.SetFloat("_SliderValue", currentStamina / maxStamina);

                if (wallStickTimer <= 0 || currentStamina <= 0)
                {
                    ReleaseFromWall();
                    rb2D.velocity = new Vector2(rb2D.velocity.x, Mathf.Max(rb2D.velocity.y, -slide));
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ReleaseFromWall();
                    onAir = true;
                    rb2D.velocity = new Vector2(rb2D.velocity.x, playerController.Jump());
                }
            }
        }
        else if (playerController.isOnGround)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maxStamina);
            sliderMaterial.SetFloat("_SliderValue", currentStamina / maxStamina);
        }
        wasTouchingWall = isTouchingWall;
    }
    void ReleaseFromWall()
    {
        rb2D.constraints = RigidbodyConstraints2D.None;
        rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall") && playerController.isJumping && !hasStuckToWall && !onAir)
        {
            isTouchingWall = true;
            hasStuckToWall = true;
            rb2D.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            rb2D.constraints = RigidbodyConstraints2D.None;
            rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            onAir = true;
            isTouchingWall = false;
            hasStuckToWall = false;
        }
    }
}
