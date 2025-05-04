using UnityEngine;
using System.Collections;
using NUnit.Framework;

public class DarknessSweepEffectBehaviour : MonoBehaviour
{
    public static DarknessSweepEffectBehaviour Instance { get; private set; }
    private Animator anim;
    private Transform effectPosition;
    private SpriteRenderer spriteRenderer;
    private Coroutine activeCoroutine;
    private Vector3 initialPosition;
    private float distance;
    private BoxCollider2D boxCollider;

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
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        effectPosition = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
            // Debug.Log("Sprite renderer in DarknessSweepEffectBehaviour set to false");
        }

        initialPosition = effectPosition.position; // Save initial position
        distance = 0;
    }

    public void impactActivator_darknessSweepEffect(Transform ioriPosition)
    {
        // Stop any active coroutine to prevent overlap
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
            disableSpriteRenderer();
        }

        // Reset effect position to ensure fresh start
        effectPosition.position = new Vector3(ioriPosition.position.x + 1.8f, effectPosition.position.y, effectPosition.position.z);

        //Debug.Log("Effect Position Updated to: " + effectPosition.position + " Effect Scale updated to: " + effectPosition.localScale);

        // Trigger the animation
        spriteRenderer.enabled = true;
        boxCollider.enabled = true;
        anim.SetBool("DarknessSweepEffect", true);
        darknessSweepHitboxBehaviour.Instance.hasHit = false;

        // Start the timeout coroutine
        activeCoroutine = StartCoroutine(DarknessSweepEffectTimeout(effectPosition.position.x + 20f)); // Target x-distance
    }

    private IEnumerator DarknessSweepEffectTimeout(float targetX)
    {
        float speed = 15f;
        Vector3 startPosition = effectPosition.position;

        while (effectPosition.position.x < targetX)
        {
            if (darknessSweepHitboxBehaviour.Instance.hasHit == true)
            {
                yield return null;
                break;
            }
            // Debug.Log("distance: " + distance + " Speed: " +speed);
            distance = speed * Time.deltaTime;
            if (!Iori_data.Instance.ioriFlipped)
                effectPosition.position += new Vector3(distance, 0f, 0f);
            else
                effectPosition.position -= new Vector3(distance, 0f, 0f);
            yield return null;
        }



        // Reset state
        disableSpriteRenderer();
        //effectPosition.position = startPosition;
        Debug.Log("Darkness Sweep Effect finished.");

        // Clear the active coroutine reference
    }

    public void disableSpriteRenderer()
    {
        anim.SetBool("DarknessSweepEffect", false);
        spriteRenderer.enabled = false;
        effectPosition.position = initialPosition; // Reset to initial position
        activeCoroutine = null;
        boxCollider.enabled = false;
    }
}
