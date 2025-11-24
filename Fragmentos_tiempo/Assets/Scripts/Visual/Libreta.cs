using UnityEngine;
using System.Collections;

public class Libreta : MonoBehaviour
{
    [Header("Canvas que quieres mostrar")]
    public GameObject targetCanvas;

    [Header("Fade Controller (tu fade)")]
    public FadeController fadeController;

    [Header("Configuración")]
    public float duracionVisible = 20f;

    private bool activado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || activado) return;
        activado = true;

        StartCoroutine(MostrarCanvasRoutine());
    }

    private IEnumerator MostrarCanvasRoutine()
    {
        // Pausar juego
        Time.timeScale = 0f;

        // Activamos canvas
        targetCanvas.SetActive(true);

        // Fade a negro (tu método real)
        if (fadeController != null)
            fadeController.FadeToBlack();

        // Esperar en tiempo real 15s
        yield return new WaitForSecondsRealtime(duracionVisible);

        // Fade desde negro
        if (fadeController != null)
            fadeController.FadeFromBlack();

        // Esperar a que termine el fade (usa tu FadeDuration)
        yield return new WaitForSecondsRealtime(fadeController.FadeDuration);

        // Ocultar canvas
        targetCanvas.SetActive(false);

        // Reanudar tiempo
        Time.timeScale = 1f;

        // Destruir la libreta
        Destroy(gameObject);
    }
}
