
using Unity.VisualScripting;
using UnityEngine;

public class Iori_main : MonoBehaviour
{
    public static Iori_main Instance { get; private set; }
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Animator anim;

    private Transform position;
    public Rigidbody2D Body => body;
    public Animator Anim => anim;
    [SerializeField] Iori_physicsHandler physics;
    [SerializeField] Iori_data data;
    [SerializeField] ioriFlippedPhysicsHandler flippedPhysics; [HideInInspector]

    // Define the maximum time allowed for an action
    public float maxActionTime = 1.5f; [HideInInspector] // Maximum time in seconds 
    public float actionTimer = 0.0f; [HideInInspector] // Timer to track elapsed time


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
        if (physics == null) physics = GetComponent<Iori_physicsHandler>();
        position = GetComponent<Transform>();
    }

    private void Update()
    {
        if (Character_Base_Behaviour.Instance.ActiveCharacterIori)
        {
            if (!data.ioriFlipped)
                physics.xy_physics();
            else
                flippedPhysics.xy_physics();
            actionDebugger();
        }
        if(data.iori_hit!=0)
        {
            KyoHitTimerReset();
        }
    }

    private void actionDebugger()
    {
        if (data.action)
        {
            // Increment the timer
            actionTimer += Time.deltaTime;

            // Reset action if the timer exceeds the maximum time
            if (actionTimer >= maxActionTime)
            {
                actionTimer = 0f;  // Reset the timer
                data.action = false; // Reset the action
                Debug.Log("Action reset due to time limit.");
            }
        }
    }
    public Transform getIoriPosition()
    {
        return position;
    }
    public void setIoriPositionZ(float positionZ)
    {
        position.position = new Vector3(position.position.x, position.position.y, positionZ);
    }
    public Animator getAnim()
    {
        return anim;
    }
    public void resetIoriVelocityAndPosition()
    {
        body.linearVelocityX = 0f;
        body.linearVelocityY = 0f;
        body.position = new Vector2(body.position.x, 1.48f);
    }

    public Vector3 getIoriPos()
    {
        return position.position;
    }

    public void KyoHitTimerReset()
    {
        if (data.iori_hit>0 && (Time.time - data.last_hit_time >= data.max_hit_time))
        {
            data.iori_hit = 0; // Reset the hit state
            //Debug.Log("Kyo hit state reset after max hit time.");
        }
    }

}
