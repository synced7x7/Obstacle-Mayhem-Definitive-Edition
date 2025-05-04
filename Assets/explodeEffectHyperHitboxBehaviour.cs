using UnityEngine;

public class explodeEffectHyperHitboxBehaviour : MonoBehaviour
{
    [SerializeField] private string targetLayerName = "Kyo"; // The name of the layer for collision detection
    private int targetLayerMask;
    [SerializeField] Kyo_data kyo_data;
    [SerializeField] private SlowMotionEffect slowMotionEffect;

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
                //slowMotionEffect.TriggerSlowMotion(0.2f, 0.6f);
            }
        }
    }
}
