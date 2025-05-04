using UnityEngine;
using System.Collections;

public class shatterredFlowersSPBehaviour : MonoBehaviour
{
     public static shatterredFlowersSPBehaviour Instance { get; private set; }
    private Transform ioriTransform;
    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] private float slowZoneRadius = 0.2f;
    [SerializeField] private float slowSpeed = 1f;
    [SerializeField] private float accelerationTime = 1f;
    [SerializeField] private float fadeDuration = 0.1f; // Duration for fade-in and fade-out
    [SerializeField] private float timeBetweenFade = 0.15f;
    private bool isMoving = false;
    private SpriteRenderer spriteRenderer;

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
        spriteRenderer.enabled = true; // Keep it enabled for fade effect
        SetSpriteAlpha(0f); // Start with fully transparent background

        // Assign ioriTransform
        GameObject iori = GameObject.FindWithTag("Player");
        if (iori != null)
        {
            ioriTransform = iori.transform;
        }
        else
        {
            Debug.LogError("Iori's Transform not found! Please ensure the Player tag is assigned.");
        }
    }

    public void StartBackgroundMove(float startPosition, float endPosition)
    {
        //Debug.Log("Start Position: " + startPosition + " End position: " + endPosition);

        transform.position = new Vector2(startPosition, transform.position.y);

        if (!isMoving)
        {
            StartCoroutine(MoveCoroutine(endPosition));
            StartCoroutine(FadeCoroutine(timeBetweenFade));
        }
    }

    private IEnumerator MoveCoroutine(float endPosition)
    {
        // Start Fade-In
        spriteRenderer.enabled = true;

        isMoving = true;
        float currentSpeed = moveSpeed;

        while (Mathf.Abs(transform.position.x - endPosition) > 0.1f)
        {
            float distanceToIori = Vector3.Distance(transform.position, ioriTransform.position);
            //Debug.Log("Tranform position " + transform.position.x + " iori Position: " + ioriTransform.position.x);

            if (distanceToIori <= slowZoneRadius)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, slowSpeed, Time.deltaTime * accelerationTime);
            }
            else
            {
                currentSpeed = Mathf.Lerp(currentSpeed, moveSpeed, Time.deltaTime * accelerationTime);

            }

            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(endPosition, transform.position.y, transform.position.z),
                currentSpeed * Time.deltaTime);

            yield return null;
        }

        transform.position = new Vector3(endPosition, transform.position.y, transform.position.z);
        isMoving = false;
        spriteRenderer.enabled = false;
    }
    private IEnumerator FadeCoroutine(float timeBetweenFade)
    {
        yield return StartCoroutine(FadeSprite(0f, 1f, fadeDuration));
        yield return new WaitForSeconds(timeBetweenFade);
        yield return StartCoroutine(FadeSprite(1f, 0f, fadeDuration));
    }

    private IEnumerator FadeSprite(float startAlpha, float endAlpha, float duration)
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
    }

    private void SetSpriteAlpha(float alpha)
    {
        Color color = spriteRenderer.color; // Step 1: Get the current color of the sprite.
        color.a = alpha;                   // Step 2: Modify the alpha (transparency) value.
        spriteRenderer.color = color;      // Step 3: Apply the updated color back to the sprite.
    }
}
