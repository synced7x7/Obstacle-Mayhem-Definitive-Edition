using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using System.Data.Common;
using UnityEngine;
using System.Runtime.InteropServices;

public class Iori_spriteActivator : MonoBehaviour
{
    public static Iori_spriteActivator Instance { get; private set; }
    [SerializeField] Iori_data data;
    private Rigidbody2D body;
    private Animator anim;
    [SerializeField] private Iori_main ioriMain;
    [SerializeField] private SlowMotionEffect slowMotionEffect;
    [SerializeField] private ColoredFlash coloredFlash;


    public enum InTransitionmoves
    {
        fangsOfTheInferno_shiki,
        riotOfTheBlood
    }

    public enum HurtReaction
    {
        Mid,
        Face,
        Fall
    }

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
        // Use references from Iori_main
        if (ioriMain == null) ioriMain = GetComponent<Iori_main>();
        body = ioriMain.Body;
        anim = ioriMain.Anim;
    }
    public void iori_sprite_activator(int num)
    {
        ioriMain.actionTimer = 0f;
        reset_sprite(); // Reset all sprites before activating the new one

        switch (num)
        {
            case 1:
                // Idle state
                body.linearVelocityX = 0f;
                break;
            case 2:
                // Right movement
                anim.SetBool("Iori_right_mov2", true);
                if (!data.ioriFlipped)
                    data.leftKeyPressCount = 1;
                else
                    data.rightKeyPressCount = 1;
                break;
            case 3:
                // Left movement
                anim.SetBool("Iori_left3", true);
                if (!data.ioriFlipped)
                    data.rightKeyPressCount = 1;
                else
                    data.leftKeyPressCount = 1;
                //Debug.Log("Leftmov is being called");
                break;
            case 4:
                // Jump
                anim.SetTrigger("jumpTrigger");
                data.isjumping = true;
                break;
            case 5:
                // Running
                float horizontal_input = Input.GetAxis("Horizontal");
                anim.SetBool("iori_run6", true);
                if (!data.isjumping)
                {
                    if (!data.ioriFlipped)
                    {
                        if (!data.isCollidingPlayer)
                            body.linearVelocityX = 15f;
                        else
                            body.linearVelocityX = 4.5f;
                    }
                    else
                    {
                        if (!data.isCollidingPlayer)
                            body.linearVelocityX = -15f;
                        else
                            body.linearVelocityX = -4.5f;
                    }

                }
                else
                    body.linearVelocity = new Vector2(horizontal_input * 10, body.linearVelocity.y); //speed while run + jump
                if (!data.ioriFlipped)
                    data.leftKeyPressCount = 1;
                else
                    data.rightKeyPressCount = 1;
                //Debug.Log("run animation is set");
                break;
            case 7:
                body.linearVelocity = new Vector2(0f, body.linearVelocity.y);
                anim.SetBool("iori_sit7", true);
                //Debug.Log("sit " + anim.GetBool("iori_sit7"));
                data.rightKeyPressCount = 1;
                break;
            case 8:
                anim.SetBool("iori_backjump8", true);
                data.interval = true;
                //data.rightKeyPressCount = 1;
                if (!data.ioriFlipped)
                    body.linearVelocity = new Vector2(-8f, body.linearVelocity.y);
                else
                    body.linearVelocity = new Vector2(8f, body.linearVelocity.y);
                break;
            case 9:
                if (!data.ioriFlipped)
                    body.linearVelocity = new Vector2(11f, body.linearVelocity.y);
                else
                    body.linearVelocity = new Vector2(-11f, body.linearVelocity.y);
                //Debug.Log("dodging velo = " + body.velocity.x);
                anim.SetBool("iori_frontroll9", true);
                break;
            case 10:
                if (!data.ioriFlipped)
                    body.linearVelocity = new Vector2(-11f, body.linearVelocity.y);
                else
                    body.linearVelocity = new Vector2(11f, body.linearVelocity.y);
                //Debug.Log("dodging velo = " + body.velocity.x);
                anim.SetBool("iori_backroll10", true);
                break;
            default:
                Debug.LogError("Invalid sprite number passed to iori_sprite_activator");
                break;
        }
    }
    public void basic_combo(int combo_num)
    {
        ioriMain.actionTimer = 0f;

        reset_sprite();
        switch (combo_num)
        {
            case 1: ////basic punch 1
                data.action = true;
                body.linearVelocity = new Vector2(0f, body.linearVelocity.y);
                anim.SetBool("first_move", true);
                Debug.Log("first move is set");
                break;
            case 2: //basic punch 2
                data.action = true;
                anim.SetBool("second_move", true);
                Debug.Log("second move is set");
                body.linearVelocity = new Vector2(0f, body.linearVelocity.y);
                break;
            case 3: //basic punch 3
                anim.SetBool("third_mov", true);
                data.action = true;
                Debug.Log("...third move is set to :" + anim.GetBool("third_mov"));
                break;
            case 4: //basic punch 4
                anim.SetBool("fourth_move", true);
                data.action = true;
                Debug.Log("fourth move is set");
                break;
            case 5: //basic kick 1
                body.linearVelocity = new Vector2(0f, body.linearVelocity.y);
                anim.SetBool("firstKick", true);
                data.action = true;
                Debug.Log("first kick is set");
                break;
            case 6: //basic kick 2
                anim.SetBool("secondKick", true);
                Debug.Log("second kick is set");
                break;
            case 7: //basic kick 3
                anim.SetBool("thirdKick", true);
                Debug.Log("third kick is set");
                break;
            case 8: //heavy _ punch 1 
                body.linearVelocity = new Vector2(0f, body.linearVelocity.y);
                anim.SetBool("heavy_punch1", true);
                data.action = true;
                Debug.Log("first heavy punch is set");
                break;
            case 9:
                anim.SetBool("heavy_punch2", true);
                Debug.Log("second heavy punch is set");
                break;
            case 10:
                anim.SetBool("heavy_punch3", true);
                Debug.Log("third heavy punch is set");
                break;
            case 11:
                anim.SetBool("iori_run6", true);
                data.interval = true;
                //Debug.Log("run animation is set to " + anim.GetBool("iori_run6"));
                //Debug.Log("shinigami is set");
                break;
            case 12:
                body.linearVelocity = new Vector2(0f, body.linearVelocity.y);
                data.interval = true;
                anim.SetBool("basic_shinigami", true);
                Debug.Log("Basic shinigami is set");
                break;
            case 13:
                data.interval = true;
                anim.SetBool("around_the_world", true);
                Debug.Log("Around the world is set");
                break;
            case 14:
                body.linearVelocity = new Vector2(0f, body.linearVelocity.y);
                data.interval = true;
                anim.SetBool("riot_of_the_blood", true);
                Debug.Log("riot of the blood");
                slowMotionEffect.TriggerSlowMotion(0.45f, 0.3f);
                MapBaseBehaviour.Instance.MAPFadeINFadeOutBasedOnTime(timeBetweenFade: 0.3f, fadeDuration: 0.1f, Color.black);
                break;
            case 15:
                body.linearVelocity = new Vector2(0f, body.linearVelocity.y);
                basic_combo(14);
                anim.SetBool("Blood Blossom of the Oblivion", true);
                Debug.Log("Blood Blossom of the Oblivion");
                break;
            case 16:
                Special_effect_behaviour.Instance.specialEffect1Activator(0.6f, -1f);
                SlowMotionEffect.Instance.TriggerSlowMotion(0.5f, 0.2f);
                //CameraShake.Instance.ShakeCamera(10f, 0.1f); // camera shake
                body.linearVelocity = new Vector2(0f, body.linearVelocity.y);
                //data.devotion_to_the_inferno = true;
                anim.SetBool("Devotion to the Inferno", true);
                Debug.Log("Devotion to the Inferno");
                MapBaseBehaviour.Instance.MAPFadeINFadeOutBasedOnTime(timeBetweenFade: 0.3f, fadeDuration: 0.1f, Color.black);
                break;
            case 17:
                body.linearVelocity = new Vector2(0f, body.linearVelocity.y);
                data.interval = true;
                anim.SetBool("Devotion to the Inferno", false);
                anim.SetTrigger("Flame Ritual");
                Debug.Log("Flame Ritual");
                break;
            case 18:
                body.linearVelocity = new Vector2(0f, 0f);
                specialEffect4Behaviour.Instance.specialEffect4Activator();
                SlowMotionEffect.Instance.TriggerSlowMotion(0.5f, 0.2f);
                CameraShake.Instance.ShakeCamera(5f, 0.1f); // camera shake
                data.interval = true;
                anim.SetBool("Infernal Blood Lust", true);
                Debug.Log("Infernal Blood Lust");

                break;
            default:
                Debug.LogError("Invalid combo number.");
                break;
        }

    }
    public void Additional_Move(int num)
    {
        ioriMain.actionTimer = 0f;
        switch (num)
        {
            case 1:
                anim.SetTrigger("Jump Punch");
                Debug.Log("Jump punch");
                break;
            case 2:
                anim.SetBool("Jump Kick", true);
                Debug.Log("Jump Kick");
                break;
            case 3:
                data.movementFlag = true;
                anim.SetBool("Sit Punch", true);
                Debug.Log("Sit Punch is set");
                break;
            case 5:
                data.movementFlag = true;
                anim.SetBool("Sit Kick", true);
                Debug.Log("Sit Kick is set");
                break;
            case 6:
                data.movementFlag = true;
                anim.SetBool("Sit Kick Chain", true);
                Debug.Log("Sit Kick Chain is set");
                break;
            default:
                Debug.LogError("Invalid mov num");
                break;
        }
    }

    public void InTransitionMovesActivator(InTransitionmoves Tmoves)
    {
        ioriMain.actionTimer = 0f;
        switch (Tmoves)
        {
            case InTransitionmoves.fangsOfTheInferno_shiki:
                body.linearVelocityX = 0f;//y coordinate placement is necessary and will be added later during kyoAI
                data.fangsOfTheInferno = true;
                anim.SetBool("intermediate_shinigami", true);
                Debug.Log("Fangs of the Inferno");
                break;
            case InTransitionmoves.riotOfTheBlood:
                anim.Play("riot_of_the_blood_part2");
                break;
            default:
                Debug.LogError("Invalid mov num");
                break;
        }

    }

    public void HurtReactionActivator(HurtReaction hurtReaction, float intensity, float time, Color color, float duration)
    {
        ioriMain.actionTimer = 0f;
        switch (hurtReaction)
        {
            case HurtReaction.Mid:
                anim.SetTrigger("Hurt Mid");
                CameraShake.Instance.ShakeCamera(intensity, time); // camera shake
                coloredFlash.Flash(color, duration); //flash effect\
                break;
            case HurtReaction.Face:
                anim.SetTrigger("Hurt Face");
                CameraShake.Instance.ShakeCamera(intensity, time); // camera shake
                coloredFlash.Flash(color, duration); //flash effect\
                break;
        }
    }

    public void reset_sprite()
    {
        // Reset all animation states to ensure only the desired one is active
        anim.SetBool("Iori_right_mov2", false);
        anim.SetBool("Iori_left3", false);
        if (anim.GetBool("iori_run6"))
        {
            anim.SetBool("iori_run6", false);
            body.linearVelocityX = 0f;
        }

        anim.SetBool("iori_sit7", false);
        anim.SetBool("iori_backjump8", false);
        anim.SetBool("iori_frontroll9", false);
        anim.SetBool("iori_backroll10", false);
        anim.SetBool("first_move", false);
        anim.SetBool("second_move", false);
        anim.SetBool("third_mov", false);
        anim.SetBool("fourth_move", false);
        anim.SetBool("firstKick", false);
        anim.SetBool("secondKick", false);
        anim.SetBool("thirdKick", false);
    }

    public void PauseAnimation(float duration)
    {
        anim.speed = 0;
        StartCoroutine(ResumeAnimationAfterDelay(duration));
    }
    private IEnumerator ResumeAnimationAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        anim.speed = 1;
    }

}
