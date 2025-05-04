using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Kyo_spriteActivator : MonoBehaviour
{
    public static Kyo_spriteActivator Instance { get; private set; }
    public enum Action
    {
        hurt,
        fall,
        face_hurt,
        fall_static
    }

    public enum Movement
    {
        Idle,
        Left,
        Right,
        Run,
        Jump,
        frontRoll,
        backDodge,
        sit
    }

    public enum ActionMov
    {
        Punch,
        Kick,
        SitPunch,
        SitKick,
        JumpPunch,
        JumpKick, 
        tornadoTwist
    }

    public enum customAnimationRunner
    {
        face_hurt,
        belly_hurt,
        hit_face_dramatic,
        hit_fall_dramatic
    }

    [SerializeField] private Kyo_data Data;
    private Rigidbody2D body;
    private Animator anim;
    [SerializeField] private Kyo_main kyoMain;
    // [SerializeField] private kyoMainTransformScript kyo_main_transform_script;
    [SerializeField] private ColoredFlash coloredFlash;
    //default
    /* public float intensity = 5f;
    public float time = 0.1f; */

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
        if (kyoMain == null) kyoMain = GetComponent<Kyo_main>();
        body = kyoMain.Body;
        anim = kyoMain.Anim;
    }

    public void BasicMovementActvator(Movement mov)
    {
        switch (mov)
        {
            case Movement.Idle:
                body.linearVelocityX = 0f;
                Debug.Log("Idle");
                ResetBasicMovementSprite();
                break;
            case Movement.Left:
                anim.SetBool("RightMov", true);
                if (Data.kyoFlipped)
                    body.linearVelocityX = -6f;
                else
                    body.linearVelocityX = 6f;
                Debug.Log("Left Movement");
                break;
            case Movement.Right:
                anim.SetBool("LeftMov", true);
                if (Data.kyoFlipped)
                    body.linearVelocityX = 6f;
                else
                    body.linearVelocityX = -6f;
                Debug.Log("Right Movement");
                break;
            case Movement.Run:
                if (Data.kyoGrounded)
                    anim.SetBool("Run", true);
                if (Data.kyoFlipped)
                    body.linearVelocityX = 16f;
                else
                    body.linearVelocityX = -16f;
                Debug.Log("Running");
                break;
            case Movement.Jump:
                anim.SetTrigger("Jump");
                Data.kyoGrounded = false;
                body.linearVelocity = new Vector2(body.linearVelocity.x, 15);
                StartCoroutine(Kyo_physicsHandler.Instance.gravityActivatorAfterDelay(0.4f));
                Debug.Log("Jump Activated");
                break;
            case Movement.frontRoll:
                Data.interval = true;
                ResetBasicMovementSprite();
                anim.SetTrigger("Front Roll");
                Debug.Log("Front Roll Activated");
                break;
            case Movement.backDodge:
                Data.interval = true;
                ResetBasicMovementSprite();
                anim.SetTrigger("Back Dodge");
                Debug.Log("Back Dodge Activated");
                break;
            case Movement.sit:
                anim.SetBool("Sit", true);
                Debug.Log("Sitting");
                break;
        }
    }

    public void MovesetActivator(ActionMov actionMov)
    {

        switch (actionMov)
        {
            case ActionMov.Punch:
                ResetBasicMovementSprite();
                body.linearVelocityX = 0f;
                Data.interval = true;
                anim.SetBool("Punch 1", true);
                Debug.Log("Punch activated");
                break;
            case ActionMov.Kick:
                ResetBasicMovementSprite();
                body.linearVelocityX = 0f;
                Data.interval = true;
                anim.SetBool("Kick 1", true);
                Debug.Log("Kick activated");
                break;
            case ActionMov.SitPunch:
                Data.interval = true;
                anim.SetBool("Sit Punch", true);
                Debug.Log("Sit Punch Activated");
                break;
            case ActionMov.SitKick:
                Data.interval = true;
                anim.SetBool("Sit Kick 1", true);
                Debug.Log("Sit Kick Activated");
                break;
            case ActionMov.JumpPunch:
                Data.interval = true;
                anim.SetTrigger("Jump Punch");
                Debug.Log("Jump Punch Activated");
                break;
            case ActionMov.JumpKick:
                Data.interval = true;
                anim.SetTrigger("Jump Kick");
                Debug.Log("Jump Kick Activated");
                break;
            case ActionMov.tornadoTwist:
                ResetBasicMovementSprite();
                body.linearVelocityX = 0f;
                Data.interval = true;
                anim.SetBool("Tornado Twist 1", true);
                Debug.Log("Tornado Twist Activated");
                break;
        }
    }


    private void ResetBasicMovementSprite()
    {
        anim.SetBool("LeftMov", false);
        anim.SetBool("RightMov", false);
        if (anim.GetBool("Run"))
        {
            Data.leftKeyPressCount_independent = 1;
            Data.rightKeyPressCount_independent = 1;
            anim.SetBool("Run", false);
        }
        anim.SetBool("Sit", false);
        // Debug.Log("BasicMovementSprite Resetted");
    }






    public void hurtaction(Action hurt_action, float intensity, float time, Color color, float duration) //new Color(0.5f, 0f, 0.5f) = violet
    {
        switch (hurt_action)
        {
            case Action.hurt:
                anim.SetTrigger("Belly Hit");  //animation set
                CameraShake.Instance.ShakeCamera(intensity, time); // camera shake
                coloredFlash.Flash(color, duration); //flash effect\
                break;

            case Action.fall:
                anim.SetTrigger("Fall Hit");  //animation
                CameraShake.Instance.ShakeCamera(intensity + 10f, time); // camera shake
                coloredFlash.Flash(color, duration); //flash effect
                //Debug.Log("Kyo falling. " + Data.kyo_hit);
                break;
            case Action.face_hurt:
                anim.SetTrigger("Face Hit");  //animation
                coloredFlash.Flash(color, duration); //flash effect
                CameraShake.Instance.ShakeCamera(intensity + 10f, time); // camera shake
                Debug.Log("Face hit");
                break;
            case Action.fall_static:
                anim.SetBool("Fall static", true);
                coloredFlash.Flash(color, duration); //flash effect
                CameraShake.Instance.ShakeCamera(intensity + 10f, time); // camera shake
                Debug.Log("Fall static hit");
                break;
        }
    }

    public void customAnimation(customAnimationRunner customAnimationRunner)
    {
        switch (customAnimationRunner)
        {
            case customAnimationRunner.face_hurt:
                anim.Play("Kyo Hit Face");
                break;
            case customAnimationRunner.belly_hurt:
                anim.Play("Kyo Hit Belly");
                break;
            case customAnimationRunner.hit_face_dramatic:
                anim.Play("Kyo Hit Face Dramatic");
                break;
            case customAnimationRunner.hit_fall_dramatic:
                anim.Play("Kyo Hit Fall Dramatic");
                break;
        }
    }

    public void PauseAnimation(float duration)
    {
        anim.speed = 0f;
        StartCoroutine(ResumeAnimationAfterDelay(duration));
    }
    private IEnumerator ResumeAnimationAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        ResumeAnimation();
    }
    public void PauseAnimation()
    {
        anim.speed = 0f;
    }
    public void ResumeAnimation()
    {
        anim.speed = 1f;
    }







}

