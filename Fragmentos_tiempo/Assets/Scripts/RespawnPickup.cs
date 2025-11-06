using UnityEngine;
using System.Collections;

public class RespawnPickup : MonoBehaviour
{
    [Header("Tiempo de respawn")]
    public float respawnTime = 7f;

    private Collider col;
    private Renderer rend;

    void Start()
    {
        col = GetComponent<Collider>();
        rend = GetComponent<Renderer>();
    }

    public void PickUp()
    {
        // Desactivar visual y colisión
        col.enabled = false;
        rend.enabled = false;

        // Iniciar el respawn
        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnTime);

        // Reactivar visual y colisión
        col.enabled = true;
        rend.enabled = true;
    }
}
