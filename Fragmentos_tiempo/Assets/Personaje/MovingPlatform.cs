using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movimiento")]
    public Transform puntoA;
    public Transform puntoB;
    public float speed = 2f;

    private Vector3 destinoActual;
    private Vector3 lastPosition;
    public Vector3 PlatformVelocity { get; private set; }

    void Start()
    {
        if (puntoA == null || puntoB == null)
        {
            Debug.LogError("Debes asignar los puntos A y B en el inspector.");
            enabled = false;
            return;
        }

        destinoActual = puntoB.position;
        lastPosition = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destinoActual, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, destinoActual) < 0.1f)
        {
            destinoActual = destinoActual == puntoA.position ? puntoB.position : puntoA.position;
        }

        // Calcular la velocidad de la plataforma
        PlatformVelocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }
}
