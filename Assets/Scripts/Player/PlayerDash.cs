using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] float dashForce = 24f;
    [SerializeField] float dashTime = 0.2f;
    [SerializeField] float dashCooldown;
    [SerializeField] bool canDash = true;
    public bool isDashing;

    PlayerController playerController;
    Rigidbody2D rb2D;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb2D = playerController.rb;
    }
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb2D.gravityScale;
        rb2D.gravityScale = 0;
        rb2D.velocity = new Vector2(rb2D.velocity.x, dashForce);
        yield return new WaitForSeconds(dashTime);
        rb2D.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

}
