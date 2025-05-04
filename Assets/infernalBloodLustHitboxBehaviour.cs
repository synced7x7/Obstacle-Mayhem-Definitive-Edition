using UnityEngine;

public class infernalBloodLustHitboxBehaviour : MonoBehaviour
{
    [SerializeField] private string targetLayerName = "Kyo"; // The name of the layer for collision detection
    private int targetLayerMask;
    [SerializeField] Kyo_spriteActivator sprite;
    [SerializeField] Kyo_data kyo_data;
    [SerializeField] private SlowMotionEffect slowMotionEffect;
    [SerializeField] private KnockbackPhysicsKKyo knockBack;
    private Animator anim;


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
        //the infernal blood lust chain is activated here after devouring kyo... dont forget!!!
        if (collision.gameObject.layer == targetLayerMask)
        {
            if (!kyo_data.kyo_interval)
            {
                anim = Iori_main.Instance.getAnim();
                Iori_animationEndController.Instance.StopAllCoroutines();
                Kyo_main.Instance.changeSpriteColor(Color.black);
                MapBaseBehaviour.Instance.MapFadeIN(fadeDuration: 0.1f, targetColor: Color.black);
                Iori_main.Instance.resetIoriVelocityAndPosition();
                sprite.customAnimation(Kyo_spriteActivator.customAnimationRunner.face_hurt); //kyo sprite activator
                sprite.PauseAnimation(4f);//kyo animation pause
                anim.SetBool("Infernal Blood Lust Chain", true);
                Debug.Log("Infernal Blood Lust devoured kyo");
            }
        }
    }
}
