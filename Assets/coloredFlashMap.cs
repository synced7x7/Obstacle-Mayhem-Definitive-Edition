using UnityEngine;
using System.Collections;
public class coloredFlashMap : MonoBehaviour
{
    #region Datamembers

    #region Editor Settings

    [Tooltip("Material to switch to during the flash.")]
    [SerializeField] private Material flashMaterial;

    [Tooltip("Duration of the flash.")]

    #endregion
    #region Private Fields

    // The SpriteRenderer that should flash.
    private SpriteRenderer spriteRenderer;

    // The material that was in use, when the script started.
    private Material originalMaterial;

    // The currently running coroutine.
    private Coroutine flashRoutine;
    private Color genuineColor;

    #endregion

    #endregion


    #region Methods

    #region Unity Callbacks

    void Start()
    {
        // Get the SpriteRenderer to be used,
        // alternatively you could set it from the inspector.
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the material that the SpriteRenderer uses, 
        // so we can switch back to it after the flash ended.
        originalMaterial = spriteRenderer.material;
        genuineColor = spriteRenderer.color;
        // Copy the flashMaterial material, this is needed, 
        // so it can be modified without any side effects.
        flashMaterial = new Material(flashMaterial);
    }

    #endregion

    public void Flash(Color color, float duration) //default value = 0.1
    {
        // If the flashRoutine is not null, then it is currently running.
        if (flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        flashRoutine = StartCoroutine(FlashRoutine(color, duration));
    }
    
    public void flashAlpha(Color color, float duration)
    {
        if (flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        flashRoutine = StartCoroutine(FlashRoutineAlpha(color, duration));
    }

    private IEnumerator FlashRoutineAlpha(Color color, float duration)
    {
        // Swap to the flashMaterial.
        Color initialColor = spriteRenderer.color; 
        spriteRenderer.color = color;

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(duration);

        // After the pause, swap back to the original material.
        spriteRenderer.color = initialColor;

        // Set the flashRoutine to null, signaling that it's finished.
        flashRoutine = null;
    }

    private IEnumerator FlashRoutine(Color color, float duration)
    {
        // Swap to the flashMaterial.
        spriteRenderer.material = flashMaterial;

        // Set the desired color for the flash.
        flashMaterial.color = color;

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(duration);

        // After the pause, swap back to the original material.
        spriteRenderer.material = originalMaterial;

        // Set the flashRoutine to null, signaling that it's finished.
        flashRoutine = null;
    }
    public void resetColor()
    {
        spriteRenderer.color = genuineColor;
    }

    #endregion
}
