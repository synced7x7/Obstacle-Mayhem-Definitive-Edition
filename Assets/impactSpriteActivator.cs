using UnityEngine;
using System.Collections;

public class impactSpriteActivator : MonoBehaviour
{
    private Animator anim;
    private Transform effectPosition;
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRendere
    private Vector3 originalScale;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        effectPosition = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
            //Debug.Log("Sprite renderer in impactSpriteActivator Script set to false");
        }
        originalScale = effectPosition.localScale;
    }
    public void impactActivator_effect1(Transform ioriPosition, Vector3 newScale)
    {
        if (!Iori_data.Instance.ioriFlipped)
            effectPosition.position = new Vector3(ioriPosition.position.x + 1.9f, ioriPosition.position.y + 1.2f, effectPosition.position.z); //hard coded // more optimized than box collider as the animation is fixed
        else
            effectPosition.position = new Vector3(ioriPosition.position.x - 1.9f, ioriPosition.position.y + 1.2f, effectPosition.position.z);
        effectPosition.localScale = newScale;
        // Debug.Log("Effect Position Updated to: " + effectPosition.position + "Effect Scale update to: " + effectPosition.localScale);

        // Trigger the animation
        spriteRenderer.enabled = true;
        anim.SetTrigger("effect1");
    }
    public void impactActivator_effect2(Transform ioriPosition, Vector3 newScale)
    {
        if (!Iori_data.Instance.ioriFlipped)
            effectPosition.position = new Vector3(ioriPosition.position.x + 1.8f, ioriPosition.position.y, effectPosition.position.z); //hard coded // more optimized than box collider as the animation is fixed
        else
            effectPosition.position = new Vector3(ioriPosition.position.x - 1.8f, ioriPosition.position.y, effectPosition.position.z);
        effectPosition.localScale = newScale;
        // Debug.Log("Effect Position Updated to: " + effectPosition.position + "Effect Scale update to: " + effectPosition.localScale);

        // Trigger the animation
        spriteRenderer.enabled = true;
        anim.SetTrigger("effect2");
    }

    public void impactActivator_effectBlaze(Transform ioriPosition, Vector3 newScale)
    {
        if (!Iori_data.Instance.ioriFlipped)
        effectPosition.position = new Vector3(ioriPosition.position.x + 1.8f, ioriPosition.position.y - 0.2f, effectPosition.position.z); //hard coded // more optimized than box collider as the animation is fixed
        else
        effectPosition.position = new Vector3(ioriPosition.position.x - 1.8f, ioriPosition.position.y - 0.2f, effectPosition.position.z);
        effectPosition.localScale = newScale;
        // Debug.Log("Effect Position Updated to: " + effectPosition.position + "Effect Scale update to: " + effectPosition.localScale);

        // Trigger the animation
        spriteRenderer.enabled = true;
        anim.SetTrigger("effectblaze1");
    }
    //position changable with time.




    public void disableSpriteRenderer()
    {
        spriteRenderer.enabled = false;
        effectPosition.localScale = originalScale;
    }
}
