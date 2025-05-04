using UnityEngine;
using System.Collections;
using System.Data;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class BurnEffectBehaviour : MonoBehaviour
{
    public static BurnEffectBehaviour Instance { get; private set; }
    [SerializeField] Iori_data Idata;
    private Animator anim;
    private Transform effectPosition;
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

        anim = GetComponent<Animator>();
        effectPosition = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
    }

    public void BurnEffectActivator(float delay, float offsetX, float offsetY) //-3.23f
    {
        Transform kyo_position = Kyo_main.Instance.getKyoPosition();
        effectPosition.position = new Vector3(kyo_position.position.x + offsetX, kyo_position.position.y + offsetY, effectPosition.position.z);
        spriteRenderer.enabled = true;
        anim.SetBool("Burn effect", true);
        if (!Idata.fangsOfTheInferno) //doesn't follow kyo //other moves except fangs of the inferno
            StartCoroutine(burnEffectCooldown(delay));
        else //follow kyo // fangs of the Inferno
        {
            float followUpDuration = 1.4f; // Set the duration for follow-up movement
            burnEffectFollowUpActivator(followUpDuration);
        }
    }

    public void burnEffectFollowUpActivator(float followUpDuration)
    {
        StartCoroutine(burnEffectFollowUp(followUpDuration));
    }

    private IEnumerator burnEffectCooldown(float delay)
    {
        yield return new WaitForSeconds(delay);
        anim.SetBool("Burn effect", false);
        Debug.Log("Burn effect cooled down after " + delay + " seconds");
        spriteRenderer.enabled = false;
    }

    public IEnumerator burnEffectFollowUp(float duration)
    {
        float elapsedTime = 0f; // Tracks time passed
        while (elapsedTime < duration)
        {
            // Update effect position to follow Kyo
            Transform kyo_position = Kyo_main.Instance.getKyoPosition();
            effectPosition.position = new Vector3(kyo_position.position.x, kyo_position.position.y - 3.23f, effectPosition.position.z);

            elapsedTime += Time.deltaTime; // Increment elapsed time
            yield return null; // Wait for the next frame
        }
        Debug.Log("Burn effect cooled down after " + duration + " seconds");
        // End the effect after follow-up
        anim.SetBool("Burn effect", false);
        spriteRenderer.enabled = false;
    }

}
