using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HitFlash : MonoBehaviour
{
    public Image vignetteImage;

    [Header("Config")]
    public float fadeInSpeed = 4f;
    public float fadeOutSpeed = 2f;
    public float maxAlpha = 0.8f;

    private Coroutine flashRoutine;

    private void Start()
    {
        if (vignetteImage == null)
        {
            Debug.LogError("❌ No asignaste la imagen de la viñeta.");
            return;
        }

        SetAlpha(0f); // Forzar invisible al inicio
    }

    private void SetAlpha(float a)
    {
        Color c = vignetteImage.color;
        c.a = a;
        vignetteImage.color = c;
    }

    public void Flash()
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(FlashEffect());
    }

    private IEnumerator FlashEffect()
    {
        float a = vignetteImage.color.a;

        while (a < maxAlpha)
        {
            a += Time.unscaledDeltaTime * fadeInSpeed;
            SetAlpha(a);
            yield return null;
        }

        while (a > 0f)
        {
            a -= Time.unscaledDeltaTime * fadeOutSpeed;
            SetAlpha(a);
            yield return null;
        }

        SetAlpha(0f);
    }
}
