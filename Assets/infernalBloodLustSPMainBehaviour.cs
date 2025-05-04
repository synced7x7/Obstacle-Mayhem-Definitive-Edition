using System.Collections;
using UnityEngine;

public class infernalBloodLustSPMainBehaviour : MonoBehaviour
{
    public static infernalBloodLustSPMainBehaviour Instance { get; private set; }
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private Transform effectPos;

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
        anim = GetComponent<Animator>();
        effectPos = GetComponent<Transform>();
        spriteRenderer.enabled = false;
    }

    public void infernalBloodLustSPMainActivator()
    {
        float offsetX = -5f;
        if (Iori_data.Instance.ioriFlipped)
            offsetX *= -1;

        // Ensure sprite and animation play independently of time scale
        spriteRenderer.enabled = true;
        anim.updateMode = AnimatorUpdateMode.UnscaledTime; // Use unscaled time for the cutscene

        Transform ioriPos = Iori_main.Instance.getIoriPosition();
        effectPos.position = new Vector3(ioriPos.position.x + offsetX, effectPos.position.y, effectPos.position.z);
        anim.SetTrigger("Infernal Blood Lust SP Main");

    }
    private void ResumeGame()
    {
        spriteRenderer.enabled = false;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f; // Reset to default physics timestep
    }
}
