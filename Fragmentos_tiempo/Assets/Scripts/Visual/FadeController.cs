using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public float FadeDuration = 1.5f;

    private Image fadeImage;
    private bool isFading = false;

    void Awake()
    {
        fadeImage = GetComponent<Image>();
    }

    public void FadeToBlack()
    {
        if (!isFading)
            StartCoroutine(FadeRoutine(0f, 1f));
    }

    public void FadeFromBlack()
    {
        if (!isFading)
            StartCoroutine(FadeRoutine(1f, 0f));
    }

    private IEnumerator FadeRoutine(float start, float end)
    {
        isFading = true;
        float t = 0;

        while (t < FadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float a = Mathf.Lerp(start, end, t / FadeDuration);
            fadeImage.color = new Color(0, 0, 0, a);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, end);
        isFading = false;
    }
}
