using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VisionByTime : MonoBehaviour
{   
    public FadeController fadeController;

    [Header("Referencias")]
    public Cronometro cronometro;
    public PostProcessVolume volume;

    private Vignette vignette;
    private ChromaticAberration chromatic;
    private Grain grain;
    private ColorGrading colorGrading;

    [Header("Valores máximos al llegar a 0 segundos")]
    public float MaxVignette = 0.55f;
    public float MaxSmoothness = 0.85f;
    public float MaxChromatic = 0.6f;
    public float MaxGrain = 0.6f;
    public float MinSaturation = -60f;

    [Header("Vibración")]
    public float ShakeIntensity = 0.03f;
    public float ShakeSpeed = 10f;

    private float startTime;
    private bool blackoutTriggered = false;

    void Start()
    {
        startTime = cronometro.startTime;

        // --- Obtener los efectos desde el perfil ---
        vignette = volume.profile.GetSetting<Vignette>();
        chromatic = volume.profile.GetSetting<ChromaticAberration>();
        grain = volume.profile.GetSetting<Grain>();
        colorGrading = volume.profile.GetSetting<ColorGrading>();
    }

    void Update()
    {
        float t = 1 - (cronometro.RemainingTime / startTime);
        t = Mathf.Clamp01(t);

        // --- EFECTOS PROGRESIVOS ---
        if (vignette != null)
        {
            vignette.intensity.value = Mathf.Lerp(0f, MaxVignette, t);
            vignette.smoothness.value = Mathf.Lerp(0f, MaxSmoothness, t);

            // Vibración del borde
            vignette.intensity.value += Mathf.Sin(Time.time * ShakeSpeed) * ShakeIntensity;
        }

        if (chromatic != null)
            chromatic.intensity.value = Mathf.Lerp(0f, MaxChromatic, t);

        if (grain != null)
            grain.intensity.value = Mathf.Lerp(0f, MaxGrain, t);

        if (colorGrading != null)
            colorGrading.saturation.value = Mathf.Lerp(0f, MinSaturation, t);

        // --- OSCURO TOTAL ---
        if (cronometro.RemainingTime <= 0 && !blackoutTriggered)
        {
            blackoutTriggered = true;
            ApplyBlackout();
        }
    }

    void ApplyBlackout()
    {
        if (vignette != null)
        {
            vignette.intensity.value = 1f;
            vignette.smoothness.value = 1f;
        }

        if (chromatic != null)
            chromatic.intensity.value = 1f;

        if (grain != null)
            grain.intensity.value = 1f;

        if (colorGrading != null)
            colorGrading.saturation.value = -100f;
            
        fadeController.FadeToBlack();


        Debug.Log("Pantalla completamente oscura");
    }
}
