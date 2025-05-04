using UnityEngine;

public class specialEffect4Behaviour : MonoBehaviour
{
    public static specialEffect4Behaviour Instance { get; private set; }
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

    public void specialEffect4Activator() //-3.23f
    {
        Transform iori_position = Iori_main.Instance.getIoriPosition();
        effectPosition.position = new Vector3(iori_position.position.x, iori_position.position.y-2f, effectPosition.position.z);
        spriteRenderer.enabled = true;
        anim.SetTrigger("Special Effect 4");
    }

    public void spriteRendererDeactivator()
    {
        spriteRenderer.enabled = false;
    }
}
