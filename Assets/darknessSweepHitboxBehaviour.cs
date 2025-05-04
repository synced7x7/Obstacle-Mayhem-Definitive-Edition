using UnityEngine;

public class darknessSweepHitboxBehaviour : MonoBehaviour
{
    public static darknessSweepHitboxBehaviour Instance { get; private set; }
    [SerializeField] private string targetLayerName = "Kyo"; // The name of the layer for collision detection
    private int targetLayerMask;
    [SerializeField] Kyo_spriteActivator sprite;
    [SerializeField] Kyo_data kyo_data;
    [SerializeField] private SlowMotionEffect slowMotionEffect;
    [SerializeField] private KnockbackPhysicsKKyo knockBack;
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
                hasHit = true; // Mark as hit
                Color ioriViolet = new Color(0.5f, 0f, 0.5f); // RGB approximation

                //trigger burn effect
                BurnEffectBehaviour.Instance.BurnEffectActivator(0.5f, 0 , -3.23f);
                //
                sprite.hurtaction(Kyo_spriteActivator.Action.hurt, 5f, 0.1f, ioriViolet, 0.5f);
                DarknessSweepEffectBehaviour.Instance.disableSpriteRenderer();
                knockBack.ApplyKnockback(Vector2.right, 4f, 20f); // Knockback
                slowMotionEffect.TriggerSlowMotion(0.1f, 0.2f);
            }
        }
    }

}
