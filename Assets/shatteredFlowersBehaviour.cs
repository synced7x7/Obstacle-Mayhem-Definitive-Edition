using UnityEngine;

public class shatteredFlowersBehaviour : MonoBehaviour
{
    [SerializeField] private string targetLayerName = "Kyo"; // The name of the layer for collision detection
    private int targetLayerMask;
    [SerializeField] Kyo_spriteActivator sprite;
    [SerializeField] Kyo_data kyo_data;
    [SerializeField] GameObject impactPrefab;
    [SerializeField] private SlowMotionEffect slowMotionEffect;
    [SerializeField] private KnockbackPhysicsKKyo knockBack;
    [SerializeField] private ColoredFlash coloredFlash;


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
                //sprite.hurtaction(Kyo_spriteActivator.Action.hurt, 5f, 0.1f, Color.white, 1.3f);
                CameraShake.Instance.ShakeCamera(7f, 0.1f); // camera shake
                slowMotionEffect.TriggerSlowMotion(0.2f, 0.2f);
                KnockbackPhysicsKyoXY.Instance.ApplyKnockback(
                    directionX: 1f,      // Positive X direction
                    directionY: 1f,      // Positive Y direction
                    forceX: 5f,         // Knockback force along X
                    forceY: 5f,          // Knockback force along Y
                    decelerationX: 20f,  // Deceleration rate along X
                    decelerationY: 20f   // Deceleration rate along Y
                );
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
