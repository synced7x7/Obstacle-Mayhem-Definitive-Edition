using UnityEngine;

public class groundKickhitboxBehaviour : MonoBehaviour
{
    [SerializeField] private string targetLayerName = "Kyo"; // The name of the layer for collision detection
    private int targetLayerMask;
    [SerializeField] Kyo_spriteActivator sprite;
    [SerializeField] Kyo_data kyo_data;
    [SerializeField] GameObject impactPrefab;
    [SerializeField] private SlowMotionEffect slowMotionEffect;
    [SerializeField] private KnockbackPhysicsKKyo knockBack;


    private void Awake()
    {
        // Convert layer name to a layer mask for efficient collision checks
        targetLayerMask = LayerMask.NameToLayer(targetLayerName);

        if (targetLayerMask == -1)
        {
            Debug.LogWarning($"Layer '{targetLayerName}' does not exist. Please check your Layer settings.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object's layer matches the target layer
        if (collision.gameObject.layer == targetLayerMask)
        {
            if (!kyo_data.kyo_interval)
            {
                kyo_data.kyo_hit++;
                kyo_data.last_hit_time = Time.time;


                if (kyo_data.kyo_hit < 2)
                {
                    slowMotionEffect.TriggerSlowMotion(0.1f, 0.2f);
                    knockBack.ApplyKnockback(Vector2.right, 4f, 20f); //knockback
                    sprite.hurtaction(Kyo_spriteActivator.Action.hurt, 5f, 0.1f, Color.white, 0.1f);
                }

                else if (kyo_data.kyo_hit == 2)
                {
                    slowMotionEffect.TriggerSlowMotion(0.3f, 0.3f);
                    sprite.hurtaction(Kyo_spriteActivator.Action.fall, 6f, 0.1f, Color.white, 0.1f);
                    knockBack.ApplyKnockback(Vector2.right, 8f, 20f); //knockback

                }


            }
            //hit_impact profile
            if (impactPrefab != null && !kyo_data.kyo_interval)
            {
                Vector3 impactPosition = collision.contacts[0].point;
                impactPosition.z = -1.5f;
                GameObject impactInstance = Instantiate(impactPrefab, impactPosition, Quaternion.identity);
                Animator impactAnimator = impactInstance.GetComponent<Animator>();

                SpriteRenderer spriteRenderer = impactInstance.GetComponent<SpriteRenderer>();
                impactInstance.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = true;
                }

                if (impactAnimator != null)
                {
                    impactAnimator.SetTrigger("hit_impact");
                }
                else
                {
                    Debug.LogWarning("The impact prefab does not have an Animator component.");
                }
            }

        }
    }
}
