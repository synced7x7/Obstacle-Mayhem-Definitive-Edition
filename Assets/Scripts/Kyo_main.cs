using System.Data.Common;
using UnityEngine;

public class Kyo_main : MonoBehaviour
{
    [SerializeField] Kyo_physicsHandler Physics_handler;
    [SerializeField] kyo_flippedPhysicsHandler FlippedPhysicsHandler;

    public Rigidbody2D body;
    public Animator anim;
    private Transform position;
    public Rigidbody2D Body => body;
    public Animator Anim => anim;
    [SerializeField] KyoTimerReset Timer;
    public static Kyo_main Instance { get; private set; }
    private SpriteRenderer spriteRenderer;
    private Color genuineColor;


    public void Awake()
    {
        #region instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        #endregion
        // Initialize if not assigned in Inspector
        if (body == null) body = GetComponent<Rigidbody2D>();
        if (anim == null) anim = GetComponent<Animator>();
        position = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        genuineColor = spriteRenderer.color;
    }

    private void Update()
    {
        Timer.KyoHitTimerReset();
        if (!Character_Base_Behaviour.Instance.ActiveCharacterIori)
        {
            if (Kyo_data.Instance.kyoFlipped)
                Physics_handler.Physics();
            else
                FlippedPhysicsHandler.Physics();
        }


    }
    public Transform getKyoPosition()
    {
        return position;
    }
    public void setAnimation(int num)
    {
        switch (num)
        {
            case 1:
                anim.SetBool("Fall static", false);
                Debug.Log("Fall static has been set to " + anim.GetBool("Fall static"));
                break;
            default:
                Debug.Log("Invalid num");
                break;
        }

    }

    public Animator getAnim()
    {
        return anim;
    }
    public void setTransformPos(Vector3 newPos, float offsetX, float offsetY, float offsetZ)
    {
        position.position = new Vector3(newPos.x + offsetX, position.position.y + offsetY, position.position.z + offsetZ);
        //Debug.Log("Kyo position set to" + position.position);
    }

    public void KyoMirror()
    {
        Vector3 scale = position.localScale;
        scale.x *= -1; // Flipping along the X-axis for a mirror effect
        position.localScale = scale;
    }

    public void changeSpriteColor(Color color)
    {
        spriteRenderer.color = color;
    }
    public void resetSpriteColor()
    {
        spriteRenderer.color = genuineColor;
    }


}
