using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VisionDamageController : MonoBehaviour
{
    public PostProcessVolume volume;

    private Vignette vignette;
    private ChromaticAberration chroma;
    private ColorGrading colorGrade;
    private Grain grain;

    [Header("Velocidad de deterioro")]
    public float deteriorationSpeed = 0.2f;

    [Header("Vibración")]
    public float shakeIntensity = 0.02f;
    public float shakeSpeed = 8f;

    [Header("Intensidades máximas")]
    public float maxVignetteIntensity = 0.55f;
    public float maxSmoothness = 0.9f;
    public float maxChromatic = 0.7f;
    public float maxGrain = 0.6f;
    public float maxColorDesaturation = -80f;

    private float t = 0f;

    void Start()
    {
        volume.profile.TryGetSettings(out vignette);
        volume.profile.TryGetSettings(out chroma);
        volume.profile.TryGetSettings(out colorGrade);
        volume.profile.TryGetSettings(out grain);

        vignette.intensity.value = 0;
        vignette.smoothness.value = 0.2f;
        chroma.intensity.value = 0;
        grain.intensity.value = 0;
        colorGrade.saturation.value = 0;
    }

    void Update()
    {
        t += Time.deltaTime * deteriorationSpeed;

        // Base values over time
        float vigBase = Mathf.Lerp(0f, maxVignetteIntensity, t);
        float smoothBase = Mathf.Lerp(0.2f, maxSmoothness, t);
        float chromaBase = Mathf.Lerp(0f, maxChromatic, t);
        float grainBase = Mathf.Lerp(0f, maxGrain, t);
        float colorBase = Mathf.Lerp(0f, maxColorDesaturation, t);

        // Shake
        float shake = Mathf.Sin(Time.time * shakeSpeed) * shakeIntensity;

        vignette.intensity.value = vigBase + shake;
        vignette.smoothness.value = smoothBase + shake * 0.5f;
        chroma.intensity.value = chromaBase + shake * 0.5f;
        grain.intensity.value = grainBase;
        colorGrade.saturation.value = colorBase;
    }

    // Para resetear desde otro script
    public void ResetVision()
    {
        t = 0;
    }
}
