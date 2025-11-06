using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [Header("Tiempo inicial (segundos)")]
    public float startTime = 120f; // 2 minutos

    [Header("Referencia UI")]
    public TextMeshProUGUI timerText;

    private float currentTime;
    private bool isRunning = true;

    void Start()
    {
        currentTime = startTime;
    }

    void Update()
    {
        if (!isRunning) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            isRunning = false;
            // Aquí puedes hacer que termine el juego o mostrar "Tiempo agotado"
        }

        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    // 🔹 Método público para añadir tiempo desde otro script
    public void AddTime(float amount)
    {
        currentTime += amount;
    }
}
