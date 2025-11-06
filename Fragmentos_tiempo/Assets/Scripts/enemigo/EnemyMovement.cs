using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum MovementType { Horizontal, Vertical } // Tipo de movimiento
    public MovementType movementType = MovementType.Horizontal;

    [Header("Movimiento")]
    public float speed = 2f;           // Velocidad de movimiento
    public float distance = 3f;        // Distancia total del recorrido

    private Vector3 startPosition;     // Posición inicial
    private int direction = 1;         // 1 o -1 según la dirección actual

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (movementType == MovementType.Horizontal)
        {
            transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
            if (Mathf.Abs(transform.position.x - startPosition.x) >= distance)
            {
                direction *= -1; // Cambia de dirección
            }
        }
        else if (movementType == MovementType.Vertical)
        {
            transform.Translate(Vector3.up * direction * speed * Time.deltaTime);
            if (Mathf.Abs(transform.position.y - startPosition.y) >= distance)
            {
                direction *= -1; // Cambia de dirección
            }
        }
    }
}
