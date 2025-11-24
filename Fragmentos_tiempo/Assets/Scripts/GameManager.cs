using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private FadeController fade;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Buscar fade actual
        fade = FindObjectOfType<FadeController>();

        // Reiniciar cronómetro primero (antes del Fade)
        Cronometro timer = FindObjectOfType<Cronometro>();
        if (timer != null)
            timer.ResetTimer();

        // Restaurar fade desde negro a juego
        if (fade != null)
            fade.FadeFromBlack();

        // Reiniciar enemigos si existen
        if (EnemyManager.Instance != null)
            EnemyManager.Instance.ResetEnemies();
    }

    public void ReloadGame()
    {
        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        if (fade != null)
        {
            fade.FadeToBlack();
            yield return new WaitForSecondsRealtime(fade.FadeDuration);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        StartCoroutine(MenuRoutine());
    }

    private IEnumerator MenuRoutine()
    {
        if (fade != null)
        {
            fade.FadeToBlack();
            yield return new WaitForSecondsRealtime(fade.FadeDuration);
        }

        SceneManager.LoadScene("MainMenu");
    }
}
