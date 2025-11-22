using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform puntoA;
    public Transform puntoB;
    public float speed = 2f;

    private Vector3 destinoActual;
    private Vector3 lastPosition;

    public Vector3 DeltaMovement { get; private set; }

    void Start()
    {
        destinoActual = puntoB.position;
        lastPosition = transform.position;
    }

    void Update()
    {
        // Mover plataforma
        transform.position = Vector3.MoveTowards(transform.position, destinoActual, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, destinoActual) < 0.1f)
        {
            destinoActual = destinoActual == puntoA.position ? puntoB.position : puntoA.position;
        }

        // → ESTE ES EL MOVIMIENTO REAL DE LA PLATAFORMA (NECESARIO)
        DeltaMovement = transform.position - lastPosition;
        lastPosition = transform.position;
    }
}
