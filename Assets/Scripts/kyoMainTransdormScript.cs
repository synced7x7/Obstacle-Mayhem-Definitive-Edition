using UnityEngine;
using System.Collections;

public class kyoMainTransformScript : MonoBehaviour
{
    public static kyoMainTransformScript Instance { get; private set; }
    private BoxCollider2D boxCollider;
    private Animator anim;
    [SerializeField] private Rigidbody2D kyobody;
    [SerializeField] private Kyo_spriteActivator kyo_SpriteActivator;
    [SerializeField] private SlowMotionEffect slowMotionEffect;

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
        // Get reference to the BoxCollider2D
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            Debug.LogError("No BoxCollider2D attached to the Kyo GameObject!");
        }
        anim = GetComponent<Animator>();
        kyobody = GetComponent<Rigidbody2D>();
    }
    #region fangs Of the inferno
    public void KyoMainTransformFunction() //fangs of the inferno
    {
        if (!Iori_data.Instance.ioriFlipped)
            StartCoroutine(RotateAndHoldBody(new Vector3(0, 0, -90), 0.1f));
        else
            StartCoroutine(RotateAndHoldBody(new Vector3(0, 0, 90), 0.1f));
    }
    //coroutine // parallel work load along with update
    private IEnumerator RotateAndHoldBody(Vector3 targetRotation, float duration)
    {
        boxCollider.enabled = false;
        // Capture initial rotation and position
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(targetRotation);

        // Fixed position offset
        Vector3 startPosition = transform.position;
        Vector3 targetPosition;
        if (!Iori_data.Instance.ioriFlipped)
            targetPosition = startPosition + new Vector3(2.615f, -4.24f, 0.1f); // Adjust the offset as needed
        else
            targetPosition = startPosition + new Vector3(-2.615f, -4.24f, 0.1f); // Adjust the offset as needed

        float elapsed = 0f;

        // Rotate and move the body smoothly
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
        transform.position = targetPosition;


        yield return new WaitForSeconds(1f); // Adjust hold time as needed

        //anim.SetTrigger("Fall Hit");
        //fangs of the inferno
        kyo_SpriteActivator.hurtaction(Kyo_spriteActivator.Action.fall, 7.5f, 0.1f, new Color(0.5f, 0f, 0.5f), 1.5f);
        KnockbackPhysicsKKyo.Instance.ApplyKnockback(Vector2.right, 8f, 20f); //knockback
        //slowMotionEffect.TriggerSlowMotion(0.1f, 0.2f);
        BurnEffectBehaviour.Instance.BurnEffectActivator(1f, 0, -3.23f);
        BurnEffectBehaviour.Instance.burnEffectFollowUp(1f);

        //
        transform.position = startPosition;
        transform.rotation = startRotation;
        yield return new WaitForSeconds(1f);
        boxCollider.enabled = true;
    }
    #endregion


    #region blood blossom of the oblivion

    public void BloosBlossomOfTheOblivionKyoBehaviour()
    {
        if (!Iori_data.Instance.ioriFlipped)
            StartCoroutine(RotateAndHoldBBO(new Vector3(0, 0, -90), 0.4f));
        else
            StartCoroutine(RotateAndHoldBBO(new Vector3(0, 0, 90), 0.4f));
    }

    private IEnumerator RotateAndHoldBBO(Vector3 targetRotation, float duration)
    {
        boxCollider.enabled = false;
        // Capture initial rotation and position
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(targetRotation);

        // Fixed position offset
        Vector3 startPosition = transform.position;
        Vector3 targetPosition;
        if (!Iori_data.Instance.ioriFlipped)
            targetPosition = startPosition + new Vector3(-0.125f, -4.91f, 0.1f); // Adjust the offset as needed
        else
            targetPosition = startPosition + new Vector3(0.125f, -4.91f, 0.1f); // Adjust the offset as needed

        float elapsed = 0f;

        // Rotate and move the body smoothly
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
        transform.position = targetPosition;


        yield return new WaitForSeconds(4f); // Adjust hold time as needed
        Debug.Log("Blood blossom of the Oblivion Timeout");
        //SlowMotionEffect.Instance.TriggerSlowMotion(2f, 0.1f);
        anim.SetBool("Fall static", false);
        kyo_SpriteActivator.hurtaction(Kyo_spriteActivator.Action.fall, 7.5f, 0.1f, new Color(0.5f, 0f, 0.5f), 1.5f);
        KnockbackPhysicsKKyo.Instance.ApplyKnockback(Vector2.right, 15f, 20f); //knockback

        // BurnEffectBehaviour.Instance.BurnEffectActivator(1f, 0, -3.23f);
        //
        transform.position = startPosition;
        transform.rotation = startRotation;
        yield return new WaitForSeconds(1f);
        boxCollider.enabled = true;
    }

    #endregion
}