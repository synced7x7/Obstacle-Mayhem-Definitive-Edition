
using UnityEngine;
using UnityEngine.UIElements;

public class rainBehaviour : MonoBehaviour
{
    public static rainBehaviour Instance { get; private set; }
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
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }
    public void rainActivator()
    {
        spriteRenderer.enabled = true;
    }
    public void rainDeactivator()
    {
        spriteRenderer.enabled = false;
    }
}
