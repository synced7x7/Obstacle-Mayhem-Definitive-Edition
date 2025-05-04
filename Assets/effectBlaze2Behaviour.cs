using UnityEngine;

public class effectBlaze2Behaviour : MonoBehaviour
{
    public static effectBlaze2Behaviour Instance { get; private set; }
    private Animator anim;
    private Transform effectPosition;
    private SpriteRenderer spriteRenderer;
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
        }
    }

    public void effectBlaze2Activator(float offsetX, float offsetY) //-3.23f
    {
        if (Iori_data.Instance.ioriFlipped)
        {
            offsetX *= -1;
            offsetX+=3f;
        }
            
        Transform iori_position = Iori_main.Instance.getIoriPosition();
        effectPosition.position = new Vector3(iori_position.position.x + offsetX, iori_position.position.y + offsetY, effectPosition.position.z);
        spriteRenderer.enabled = true;
        anim.SetTrigger("Effect Blaze2");
    }

    public void spriteRendererDeactivator()
    {
        spriteRenderer.enabled = false;
    }

    private void kyoGravityController() // controlled by animation event
    {
        Kyo_physicsHandler.Instance.kyoGravityController();
        KnockbackPhysicsKKyo.Instance.ApplyKnockback(Vector2.right, 15f, 20f); //knockback
        Debug.Log("Slow mo.Blazed");
        SlowMotionEffect.Instance.TriggerSlowMotion(2.5f, 0.2f);
    }
}
