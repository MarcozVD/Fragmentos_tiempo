using UnityEngine;
using TMPro;

public class obtenerTiempo : MonoBehaviour
{
    public Cronometro timerUI;
    public TextMeshProUGUI mensajeTexto;   // Texto UI que se mostrará
    public float tiempoVisible = 1.5f;     // Cuánto dura visible el texto

    private Coroutine rutinaMensaje;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pastilla_larga"))
        {
            timerUI.AddTime(15f);
            MostrarMensaje("Haz recogido un Frasco de Pastillas! +15s");
            RespawnPickup pickup = other.GetComponent<RespawnPickup>();
            if (pickup != null) pickup.PickUp();
        }
        else if (other.CompareTag("pastilla_corta"))
        {
            timerUI.AddTime(7f);
            MostrarMensaje("Haz recogido una Tableta de Pastillas! +7s");
            RespawnPickup pickup = other.GetComponent<RespawnPickup>();
            if (pickup != null) pickup.PickUp();
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
