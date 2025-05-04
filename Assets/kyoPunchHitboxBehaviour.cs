using UnityEngine;

public class kyoPunchHitboxBehaviour : MonoBehaviour
{
    [SerializeField] private string targetLayerName = "Iori"; // The name of the layer for collision detection
    private int targetLayerMask;
    [SerializeField] Iori_spriteActivator sprite;
    [SerializeField] Iori_data iori_data;
    [SerializeField] GameObject impactPrefab;
    [SerializeField] private SlowMotionEffect slowMotionEffect;
    [SerializeField] private IoriKnockback_horizontal knockBack;


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
            iori_data.iori_hit++;
            iori_data.last_hit_time = Time.time;
            
            sprite.HurtReactionActivator(Iori_spriteActivator.HurtReaction.Mid, 5f, 0.1f, Color.white, 0.1f);
            // Example usage in another script
            knockBack.ApplyKnockback(Vector2.right, 4f, 20f); //knockback
            
            slowMotionEffect.TriggerSlowMotion(0.1f, 0.4f);
        }
        //hit_impact profile
        if (impactPrefab != null)
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

