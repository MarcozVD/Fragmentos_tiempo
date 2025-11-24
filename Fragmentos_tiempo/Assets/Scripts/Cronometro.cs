using UnityEngine;
using TMPro;
using System.Collections;

public class Cronometro : MonoBehaviour
{
    [Header("Tiempo inicial (segundos)")]
    public float startTime = 120f;

    [Header("UI")]
    public TextMeshProUGUI timerText;

    [Header("Pantalla de Perdiste")]
    public FadeCanvas fadePerdiste;

    private float currentTime;
    private bool isRunning = false;
    private bool gameOverTriggered = false;

    public float RemainingTime => currentTime;

    void Update()
    {
        if (!isRunning) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0 && !gameOverTriggered)
        {
            currentTime = 0;
            isRunning = false;
            gameOverTriggered = true;
            StartCoroutine(GameOverRoutine());
        }

        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        if (timerText == null)
            timerText = FindObjectOfType<TextMeshProUGUI>();

        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    public void AddTime(float amount)
    {
        currentTime += amount;
    }

    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(3f);

        if (timerText != null)
            timerText.enabled = false;

        if (fadePerdiste != null)
            yield return fadePerdiste.FadeIn();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    public void ResetTimer()
    {
        currentTime = startTime;
        isRunning = true;
        gameOverTriggered = false;

        // 🔹 Buscar texto si es null o destruido
        if (timerText == null)
            timerText = FindObjectOfType<TextMeshProUGUI>();

        if (timerText != null)
        {
            timerText.gameObject.SetActive(true);
            timerText.enabled = true;
            UpdateTimerDisplay();
        }

        // Cerrar pantalla de "Perdiste"
        if (fadePerdiste != null)
        {
            fadePerdiste.canvasGroup.alpha = 0f;
            fadePerdiste.canvasGroup.interactable = false;
            fadePerdiste.canvasGroup.blocksRaycasts = false;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;
    }
}
