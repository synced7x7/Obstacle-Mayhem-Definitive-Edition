using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Net.Http.Headers;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
//All the functions directly or indirectly controlled by animation event (Sensitive script)
public class Iori_animationEndController : MonoBehaviour
{
    public static Iori_animationEndController Instance { get; private set; }
    [HideInInspector]
    [SerializeField] Iori_data data;
    [SerializeField] private Iori_main ioriMain; // Reference to `Iori_main`
    [SerializeField] private Kyo_spriteActivator K_SA;
    private Rigidbody2D body;
    private Animator anim;
    [SerializeField] private impactSpriteActivator impact;
    [SerializeField] private Transform ioriPosition;
    [SerializeField] private SlowMotionEffect slowMotionEffect;
    [SerializeField] private kyoMainTransformScript kyo_main_transform_script;
    [SerializeField] private ColoredFlash coloredFlash;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // Use references from Iori_main
        if (ioriMain == null) ioriMain = GetComponent<Iori_main>();
        body = ioriMain.Body;
        anim = ioriMain.Anim;
        ioriPosition = GetComponent<Transform>();
    }
    /// <summary>
    ///Specific animation timeline controller
    /// </summary>
    public void backJumpcontroller()
    {
        data.isbackjumping = false;
        reset_keypress();
        data.interval = false;
        //Debug.Log("Backjump animation ended. isBackJumping set to false.");
    }

    public void iori_frontroll9_controller()
    {
        if (!data.interval)//for basic roll
        {
            data.isdodging = false;
            reset_keypress();
        }
        else // riot of the blood
        {
            if (!data.isCollidingPlayer)
            {
                speedyanimationBGBehaviour.Instance.speedyAnimationDeactivator(0.1f);
                anim.SetBool("riot_of_the_blood", false);
                anim.SetBool("Blood Blossom of the Oblivion", false);
                data.interval = false;
            }
        }

        // Debug.Log("roll animation ended. isdodging set to false.");
    }
    private void iori_interval_controller()
    {
        data.interval = true;
        //Debug.Log("Interval set to true");
    }
    private void ManualSpeedController()
    {
        if (!Iori_data.Instance.ioriFlipped)
            body.linearVelocity = new Vector2(15f, body.linearVelocity.y);
        else
            body.linearVelocity = new Vector2(-15f, body.linearVelocity.y);
    }
    public void reset_keypress()
    {
        data.rightKeyPressCount = 1;
        data.leftKeyPressCount = 1;
    }

    private void IntermediateShinigamiController()
    {
        body.linearVelocity = new Vector2(0f, body.linearVelocity.y);
    }
    private void aroundtheworldspeedscontroller()
    {
        if (!data.ioriFlipped)
            body.linearVelocity = new Vector2(3f, 15f);
        else
            body.linearVelocity = new Vector2(-3f, 15f);
        data.grounded = false;
    }

    private void riot_of_the_blood_x_frontroll_controller()
    {
        if (data.interval) //riot of the blood speed controller
        {
            if (!data.ioriFlipped)
                body.linearVelocity = new Vector2(25f, body.linearVelocity.y);
            else
                body.linearVelocity = new Vector2(-25f, body.linearVelocity.y);
        }
        //else frontroll mechanism. controlled by the iori_sprite activator
    }
    private void manualSpeedBlocker()
    {
        body.linearVelocity = new Vector2(0f, body.linearVelocity.y);
    }

    #region fangsoftheInferno
    private void fangsOfTheInfernoController1()
    {
        K_SA.hurtaction(Kyo_spriteActivator.Action.hurt, 5f, 0.1f, new Color(0.5f, 0f, 0.5f), 0.1f);
        transform.position = new Vector3(body.position.x, body.position.y, -1.1f); //making iori go front
        slowMotionEffect.TriggerSlowMotion(0.1f, 0.5f);
        if (!Iori_data.Instance.ioriFlipped)
            impact.impactActivator_effect1(ioriPosition, new Vector3(3f, 3f, 0f));
        else
            impact.impactActivator_effect1(ioriPosition, new Vector3(-3f, 3f, 0f));
    }

    private void fangsOfTheInfernoController2()
    {
        K_SA.hurtaction(Kyo_spriteActivator.Action.face_hurt, 5f, 0.1f, new Color(0.5f, 0f, 0.5f), 0.1f);
        kyo_main_transform_script.KyoMainTransformFunction();
        transform.position = new Vector3(body.position.x, body.position.y, -0.09f);
        impact.impactActivator_effect2(ioriPosition, new Vector3(3f, 3f, 0f));
        // Debug.Log("transform = " + ioriPosition.localScale);
    }
    private void fangsOfTheInfernoController3()
    {
        StartCoroutine(ResumeImpactAnimationAfterDelay(0.8f));
        CameraShake.Instance.ShakeCamera(5f, 1f);
        CameraShake.Instance.zoomCamera(5f, 0.4f, 0.2f);
        PauseAnimation(1f);
    }

    private void PauseAnimation(float pauseDuration)
    {
        anim.speed = 0; // Pause the animation
        StartCoroutine(ResumeAnimationAfterDelay(pauseDuration));
    }

    private IEnumerator ResumeAnimationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        anim.speed = 1; // Resume the animation

    }

    private IEnumerator ResumeImpactAnimationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        impact.impactActivator_effectBlaze(ioriPosition, new Vector3(3f, 3f, 0f));
        CameraShake.Instance.ShakeCamera(20f, 0.3f);
    }
    #endregion

    #region darknessSweep
    private void darknessSweepController()
    {
        if (!data.ioriFlipped)
            impact.impactActivator_effect1(ioriPosition, new Vector3(3f, 3f, 0f));
        else
            impact.impactActivator_effect1(ioriPosition, new Vector3(-3f, 3f, 0f));
    }
    private void darknessSweepCOntroller2()
    {
        DarknessSweepEffectBehaviour.Instance.impactActivator_darknessSweepEffect(ioriPosition);
    }
    #endregion

    #region riot of the blood 
    private void riotOfTheBloodSpcialEffect1Controller()
    {
        if (!anim.GetBool("Blood Blossom of the Oblivion"))
            Special_effect_behaviour.Instance.specialEffect1Activator(0f, 0f);
        else
        {
            SpecialEffect2Behaviour.Instance.specialEffect2Activator();
        }
    }
    private void riotOfTheBloodKyoResponseController(float duration)
    {
        KyoManualSpriteController.Instance.AnimationResponsePauseRiotOfTheBlood(duration);
        ioriPosition.position = new Vector3(ioriPosition.position.x, ioriPosition.position.y, -0.11f);//default iori position =  -0.09
    }
    private void RiotoftheBloodCameraController()
    {
        CameraShake.Instance.ShakeCamera(6f, 0.5f); //intensity, duration
        CameraShake.Instance.zoomCamera(6f, 0.15f, 0.05f); //zoomamount, zoominduration, zoomoutduration
    }
    private void riotOfTheBloodKyoCustomPositionAdjuster()
    {
        if (!data.ioriFlipped)
            KyoManualSpriteController.Instance.StartRiotOfTheBloodAdjustment(0.05f);
        else
            KyoManualSpriteController.Instance.StartRiotOfTheBloodAdjustment(-0.05f);
    }
    private void riotOfTheBloodExplodeEffect1Controller()
    {
        if (!anim.GetBool("Shattered_Flowers")) //riot of the blood
        {
            explodeEffect1Behaviour.Instance.explodeEffect1Activator(1.2f, -1.5f);
            if (!anim.GetBool("Blood Blossom of the Oblivion"))
            {
                K_SA.hurtaction(Kyo_spriteActivator.Action.fall, 7.5f, 0.1f, new Color(0.5f, 0f, 0.5f), 1.3f);
                KnockbackPhysicsKKyo.Instance.ApplyKnockback(Vector2.right, 10f, 20f); //knockback
                BurnEffectBehaviour.Instance.BurnEffectActivator(1f, 0, -3.23f);
                BurnEffectBehaviour.Instance.burnEffectFollowUpActivator(1.3f);
            }
            slowMotionEffect.TriggerSlowMotion(1f, 0.1f);
            ioriPosition.position = new Vector3(ioriPosition.position.x, ioriPosition.position.y, -0.09f); //default iori position =  -0.09
        }
        else //shatterred flowers 
        {
            BurnEffectBehaviour.Instance.BurnEffectActivator(3.3f, 0, -3.23f);
            BurnEffectBehaviour.Instance.burnEffectFollowUpActivator(3.3f);
            explodeEffect1Behaviour.Instance.explodeEffect1Activator(1.2f, -1.5f);
            K_SA.hurtaction(Kyo_spriteActivator.Action.fall, 7.5f, 0.1f, new Color(0.5f, 0f, 0.5f), 3.3f);
            slowMotionEffect.TriggerSlowMotion(1f, 0.1f);
            KnockbackPhysicsKKyo.Instance.ApplyKnockback(Vector2.right, 6f, 20f); //knockback
        }
        MapBaseBehaviour.Instance.ColoredFlashMAPAlphaActivator(Color.red, 0.05f); //flash effect
    }


    private void ShatterredFlowersxEffectBlaze2()
    {
        effectBlaze2Behaviour.Instance.effectBlaze2Activator(offsetX: 5f, offsetY: 5f);
    }

    private void Shattered_FlowersSpecialEffect2Activator()
    {
        if (anim.GetBool("Shattered_Flowers"))
        {
            //shatterred flowers animation starts here
            MapBaseBehaviour.Instance.ColoredFlashMAPActivator(Color.white, 0.1f);
            speedyanimationBGBehaviour.Instance.speedyAnimationActivator();
            shatterredFlowersSPBehaviour.Instance.StartBackgroundMove(startPosition: ioriPosition.position.x - 10f, endPosition: ioriPosition.position.x + 20f);
            SpecialEffect2Behaviour.Instance.specialEffect2Activator();
            slowMotionEffect.TriggerSlowMotion(1f, 0.1f);
        }

    }

    private void shatteredFlowersSpecialEffect3Controller()
    {
        specialEffect3Behaviour.Instance.specialEffect3Activator();//shei shei onek dur chole aschchi//working from 10am to 8.42PM at a stretch still going //idk why i am so obsessed. i need to stop. 
        //football kehlte o jai nai. game dev ki football er thekeo important hoye gelo naki // but i need to do this , no room for excuses.
    }

    #endregion

    #region bloodBlossomoftheOblivion

    private void BBOKyoresponseController()
    {
        K_SA.hurtaction(Kyo_spriteActivator.Action.fall_static, 5f, 0.1f, new Color(0.5f, 0f, 0.5f), 0.1f);
        kyoMainTransformScript.Instance.BloosBlossomOfTheOblivionKyoBehaviour();
    }

    private void BBOEffectActivator()
    {
        explodeEffectHyperBehaviour.Instance.explodeEffect2Activator(offsetX: -0.9f, offsetY: 2.2f, offsetZ: 0.05f);
        coloredFlash.Flash(new Color(0.5f, 0f, 0.5f), 0.1f); //flash effect
        CameraShake.Instance.ShakeCamera(15f, 0.1f); // camera shake
        slowMotionEffect.TriggerSlowMotion(0.2f, 0.2f);
        //CameraShake.Instance.zoomCamera(5f, 0.1f, 0.1f);
    }
    private void BBOHyperActivator()
    {
        explodeEffectHyperBehaviour.Instance.explodeEffectHyperActivator(offsetX: -1.68f, offsetY: -2.3f, offsetZ: -0.1f);
        CameraShake.Instance.ShakeCamera(15f, 0.1f); // camera shake
        slowMotionEffect.TriggerSlowMotion(1f, 0.5f);
    }

    #endregion

    private void customCharacteristicsActivator()
    {
        MapBaseBehaviour.Instance.mapActivator(MapBaseBehaviour.map.eclipseOfDespair);
        speedyanimationBGBehaviour.Instance.speedyAnimationDeactivator(0.8f);
        SlowMotionEffect.Instance.TriggerSlowMotion(3f, 0.3f);
        CameraShake.Instance.ShakeCamera(15f, 0.1f); // camera shake
        Debug.Log("slowmotionTriggerred");
    }

    #region devotion to the flame
    private void flameRitualcontroller1()
    {
        devotiontotheFlameEffectBehaviour.Instance.devotiontotheFlameEffectActivator(ioriPosition);
    }

    #endregion

    #region infernal blood lust
    public void infernalBloodLustJumpController()
    {
        if (body == null)
        {
            Debug.LogError("Rigidbody2D reference is null. Cannot perform jump.");
            return;
        }

        // Parameters
        float jumpHeight = 2f;
        float forwardDistance;
        if (!Iori_data.Instance.ioriFlipped)
            forwardDistance = 15f;
        else
            forwardDistance = -15f;
        float jumpDuration = 0.4f;

        StartCoroutine(JumpAndMoveCoroutine(jumpHeight, forwardDistance, jumpDuration));
    }

    private IEnumerator JumpAndMoveCoroutine(float jumpHeight, float forwardDistance, float duration)
    {
        float elapsedTime = 0f;

        Vector2 startPosition = body.position;
        Vector2 targetPosition = new Vector2(startPosition.x + forwardDistance, startPosition.y);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / duration;
            float newX = Mathf.Lerp(startPosition.x, targetPosition.x, t);
            float newY = startPosition.y + jumpHeight * Mathf.Sin(Mathf.PI * t); // Parabolic jump //interesting!!

            body.position = new Vector2(newX, newY);
            if (body.position.x >= 35.3f)
                body.position = new Vector2(35.3f, body.position.y);

            yield return null;
        }

        if (body.position.x >= 35.3f)
            body.position = new Vector2(35.3f, body.position.y);
        else
            body.position = targetPosition;
        anim.SetBool("Infernal Blood Lust", false);
        Debug.Log("Infernal Blood Lust courotine completed.");
    }

    #endregion

    #region Infernal Blood Lust 
    private void infernalBloodLustController0()
    {
        infernalBloodLustSPMainBehaviour.Instance.infernalBloodLustSPMainActivator();
        Time.timeScale = 0f; //time.timescale controls the overall speed of the game. Normal speed  = 1f.
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // Update physics timestep to match new time scale to sync physics with the system scale. important!!!
    }
    private void infernalBloodLustController1()
    {
        MapBaseBehaviour.Instance.resetColor();
        MapBaseBehaviour.Instance.ColoredFlashMAPActivator(Color.red, 0.5f);
        StartCoroutine(waitforDelay(0.2f));
        Iori_main.Instance.setIoriPositionZ(-0.15f);
        K_SA.customAnimation(Kyo_spriteActivator.customAnimationRunner.face_hurt);
        if (!Iori_data.Instance.ioriFlipped)
            Kyo_main.Instance.setTransformPos(newPos: new Vector3(ioriPosition.position.x, ioriPosition.position.y, ioriPosition.position.z), offsetX: 2.8f, offsetY: 0f, offsetZ: 0f);
        else
            Kyo_main.Instance.setTransformPos(newPos: new Vector3(ioriPosition.position.x, ioriPosition.position.y, ioriPosition.position.z), offsetX: -2.8f, offsetY: 0f, offsetZ: 0f);
    }
    private void infernalBloodLustController2()
    {
        MapBaseBehaviour.Instance.resetColor();
        MapBaseBehaviour.Instance.ColoredFlashMAPActivator(Color.red, 0.5f);
        StartCoroutine(waitforDelay(0.2f));
        K_SA.customAnimation(Kyo_spriteActivator.customAnimationRunner.hit_face_dramatic);
        Kyo_main.Instance.KyoMirror();
        if (!Iori_data.Instance.ioriFlipped)
            Kyo_main.Instance.setTransformPos(newPos: new Vector3(ioriPosition.position.x, ioriPosition.position.y, ioriPosition.position.z), offsetX: -2.8f, offsetY: 0f, offsetZ: 0f);
        else
            Kyo_main.Instance.setTransformPos(newPos: new Vector3(ioriPosition.position.x, ioriPosition.position.y, ioriPosition.position.z), offsetX: 2.8f, offsetY: 0f, offsetZ: 0f);
    }

    private void infernalBloodLustController3()
    {
        MapBaseBehaviour.Instance.resetColor();
        MapBaseBehaviour.Instance.ColoredFlashMAPActivator(Color.red, 0.5f);
        StartCoroutine(waitforDelay(0.2f));
        Kyo_main.Instance.KyoMirror();
        K_SA.customAnimation(Kyo_spriteActivator.customAnimationRunner.face_hurt);
        if (!Iori_data.Instance.ioriFlipped)
            Kyo_main.Instance.setTransformPos(newPos: new Vector3(ioriPosition.position.x, ioriPosition.position.y, ioriPosition.position.z), offsetX: 2.8f, offsetY: 0f, offsetZ: 0f);
        else
            Kyo_main.Instance.setTransformPos(newPos: new Vector3(ioriPosition.position.x, ioriPosition.position.y, ioriPosition.position.z), offsetX: -2.8f, offsetY: 0f, offsetZ: 0f);

    }

    private void infernalBloodLustController4()
    {
        if (!MapBaseBehaviour.Instance.intermediateMapFlag)
        {
            MapBaseBehaviour.Instance.mapActivator(MapBaseBehaviour.map.trueEclipseOfSerenity);
        }
        else
            MapBaseBehaviour.Instance.mapActivator(MapBaseBehaviour.map.trueEclipseOfDespair);
        Kyo_main.Instance.resetSpriteColor();
        MapBaseBehaviour.Instance.MapFadeOut(fadeDuration: 1.2f);
        Iori_main.Instance.setIoriPositionZ(-0.09f);
        if (!Iori_data.Instance.ioriFlipped)
            Kyo_main.Instance.setTransformPos(newPos: new Vector3(ioriPosition.position.x, ioriPosition.position.y, ioriPosition.position.z), offsetX: 10f, offsetY: 0f, offsetZ: 0f);
        else
            Kyo_main.Instance.setTransformPos(newPos: new Vector3(ioriPosition.position.x, ioriPosition.position.y, ioriPosition.position.z), offsetX: -10f, offsetY: 0f, offsetZ: 0f);
        K_SA.customAnimation(Kyo_spriteActivator.customAnimationRunner.hit_fall_dramatic);
        K_SA.ResumeAnimation();
    }
    private void infernalBloodLustController5()
    {
        CameraShake.Instance.ShakeCamera(15f, 0.2f); // camera shake
    }

    private IEnumerator waitforDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        MapBaseBehaviour.Instance.MapFadeIN(fadeDuration: 0.2f, Color.black);
    }

    #endregion


}
