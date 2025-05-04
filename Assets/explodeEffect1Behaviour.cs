using UnityEngine;

public class explodeEffect1Behaviour : MonoBehaviour
{
    public static explodeEffect1Behaviour Instance { get; private set; }
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

    public void explodeEffect1Activator(float offsetX, float offsetY) //-3.23f
    {
        if (Iori_data.Instance.ioriFlipped)
            offsetX *= -1;
        Transform iori_position = Iori_main.Instance.getIoriPosition();
        effectPosition.position = new Vector3(iori_position.position.x + offsetX, iori_position.position.y + offsetY, effectPosition.position.z);
        spriteRenderer.enabled = true;
        anim.SetTrigger("Explosion Effect1");
    }

    public void spriteRendererDeactivator()
    {
        spriteRenderer.enabled = false;
    }
}
