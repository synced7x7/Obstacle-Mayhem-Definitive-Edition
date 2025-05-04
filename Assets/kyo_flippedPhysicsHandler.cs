using UnityEngine;
using System.Collections;

public class kyo_flippedPhysicsHandler : MonoBehaviour
{
    public static kyo_flippedPhysicsHandler Instance { get; private set; }
    [SerializeField] private Kyo_spriteActivator SA;
    private Rigidbody2D kyoBody;
    private Transform kyoPosition;
    [SerializeField] Kyo_data data;
    [SerializeField] private float gravity = -45f; // Simulated gravity value
    [SerializeField] private float maxFallSpeed = -50f; // Maximum downward velocity
    public bool isLocomotionEnabled;
    [SerializeField] Kyo_keyHandler Keys;
    private Animator anim;


    private bool isGravityActive = false; // Control when gravity should act

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        kyoPosition = GetComponent<Transform>();
        kyoBody = GetComponent<Rigidbody2D>();
        isLocomotionEnabled = false;
        anim = GetComponent<Animator>();
    }

    public void kyoGravityController()
    {
        if (!isGravityActive)
        {
            isGravityActive = true;
            StartCoroutine(ApplyConditionalGravity());
        }
    }

    private IEnumerator ApplyConditionalGravity()
    {
        float fallSpeed = 0f; // Initialize fall speed

        while (isGravityActive && !Kyo_data.Instance.kyoGrounded)
        {
            // Apply gravity
            fallSpeed += gravity * Time.deltaTime;
            fallSpeed = Mathf.Max(fallSpeed, maxFallSpeed); // Clamp fall speed

            // Apply movement
            kyoBody.linearVelocity = new Vector2(kyoBody.linearVelocity.x, fallSpeed);

            yield return null;
        }
        kyoPosition.position = new Vector3(kyoPosition.position.x, 1.48f, kyoPosition.position.z);
        // Reset fall speed when grounded
        if (Kyo_data.Instance.kyoGrounded)
        {
            fallSpeed = 0f;
            kyoBody.linearVelocity = new Vector2(kyoBody.linearVelocity.x, 0f);
        }
        isGravityActive = false;
        Iori_main.Instance.setIoriPositionZ(-0.09f);
    }
    public void Physics()
    {
        if (!data.kyoGrounded && transform.position.x > 10f)
            kyoGravityController();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Keys.HandleLeftKeyPressIndependent();
        if (Input.GetKeyDown(KeyCode.RightArrow))
            Keys.HandleRightKeyPressIndependent();
        if (!data.interval)
        {
            #region Locomotion

            if (data.leftKeyPressCount_independent == 2)
            {
                if (!isLocomotionEnabled)
                    SA.BasicMovementActvator(Kyo_spriteActivator.Movement.Run);
                isLocomotionEnabled = true;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && data.leftKeyPressCount_independent == 1)
            {
                if (!isLocomotionEnabled)
                    SA.BasicMovementActvator(Kyo_spriteActivator.Movement.Right);
                isLocomotionEnabled = true;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (!isLocomotionEnabled)
                    SA.BasicMovementActvator(Kyo_spriteActivator.Movement.Left);
                isLocomotionEnabled = true;
            }

            if (((Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)) && !anim.GetBool("Sit")) || (Input.GetKeyUp(KeyCode.DownArrow) && anim.GetBool("Sit"))) //idle //has to ensure that my sit animation doesnt get overwritten when pressed right or left
            {
                if (isLocomotionEnabled)
                {
                    isLocomotionEnabled = false;
                    SA.BasicMovementActvator(Kyo_spriteActivator.Movement.Idle);
                }
            }
            #region jump

            if (Input.GetKeyDown(KeyCode.Space) && data.kyoGrounded) //&& !data.isbackjumping && !data.isdodging //jump
            {
                SA.BasicMovementActvator(Kyo_spriteActivator.Movement.Jump);
            }
            else if (!data.kyoGrounded && Input.GetKeyDown(KeyCode.A) && transform.position.y > 5.0f)
            {
                SA.MovesetActivator(Kyo_spriteActivator.ActionMov.JumpPunch);
            }
            else if (!data.kyoGrounded && Input.GetKeyDown(KeyCode.S) && transform.position.y > 5.0f)
            {
                SA.MovesetActivator(Kyo_spriteActivator.ActionMov.JumpKick);
            }

            #endregion
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.RightShift) && data.kyoGrounded) //front roll
            {
                SA.BasicMovementActvator(Kyo_spriteActivator.Movement.frontRoll);
                return;
            }
            else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.RightShift) && data.kyoGrounded)
            {
                SA.BasicMovementActvator(Kyo_spriteActivator.Movement.backDodge);
                return;
            }
            else if (Input.GetKey(KeyCode.DownArrow) && data.kyoGrounded)
            {
                if (!isLocomotionEnabled)
                    SA.BasicMovementActvator(Kyo_spriteActivator.Movement.sit);
                #region Sit Action

                else if (isLocomotionEnabled && Input.GetKeyDown(KeyCode.A) && anim.GetBool("Sit"))
                {
                    SA.MovesetActivator(Kyo_spriteActivator.ActionMov.SitPunch);
                }

                else if (isLocomotionEnabled && Input.GetKeyDown(KeyCode.S) && anim.GetBool("Sit"))
                {
                    SA.MovesetActivator(Kyo_spriteActivator.ActionMov.SitKick);
                }

                #endregion

                isLocomotionEnabled = true;
            }

            #endregion

            #region punch

            else if (Input.GetKeyDown(KeyCode.A) && data.kyoGrounded)
            {
                SA.MovesetActivator(Kyo_spriteActivator.ActionMov.Punch);
                isLocomotionEnabled = false;
                return;
            }

            #endregion

            #region Kick

            else if (Input.GetKeyDown(KeyCode.S) && data.kyoGrounded)
            {

                SA.MovesetActivator(Kyo_spriteActivator.ActionMov.Kick);
                isLocomotionEnabled = false;
                return;
            }

            #endregion
        }
    }


}
