using UnityEngine;
using TMPro;
using System.Collections;

public class WinTrigger : MonoBehaviour
{
    [Header("Panel de Ganaste")]
    public FadeCanvas winPanel;               // Panel con FadeCanvas
    public TextMeshProUGUI timeText;          // Texto donde mostrar tiempo

    [Header("Opciones")]
    public string playerTag = "Player";       // Tag del jugador
    public bool stopPlayer = true;            // Detener jugador al ganar

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag(playerTag))
        {
            triggered = true;

            // Detener jugador si se quiere
            if (stopPlayer)
            {
                Rigidbody rb = other.GetComponent<Rigidbody>();
                if (rb != null) rb.isKinematic = true;

                // Si usas CharacterController
                var controller = other.GetComponent<CharacterController>();
                if (controller != null) controller.enabled = false;
            }

            // Obtener tiempo restante del cronómetro
            Cronometro timer = FindObjectOfType<Cronometro>();
            if (timer != null && timeText != null)
            {
                float t = timer.RemainingTime;
                int minutes = Mathf.FloorToInt(t / 60);
                int seconds = Mathf.FloorToInt(t % 60);
                timeText.text = $"Tiempo: {minutes:00}:{seconds:00}";
            }

            // Activar el fade del panel de ganar
            if (winPanel != null)
                StartCoroutine(ShowWinPanel());
        }
    }

    private IEnumerator ShowWinPanel()
    {
        // Hacer fade in del panel
        yield return winPanel.FadeIn();

        // Pausar el juego
        Time.timeScale = 0f;

        // Liberar cursor para UI
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
