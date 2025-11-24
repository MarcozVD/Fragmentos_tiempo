using UnityEngine;
using TMPro;
using System.Collections;

public class FadeText: MonoBehaviour
{
    public TextMeshProUGUI uiText;
    public float fadeSpeed = 3f;
    public float visibleTime = 1.5f;

    private Coroutine fadeCoroutine;

    private void Awake()
    {
        if (uiText == null)
            uiText = GetComponentInChildren<TextMeshProUGUI>();

        SetAlpha(0f); // iniciar invisible
    }

    public void ShowMessage(string message = "")
    {
        if (uiText != null && !string.IsNullOrEmpty(message))
            uiText.text = message;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeTextRoutine());
    }

    private IEnumerator FadeTextRoutine()
    {
        // FADE IN
        yield return StartCoroutine(Fade(1f));

        // Mantener visible
        yield return new WaitForSeconds(visibleTime);

        // FADE OUT
        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = uiText.color.a;

        while (!Mathf.Approximately(uiText.color.a, targetAlpha))
        {
            float newAlpha = Mathf.MoveTowards(uiText.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            SetAlpha(newAlpha);
            yield return null;
        }
    }

    private void SetAlpha(float alpha)
    {
        Color c = uiText.color;
        c.a = Mathf.Clamp01(alpha);
        uiText.color = c;
    }
}
