using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public float respawnTime = 10f; // tiempo de respawn
    private Vector3 spawnPosition;  // posición inicial
    private Quaternion spawnRotation;

    private void Start()
    {
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;
    }

    // Este método lo llamas cuando el enemigo muere
    public void KillEnemy()
    {
        gameObject.SetActive(false); // lo ocultamos

        // Respawn en 10s
        Invoke(nameof(Respawn), respawnTime);
    }

    private void Respawn()
    {
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;
        gameObject.SetActive(true);
    }
}
