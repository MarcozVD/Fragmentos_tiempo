using UnityEngine;

[RequireComponent(typeof(Collider))]
public class JumpKill : MonoBehaviour
{
    [Header("Configuración")]
    public float bounceForce = 8f; // Fuerza con la que el jugador rebota al saltar sobre el enemigo
    public GameObject deathEffect; // Efecto visual opcional (explosión, humo, etc.)

    private void Start()
    {
        // Aseguramos que el collider sea un trigger
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var controller = other.GetComponent<newCharacterController>();
        if (controller == null) return;

        // Si está girando (ataque tipo Crash)
        if (controller.IsSpinning())
        {
            KillEnemy();
            return;
        }

        // Obtenemos posición relativa del jugador respecto al enemigo
        Vector3 direction = (other.transform.position - transform.position).normalized;

        // Si el jugador viene desde arriba
        if (direction.y > 0.5f)
        {
            // Rebote del jugador hacia arriba
            controller.ApplyExternalForce(Vector3.up * bounceForce);

            Debug.Log("Enemy JumpKilled!");

            // Matar enemigo
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        // Efecto visual (si tiene)
        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        // Destruir enemigo
        Destroy(gameObject);
    }
}
