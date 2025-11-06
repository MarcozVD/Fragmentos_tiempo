using UnityEngine;

public class Retroceso : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 knockbackVelocity;
    private float knockbackDuration = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (knockbackDuration > 0)
        {
            controller.Move(knockbackVelocity * Time.deltaTime);
            knockbackDuration -= Time.deltaTime;
        }
    }

    // 🔹 Llamado por el enemigo al golpear al jugador
    public void ApplyKnockback(Vector3 direction, float force, float duration)
    {
        knockbackVelocity = direction.normalized * force;
        knockbackDuration = duration;
    }
}
