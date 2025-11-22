using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemiHit : MonoBehaviour
{
    [Header("Flash por daño")]
    private HitFlash hitFlash;

    
    [Header("Daño por contacto")]
    public float damageTime = 5f;          // segundos que se restan
    public float damageInterval = 1f;      // tiempo entre cada daño
    public float knockbackForce = 5f;      // fuerza de retroceso

    private Cronometro cronometro;
    private float nextDamageTime = 0f;

    void Start()
    {
        hitFlash = FindObjectOfType<HitFlash>();
        if (hitFlash == null)
            Debug.LogError("❌ No se encontró HitFlash en la escena.");

        cronometro = FindObjectOfType<Cronometro>();
        if (cronometro == null)
            Debug.LogError("❌ No se encontró el Cronometro en la escena.");

        // 🔹 Asegurar collider tipo trigger
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;

        // 🔹 No necesitamos Rigidbody para triggers
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            Destroy(rb);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Time.time >= nextDamageTime)
        {
            cronometro.AddTime(-damageTime);
            hitFlash.Flash();

            Debug.Log($"💥 Golpe enemigo - {damageTime}s menos. Tiempo actual: {cronometro.RemainingTime}");

            // 🌀 Retroceso
            Retroceso retroceso = other.GetComponent<Retroceso>();
            if (retroceso != null)
            {
                Vector3 direction = (other.transform.position - transform.position).normalized;
                retroceso.ApplyKnockback(direction, knockbackForce, 0.2f);
            }

            nextDamageTime = Time.time + damageInterval;
        }
    }
}
