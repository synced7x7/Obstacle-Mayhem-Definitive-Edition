using Unity.VisualScripting;
using UnityEngine;

public class devotiontotheFlameEffectHitboxBehaviour : MonoBehaviour
{
    public static devotiontotheFlameEffectHitboxBehaviour Instance { get; private set; }
    [SerializeField] private string targetLayerName = "Kyo"; // The name of the layer for collision detection
    private int targetLayerMask;
    [SerializeField] Kyo_spriteActivator sprite;
    [SerializeField] Kyo_data kyo_data;
    [SerializeField] private SlowMotionEffect slowMotionEffect;
    [SerializeField] private KnockbackPhysicsKKyo knockBack;
    [SerializeField] private Kyo_spriteActivator kyo_SpriteActivator;
    [SerializeField] private ColoredFlash coloredFlash;
    private Animator anim;

    public bool hasHit = false; // Flag to track if the projectile has already hit

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
        // Convert layer name to a layer mask for efficient collision checks
        targetLayerMask = LayerMask.NameToLayer(targetLayerName);

        if (targetLayerMask == -1)
        {
            Debug.LogWarning($"Layer '{targetLayerName}' does not exist. Please check your Layer settings.");
        }

        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Prevent repeated collisions
        if (hasHit) return;

        // Check if the collided object's layer matches the target layer
        if (collision.gameObject.layer == targetLayerMask)
        {
            if (!kyo_data.kyo_interval) // That means Kyo is not in an affected state
            {
                float minDevotionTime = 0.005f;
                float maxDevotionTime = 1.015f;
                float minMultiplier = 1.2f;
                float maxMultiplier = 1.7f;

                // Calculate the multiplier based on devotion time
                float devotionMultiplier = CalculateMultiplier(Iori_data.Instance.devotionTime, minDevotionTime, maxDevotionTime, minMultiplier, maxMultiplier);
                Debug.Log("Devotion Multiplier = " + devotionMultiplier);
                hasHit = true; // Mark as hit
                Color ioriViolet = new Color(0.5f, 0f, 0.5f); // RGB approximation

                //trigger burn effect
                BurnEffectBehaviour.Instance.BurnEffectActivator(1.5f, 0, -3.23f);
                BurnEffectBehaviour.Instance.burnEffectFollowUpActivator(devotionMultiplier);//////
                //
                CameraShake.Instance.ShakeCamera(10f, 0.1f); // camera shake
                coloredFlash.Flash(ioriViolet, devotionMultiplier); //flash effect\//////
                kyo_SpriteActivator.customAnimation(Kyo_spriteActivator.customAnimationRunner.belly_hurt);
                KyoManualSpriteController.Instance.AnimationResponsePauseRiotOfTheBlood(devotionMultiplier);//////
                knockBack.ApplyKnockback(Vector2.right, 3f, 20f); // Knockback
                slowMotionEffect.TriggerSlowMotion(0.2f, 0.2f);
            }
        }
    }

    float CalculateMultiplier(float devotionTime, float minDevotionTime, float maxDevotionTime, float minMultiplier, float maxMultiplier)
    {
        // Clamp devotionTime to ensure it stays within bounds
        devotionTime = Mathf.Clamp(devotionTime, minDevotionTime, maxDevotionTime);

        // Normalize devotion time and map it to multiplier range
        return minMultiplier + ((devotionTime - minDevotionTime) / (maxDevotionTime - minDevotionTime)) * (maxMultiplier - minMultiplier);
    }
}
