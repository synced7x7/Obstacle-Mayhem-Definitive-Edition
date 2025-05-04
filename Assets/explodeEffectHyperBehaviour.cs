using UnityEngine;

public class explodeEffectHyperBehaviour : MonoBehaviour
{
    public static explodeEffectHyperBehaviour Instance { get; private set; }
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

    public void explodeEffectHyperActivator(float offsetX, float offsetY, float offsetZ) //-3.23f
    {
        if (Iori_data.Instance.ioriFlipped)
            offsetX *= -1;
        Transform iori_position = Iori_main.Instance.getIoriPosition();
        effectPosition.position = new Vector3(iori_position.position.x + offsetX, iori_position.position.y + offsetY, iori_position.position.z + offsetZ);
        spriteRenderer.enabled = true;
        anim.SetTrigger("Explode Effect Hyper");
    }

    public void explodeEffect2Activator(float offsetX, float offsetY, float offsetZ) //-3.23f
    {
        if (Iori_data.Instance.ioriFlipped)
            offsetX *= -1;
        Transform iori_position = Iori_main.Instance.getIoriPosition();
        effectPosition.position = new Vector3(iori_position.position.x + offsetX, iori_position.position.y + offsetY, iori_position.position.z + offsetZ);
        spriteRenderer.enabled = true;
        anim.SetTrigger("Explode Effect 2");
        //Debug.Log("effect position z: " + effectPosition.position.z);
    }

    public void spriteRendererDeactivator()
    {
        spriteRenderer.enabled = false;
    }

    private void SlowmoConntrollerAE()
    {
        SlowMotionEffect.Instance.TriggerSlowMotion(0.5f, 0.5f);
    }
}
