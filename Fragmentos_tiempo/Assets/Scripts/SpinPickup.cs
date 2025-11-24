using UnityEngine;
using TMPro;

public class SyringePickup : MonoBehaviour
{
    public AudioClip pickupSound;
    public TextMeshProUGUI mensajeTexto;   // Texto UI que se mostrará
    public float tiempoVisible = 1.5f;     // Cuánto dura visible el texto

    private Coroutine rutinaMensaje;

    private void OnTriggerEnter(Collider other)
    {
        newCharacterController player = other.GetComponent<newCharacterController>();

        if (player != null)
        {
            // Activar habilidad del jugador
            player.EnableSpinAbility();

            // Sonido
            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            // Mostrar mensaje
            MostrarMensaje("Desbloqueaste una habilidad! Pulsa E para usar la habilidad");

            // Destruir objeto
            Destroy(gameObject);
        }
    }

    void MostrarMensaje(string texto)
    {
        if (rutinaMensaje != null)
            StopCoroutine(rutinaMensaje);

        rutinaMensaje = StartCoroutine(MostrarMensajeRoutine(texto));
    }

    private System.Collections.IEnumerator MostrarMensajeRoutine(string texto)
    {
        mensajeTexto.text = texto;
        mensajeTexto.enabled = true;

        yield return new WaitForSeconds(tiempoVisible);

        mensajeTexto.enabled = false;
    }
}
