using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    [Header("Lugar de respawn")]
    public Transform respawnPoint; // referencia al lugar donde reaparecerá el jugador

    private void OnTriggerEnter(Collider other)
    {
        // Detecta si el objeto que entra tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();

            if (controller != null)
            {
                // Desactiva temporalmente el CharacterController
                controller.enabled = false;

                // Teletransporta al jugador al punto de respawn
                other.transform.position = respawnPoint.position;

                // Reactiva el CharacterController
                controller.enabled = true;
            }
        }
    }
}

