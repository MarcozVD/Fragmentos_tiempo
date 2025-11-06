using UnityEngine;

public class SyringePickup : MonoBehaviour
{
    public AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        newCharacterController player = other.GetComponent<newCharacterController>();

        if (player != null)
        {
            // Activa la habilidad de girar
            player.EnableSpinAbility();

            // Sonido opcional
            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            // Destruye la jeringa
            Destroy(gameObject);
        }
    }
}
