using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] private float speed;

    Rigidbody2D rb;
    Transform currentPoint;
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentPoint = pointB.transform;
    }

    private void Update()
    {
        Vector2 direction = new Vector2((currentPoint.position.x - transform.position.x), 0).normalized;

        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            currentPoint = currentPoint == pointB.transform ? pointA.transform : pointB.transform;
            
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
