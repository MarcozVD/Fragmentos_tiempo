using UnityEngine;

public class SyringePickup : MonoBehaviour
{
    public AudioClip pickupSound;
    public FadeText unlockAbilityText;

    private void OnTriggerEnter(Collider other)
    {
        newCharacterController player = other.GetComponent<newCharacterController>();

        if (player != null)
        {
            player.EnableSpinAbility();

            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            if (unlockAbilityText != null)
                unlockAbilityText.ShowMessage();

            Destroy(gameObject);
        }
    }
}
