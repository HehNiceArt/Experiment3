using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    [SerializeField] private float stunDuration = 1f;
    public bool isStunned { get; private set; } = false;
    private PlayerController playerController;
    private PlayerDash playerDash;
    private Rigidbody2D rb;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerDash = GetComponent<PlayerDash>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void StunPlayer()
    {
        if (!isStunned && !playerDash.isDashing)
        {
            StartCoroutine(ApplyStun());
        }
    }

    IEnumerator ApplyStun()
    {
        isStunned = true;

        rb.velocity = new Vector2(0, rb.velocity.y);

        yield return new WaitForSeconds(stunDuration);

        isStunned = false;
    }
    private void FixedUpdate()
    {
        if (isStunned)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
}
