using UnityEngine;
using System.Collections;

public class speedyanimationBGBehaviour : MonoBehaviour
{
    public static speedyanimationBGBehaviour Instance { get; private set; }
    private SpriteRenderer spriteRenderer;
    private Transform speedyAnimationPosition;
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
        speedyAnimationPosition = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        SetSpriteAlpha(0f); // Start with fully transparent background
    }

    public void speedyAnimationActivator()
    {

        Transform ioriPosition = Iori_main.Instance.getIoriPosition();
        if (!Iori_data.Instance.ioriFlipped)
            speedyAnimationPosition.position = new Vector3(ioriPosition.position.x + 5f, speedyAnimationPosition.position.y, speedyAnimationPosition.position.z);
        else
            speedyAnimationPosition.position = new Vector3(ioriPosition.position.x - 5f, speedyAnimationPosition.position.y, speedyAnimationPosition.position.z);
        spriteRenderer.enabled = true;
        fadeInSprite(0.1f);
    }
    public void speedyAnimationDeactivator(float duration)
    {
        fadeOutSprite(duration);
    }
    public void fadeInSprite(float duration)
    {
        StartCoroutine(FadeINSpriteRoutine(0f, 1f, duration));
    }
    public void fadeOutSprite(float duration)
    {
        MapBaseBehaviour.Instance.customMapActivator();
        StartCoroutine(FadeOUTSpriteRoutine(1f, 0f, duration));
    }
    private IEnumerator FadeINSpriteRoutine(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color color = spriteRenderer.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            SetSpriteAlpha(alpha);
            yield return null;
        }
        MapBaseBehaviour.Instance.customMapDeactivator();
        SetSpriteAlpha(endAlpha); // Ensure the final alpha is set
    }
    private IEnumerator FadeOUTSpriteRoutine(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color color = spriteRenderer.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            SetSpriteAlpha(alpha);
            yield return null;
        }

        SetSpriteAlpha(endAlpha); // Ensure the final alpha is set
        spriteRenderer.enabled = false;
    }

    private void SetSpriteAlpha(float alpha)
    {
        Color color = spriteRenderer.color; // Step 1: Get the current color of the sprite.
        color.a = alpha;                   // Step 2: Modify the alpha (transparency) value.
        spriteRenderer.color = color;      // Step 3: Apply the updated color back to the sprite.
    }

}
