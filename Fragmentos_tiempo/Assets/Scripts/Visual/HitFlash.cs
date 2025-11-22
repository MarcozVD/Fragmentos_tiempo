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

        Color c = vignetteImage.color;
        c.a = 0f;
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
        Color c = vignetteImage.color;

        // Fade IN
        while (c.a < maxAlpha)
        {
            c.a += Time.deltaTime * fadeInSpeed;
            vignetteImage.color = c;
            yield return null;
        }

        // Fade OUT
        while (c.a > 0f)
        {
            c.a -= Time.deltaTime * fadeOutSpeed;
            vignetteImage.color = c;
            yield return null;
        }

        c.a = 0f;
        vignetteImage.color = c;
    }
}
