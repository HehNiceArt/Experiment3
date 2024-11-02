using UnityEngine;

public class EnemyFloat : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 2.5f;
    [SerializeField] private float floatAmplitude = 0.3f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
