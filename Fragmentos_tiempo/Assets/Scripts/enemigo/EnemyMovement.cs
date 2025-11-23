using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Ejes de movimiento")]
    public bool moveX = true;
    public bool startRight = true;      // Dirección inicial en X

    public bool moveY = false;
    public bool startUp = true;         // Dirección inicial en Y

    public bool moveZ = false;
    public bool startForward = true;    // Dirección inicial en Z

    [Header("Distancias por eje")]
    public float distanceX = 3f;
    public float distanceY = 3f;
    public float distanceZ = 3f;

    [Header("Movimiento")]
    public float speed = 2f;

    private Vector3 startPos;
    private Vector3 direction;

    void Start()
    {
        startPos = transform.position;

        direction = new Vector3(
            moveX ? (startRight ? 1 : -1) : 0,
            moveY ? (startUp ? 1 : -1) : 0,
            moveZ ? (startForward ? 1 : -1) : 0
        );
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        Vector3 offset = transform.position - startPos;

        // Eje X
        if (moveX && Mathf.Abs(offset.x) >= distanceX)
        {
            direction.x *= -1;
            startPos.x = transform.position.x; // Reiniciar origen
        }

        // Eje Y
        if (moveY && Mathf.Abs(offset.y) >= distanceY)
        {
            direction.y *= -1;
            startPos.y = transform.position.y;
        }

        // Eje Z
        if (moveZ && Mathf.Abs(offset.z) >= distanceZ)
        {
            direction.z *= -1;
            startPos.z = transform.position.z;
        }
    }
}
