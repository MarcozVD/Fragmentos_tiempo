using UnityEngine;

public class EnemiHit : MonoBehaviour
{
    [Header("Daño por contacto")]
    public float damageTime = 5f;          // segundos que se restan al tocar
    public float damageInterval = 1f;      // tiempo entre cada daño (en segundos)

    private Cronometro cronometro;
    private bool playerInContact = false;
    private float nextDamageTime = 0f;

    void Start()
    {
        cronometro = FindObjectOfType<Cronometro>();

        if (cronometro == null)
        {
            Debug.LogError("❌ No se encontró el Cronometro en la escena.");
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("⚠️ El enemigo no tiene Rigidbody. Se agregará uno automáticamente.");
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // ✅ Hacemos el Rigidbody cinemático para evitar bugs con el movimiento manual
        rb.isKinematic = true;
        rb.useGravity = false;
    }


    // 🔹 Detecta cuando el jugador entra en contacto físico con el enemigo
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInContact = true;
            nextDamageTime = Time.time; // reinicia el temporizador de daño
        }
    }

    // 🔹 Mientras el jugador esté en contacto con el enemigo
    private void OnCollisionStay(Collision collision)
    {
        if (playerInContact && collision.gameObject.CompareTag("Player") && Time.time >= nextDamageTime)
        {
            cronometro.AddTime(-damageTime);  // ❌ resta tiempo del cronómetro
            Debug.Log($" Golpe enemigo - {damageTime}s menos. Tiempo actual: {cronometro.RemainingTime}");

            nextDamageTime = Time.time + damageInterval;
        }
    }

    // 🔹 Cuando el jugador deja de tocar al enemigo
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInContact = false;
        }
    }
}
