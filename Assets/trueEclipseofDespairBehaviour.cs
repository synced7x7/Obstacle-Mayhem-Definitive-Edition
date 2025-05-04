using UnityEngine;
using System.Collections;

public class trueEclipseofDespairBehaviour : MonoBehaviour
{
    public static trueEclipseofDespairBehaviour Instance { get; private set; }
    private SpriteRenderer spriteRenderer;
    private Color genuineColor;

    private void Awake()
    {
        #region instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        #endregion
        spriteRenderer = GetComponent<SpriteRenderer>();
        genuineColor = spriteRenderer.color;
        spriteRenderer.enabled = false;
    }

    public void trueEclipseOfDespairMapActivator()
    {
        spriteRenderer.enabled = true;
    }
    public void trueEclipseOfDespairMapDeactivator()
    {
        spriteRenderer.enabled = false;
    }
    public void trueEclipseOfDespairFade(float timeBetweenFade, float fadeDuration, Color targetColor)
    {
        StartCoroutine(fadeinitiate(timeBetweenFade, fadeDuration, targetColor));
    }
    private IEnumerator fadeinitiate(float timeBetweenFade, float fadeDuration, Color targetColor)
    {
        Color genuineColor = spriteRenderer.color;
        yield return StartCoroutine(FadeCoroutine(fadeDuration, targetColor));
        yield return new WaitForSeconds(timeBetweenFade);
        yield return StartCoroutine(FadeCoroutine(fadeDuration, genuineColor));
    }

    private IEnumerator FadeCoroutine(float fadeDuration, Color targetColor)
    {
        float elapsedTime = 0f;
        Color initialColor = spriteRenderer.color; // Store the current color of the sprite

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            // Interpolate between the initial color and the target color over time
            Color blendedColor = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            spriteRenderer.color = blendedColor; // Apply the interpolated color
            yield return null;
        }

        spriteRenderer.color = targetColor; // Ensure the final color matches the target color
    }

    public void FadeIN(float fadeDuration, Color targetColor)
    {
        StartCoroutine(FadeCoroutine(fadeDuration, targetColor));
    }
    public void FadeOut(float fadeDuration)
    {
        StartCoroutine(FadeCoroutine(fadeDuration, genuineColor));
    }

}
