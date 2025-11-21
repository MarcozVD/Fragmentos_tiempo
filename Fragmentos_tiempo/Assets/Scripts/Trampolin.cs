using UnityEngine;
using System.Collections;

public class Trampolin : MonoBehaviour
{
    public Transform puntoDestino; // A dónde saltará el jugador
    public float duracionSalto = 0.5f; // Tiempo del salto
    public float alturaSalto = 3f; // Curvatura del arco

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SaltoControlado(other.gameObject));
        }
    }

    private IEnumerator SaltoControlado(GameObject player)
    {
        CharacterController controller = player.GetComponent<CharacterController>();

        if (controller == null)
        {
            Debug.LogError("El Player no tiene CharacterController.");
            yield break;
        }

        Vector3 inicio = player.transform.position;
        Vector3 destino = puntoDestino.position;

        float tiempo = 0;

        while (tiempo < 1f)
        {
            tiempo += Time.deltaTime / duracionSalto;

            // Movimiento horizontal suave
            Vector3 pos = Vector3.Lerp(inicio, destino, tiempo);

            // Agregar altura en forma de arco
            pos.y += Mathf.Sin(tiempo * Mathf.PI) * alturaSalto;

            controller.enabled = false; // Para evitar problemas de CC
            player.transform.position = pos;
            controller.enabled = true;

            yield return null;
        }

        // Colocar exactamente al destino al final
        controller.enabled = false;
        player.transform.position = destino;
        controller.enabled = true;
    }
}
