using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;

    [Header("Distancias")]
    public float detectionRange = 10f;   // distancia para empezar a perseguir
    public float stopRange = 15f;        // distancia para dejar de perseguir

    [Header("Movimiento")]
    public float speed = 3f;

    private bool isChasing = false;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // 🔹 Encender persecución
        if (!isChasing && distance <= detectionRange)
        {
            isChasing = true;
        }

        // 🔹 Apagar persecución
        if (isChasing && distance >= stopRange)
        {
            isChasing = false;
        }

        // 🔹 Si está en modo persecución → moverse hacia el jugador
        if (isChasing)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Opcional: lo hace mirar hacia el jugador
            transform.LookAt(player);
        }
    }
}
