using UnityEngine;

public class infernalBloodLustChainHitboxBehaviour : MonoBehaviour
{
    [SerializeField] private string targetLayerName = "Kyo"; // The name of the layer for collision detection
    private int targetLayerMask;
    [SerializeField] Kyo_spriteActivator sprite;
    [SerializeField] Kyo_data kyo_data;
    [SerializeField] private SlowMotionEffect slowMotionEffect;
    [SerializeField] private KnockbackPhysicsKKyo knockBack;
    private Animator anim;


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
                CameraShake.Instance.ShakeCamera(5f, 0.1f);
                slowMotionEffect.TriggerSlowMotion(0.1f, 0.3f);
            }
        }
    }
}
