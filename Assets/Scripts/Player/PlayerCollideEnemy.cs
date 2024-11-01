using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollideEnemy : MonoBehaviour
{
    //Add freeze frame to have more impact
    //particles too and sfx
    [Range(0, 60)]
    [SerializeField] int freezeFrames = 3;
    [SerializeField] bool maintainVelocity = false;
    PlayerDash playerDash;
    PlayerStun playerStun;
    Rigidbody2D rb2D;
    void Start()
    {
        playerDash = GetComponent<PlayerDash>();
        playerStun = GetComponent<PlayerStun>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (playerDash.isDashing)
            {
                StartCoroutine(FreezeFrame());
                Destroy(other.gameObject);
            }
            else
            {
                playerStun.StunPlayer();
            }
        }
    }
    IEnumerator FreezeFrame()
    {
        Time.timeScale = 0f;
        if (maintainVelocity)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y);
        }
        else
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
        }
        yield return new WaitForSecondsRealtime(freezeFrames / 60f);
        Time.timeScale = 1f;
    }
}
