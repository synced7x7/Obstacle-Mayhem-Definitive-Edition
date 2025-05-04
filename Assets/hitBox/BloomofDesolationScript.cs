using UnityEngine;

public class BloomofDesolationScript : MonoBehaviour
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
            if (!kyo_data.kyo_interval) //that means kyo is not in an affected state.
            {
                if (kyo_data.bloom_of_desolation_hit <= 1)
                {
                    kyo_data.bloom_of_desolation_hit++;
                    kyo_data.blood_of_desolation_lastHitTime = Time.time;
                    sprite.hurtaction(Kyo_spriteActivator.Action.hurt , 5f, 0.1f, Color.white , 0.1f);
                    knockBack.ApplyKnockback(Vector2.right, 10f, 20f); //knockback
                    slowMotionEffect.TriggerSlowMotion(0.2f, 0.2f);
                }
                else if (kyo_data.bloom_of_desolation_hit == 2)
                {
                    kyo_data.bloom_of_desolation_hit++;
                    kyo_data.blood_of_desolation_lastHitTime = Time.time;
                    sprite.hurtaction(Kyo_spriteActivator.Action.fall, 7.5f, 0.1f, Color.white, 0.1f);
                    knockBack.ApplyKnockback(Vector2.right, 15f, 20f); //knockback
                    slowMotionEffect.TriggerSlowMotion(0.5f, 0.1f);
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
                impactInstance.transform.localScale =new Vector3(1.2f, 1.2f , 1.2f);
                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = true;
                }

                if (impactAnimator != null)
                {
                    impactAnimator.SetTrigger("hit_impact");
                    if (kyo_data.bloom_of_desolation_hit == 3)
                        kyo_data.kyo_interval = true;
                }
                else
                {
                    Debug.LogWarning("The impact prefab does not have an Animator component.");
                }
            }

        }
    }
}
