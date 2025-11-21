using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();

            if (controller != null)
            {
                // Desactivar temporalmente el CharacterController
                controller.enabled = false;

                // Obtener el punto de respawn correcto desde RespawnManager
                Transform respawnPoint = RespawnManager.Instance.GetRespawnPoint();

                // Teletransportar al jugador
                other.transform.position = respawnPoint.position;

                // Reactivar CharacterController
                controller.enabled = true;
            }
        }
    }
}
