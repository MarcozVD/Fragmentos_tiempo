using UnityEngine;
using System.Collections;

public class RespawnPickup : MonoBehaviour
{
    [Header("Tiempo de respawn para pastilla_corta")]
    public float respawnTime = 7f;

    private Collider[] colliders;
    private Renderer[] renderers;

    void Start()
    {
        colliders = GetComponentsInChildren<Collider>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    public void PickUp()
    {
        // Desactivar visual y colisiones
        foreach (var col in colliders)
            col.enabled = false;

        foreach (var rend in renderers)
            rend.enabled = false;

        // ---------------------------------------------
        //  PASTILLA LARGA → guardar respawn
        // ---------------------------------------------
        if (CompareTag("pastilla_larga"))
        {
            RespawnManager.Instance.SetRespawnPoint(transform);
            return; // No respawnea
        }

        // ---------------------------------------------
        //  PASTILLA CORTA → respawn normal
        // ---------------------------------------------
        if (CompareTag("pastilla_corta"))
        {
            StartCoroutine(RespawnAfterDelay());
        }
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnTime);

        foreach (var col in colliders)
            col.enabled = true;

        foreach (var rend in renderers)
            rend.enabled = true;
    }
}
