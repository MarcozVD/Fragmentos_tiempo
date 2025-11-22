using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignetteController : MonoBehaviour
{
    public PostProcessVolume volume;
    private Vignette vignette;

    public Cronometro cronometro;

    void Start()
    {
        volume.profile.TryGetSettings(out vignette); // ← AQUÍ SE CONSIGUE EL VIGNETTE

        if (vignette == null)
            Debug.LogError("No existe Vignette en el Volume Profile.");
    }

    void Update()
    {
        if (vignette == null) return;

        float t = 1f - (cronometro.RemainingTime / cronometro.startTime);
        vignette.intensity.Override(Mathf.Lerp(0f, 0.8f, t));
        vignette.smoothness.Override(Mathf.Lerp(0.2f, 0.9f, t));
    }
}
