using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private FadeController fade;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            //  Impedir que queden callbacks de objetos destruidos
            SceneManager.sceneLoaded -= OnSceneLoaded;

            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (transform.parent != null)
        {
            Debug.LogWarning("GameManager tenía un padre. Lo saco manualmente.");
            transform.SetParent(null);
        }

        DontDestroyOnLoad(gameObject);

        //  Asegurar que no se duplique la suscripción
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        fade = FindObjectOfType<FadeController>();

        // Verificar EventSystem
        if (FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            Debug.LogWarning("No hay EventSystem en la escena.");
        }

        // Si es MAIN MENU → resetear inputs/UI
        if (scene.name == "MainMenu")
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (fade != null)
                fade.FadeFromBlack();

            return;
        }

        // Si hay cronómetro, reiniciarlo
        Cronometro timer = FindObjectOfType<Cronometro>();
        if (timer != null)
            timer.ResetTimer();

        // Hacer fade in si existe
        if (fade != null)
            fade.FadeFromBlack();

        // Reiniciar enemigos
        if (EnemyManager.Instance != null)
            EnemyManager.Instance.ResetEnemies();
    }

    // ==========================
    // REINICIAR ESCENA
    // ==========================
    public void ReloadGame()
    {
        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        fade = FindObjectOfType<FadeController>();

        if (fade != null)
        {
            fade.FadeToBlack();
            yield return new WaitForSecondsRealtime(fade.FadeDuration);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ==========================
    // IR AL MAIN MENU
    // ==========================
    public void GoToMainMenu()
    {
        StartCoroutine(MenuRoutine());
    }

    private IEnumerator MenuRoutine()
    {
        fade = FindObjectOfType<FadeController>();

        if (fade != null)
        {
            fade.FadeToBlack();
            yield return new WaitForSecondsRealtime(fade.FadeDuration);
        }

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("MainMenu");
    }
}
