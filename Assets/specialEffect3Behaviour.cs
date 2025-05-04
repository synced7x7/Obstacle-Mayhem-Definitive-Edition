using UnityEngine;

public class specialEffect3Behaviour : MonoBehaviour
{
    public static specialEffect3Behaviour Instance { get; private set; }
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

    public void specialEffect3Activator() //-3.23f
    {
        Transform iori_position = Iori_main.Instance.getIoriPosition();
        if(!Iori_data.Instance.ioriFlipped)
        effectPosition.position = new Vector3(iori_position.position.x +3f, iori_position.position.y - 1f, effectPosition.position.z);
        else
        effectPosition.position = new Vector3(iori_position.position.x -3f, iori_position.position.y - 1f, effectPosition.position.z);
        
        spriteRenderer.enabled = true;
        anim.SetTrigger("Special Effect3");
    }

    public void spriteRendererDeactivator()
    {
        spriteRenderer.enabled = false;
    }
}
