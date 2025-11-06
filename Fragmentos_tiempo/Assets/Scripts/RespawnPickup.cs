using UnityEngine;
using System.Collections;

public class RespawnPickup : MonoBehaviour
{
    [Header("Tiempo de respawn")]
    public float respawnTime = 7f;

    private Collider[] colliders;
    private Renderer[] renderers;

    void Start()
    {
        // 🔹 Obtiene TODOS los colliders y renderers del objeto y sus hijos
        colliders = GetComponentsInChildren<Collider>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    public void PickUp()
    {
        // 🔹 Desactivar visual y colisiones
        foreach (var col in colliders)
            col.enabled = false;

        foreach (var rend in renderers)
            rend.enabled = false;

        // 🔹 Iniciar el respawn
        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnTime);

        // 🔹 Reactivar visual y colisiones
        foreach (var col in colliders)
            col.enabled = true;

        foreach (var rend in renderers)
            rend.enabled = true;
    }
}
