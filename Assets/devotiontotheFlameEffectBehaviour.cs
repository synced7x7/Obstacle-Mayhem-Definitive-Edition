using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class devotiontotheFlameEffectBehaviour : MonoBehaviour
{
    public static devotiontotheFlameEffectBehaviour Instance { get; private set; }
    private Animator anim;
    private Transform effectPosition;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private bool flag = true;

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
            // Debug.Log("Sprite renderer in DarknessSweepEffectBehaviour set to false");
        }

        flag = true;
    }

    public void devotiontotheFlameEffectActivator(Transform ioriPosition)
    {
        if (flag)
        {
            SlowMotionEffect.Instance.TriggerSlowMotion(0.2f, 0.2f);
            CameraShake.Instance.ShakeCamera(10f, 0.1f); // camera shake
            if (!Iori_data.Instance.ioriFlipped)
                effectPosition.position = new Vector3(ioriPosition.position.x + 2f, effectPosition.position.y, effectPosition.position.z);
            else
                effectPosition.position = new Vector3(ioriPosition.position.x - 2f, effectPosition.position.y, effectPosition.position.z);
            spriteRenderer.enabled = true;
            Debug.Log("Devotion to the Flame Effect Activated");
            anim.SetTrigger("Devotion to the Flame Effect");
            StartCoroutine(devotiontotheFlameEffectTimeout());
            flag = false;
            devotiontotheFlameEffectHitboxBehaviour.Instance.hasHit = false;
        }
    }

    private IEnumerator devotiontotheFlameEffectTimeout()
    {
        yield return new WaitForSeconds(2.5f);
        spriteRenderer.enabled = false;
        flag = true;
    }

    public void devotiontotheFlameEffectFollowThroughActivator()
    {
        // SlowMotionEffect.Instance.TriggerSlowMotion(0.2f, 0.5f);
        CameraShake.Instance.ShakeCamera(2f, 0.1f); // camera shake
        //Debug.Log("Devotion to the Flame Effect Follow Through Activated");
        if (!flag)
            anim.SetTrigger("Devotion to the Flame Effect");
        if (!Iori_data.Instance.ioriFlipped)
            effectPosition.position += new Vector3(3f, 0f, 0f);
        else
            effectPosition.position -= new Vector3(3f, 0f, 0f);
    }
}
