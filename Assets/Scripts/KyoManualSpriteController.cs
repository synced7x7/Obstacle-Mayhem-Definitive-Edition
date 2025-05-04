using UnityEngine;
using System.Collections;
using System.Data.Common;

public class KyoManualSpriteController : MonoBehaviour
{
    public static KyoManualSpriteController Instance { get; private set; }
    [SerializeField] private Kyo_data k_data;
    private Rigidbody2D body;
    [SerializeField] private Animator anim;
    [SerializeField] private Kyo_main kyoMain;
    [SerializeField] private Iori_data i_data;
    private Animator ioriAnim;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // Use references from Iori_main
        if (kyoMain == null) kyoMain = GetComponent<Kyo_main>();
        body = kyoMain.Body;
        anim = kyoMain.Anim;
    }


    private void manualFallSpeedController()
    {

        Animator ioriAnime = Iori_main.Instance.getAnim();
        if (!ioriAnime.GetBool("Shattered_Flowers"))
        {
            if (!Iori_data.Instance.ioriFlipped)
                body.linearVelocity = new Vector2(16f, body.linearVelocity.y);
            else
                body.linearVelocity = new Vector2(-16f, body.linearVelocity.y);
        }
    }
    private void manualSpeedStopper()
    {
        body.linearVelocity = new Vector2(0f, 0f);
    }

    public void PauseAnimationFace() //holds the hurt animation using animation event
    {
        ioriAnim = Iori_main.Instance.getAnim();
        if (i_data.fangsOfTheInferno)
        {
            Debug.Log("Paused animation for fangs of the inferno.");
            anim.speed = 0;
            StartCoroutine(ResumeAnimationAfterDelay(1f));
        }
    }
    #region riot of the blood, shatterred flowers kyo response
    public void AnimationResponsePauseRiotOfTheBlood(float duration) //may be used by other moves !!! //also use by devotion to the flame to stop kyo movement //shifted to KyoSpriteActivator for conveniency
    {
        //Debug.Log("kyo animation set to false");
        anim.speed = 0;
        StartCoroutine(ResumeAnimationAfterDelay(duration));
    }
    private void shatterredFlowersController()
    {
        Animator ioriAnime = Iori_main.Instance.getAnim();
        if (ioriAnime.GetBool("Shattered_Flowers"))  //shatterred flowers kyo fall anim response
        {
            if (!Iori_data.Instance.ioriFlipped)
                body.linearVelocity = new Vector2(8f, body.linearVelocity.y);
            else
                body.linearVelocity = new Vector2(-8f, body.linearVelocity.y);
            Debug.Log("AnimationPaused for shattered flowers");
            animatorSpeedController(2f, 0f); // duration, speed
        }
    }


    #endregion

    public void animatorSpeedController(float duration, float speed)
    {
        anim.speed = speed;
        StartCoroutine(ResumeAnimationAfterDelay(duration));
    }

    private IEnumerator ResumeAnimationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log(("Animation Resumed"));
        anim.speed = 1;
    }


    public void StartRiotOfTheBloodAdjustment(float movementDuration)
    {
        Transform kyoTransform = this.transform;
        Transform ioriTransform = Iori_main.Instance.getIoriPosition();
        StartCoroutine(riotofTheBloodCustomAdjuster(kyoTransform, ioriTransform, movementDuration));
    }
    private IEnumerator riotofTheBloodCustomAdjuster(Transform kyoTransform, Transform ioriTransform, float duration)
    {
        // Store the starting position of Kyo
        Vector3 startPosition = kyoTransform.position;

        // Determine the target position (Iori's position)
        Vector3 targetPosition;
        if (!Iori_data.Instance.ioriFlipped)
            targetPosition = new Vector3(ioriTransform.position.x + 1.5f, ioriTransform.position.y, ioriTransform.position.z);
        else
            targetPosition = new Vector3(ioriTransform.position.x - 1.5f, ioriTransform.position.y, ioriTransform.position.z);

        // Calculate the total time elapsed
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate the interpolation factor (normalized time between 0 and 1)
            float t = elapsedTime / duration;

            // Linearly interpolate Kyo's position towards Iori's position
            kyoTransform.position = Vector3.Lerp(startPosition, targetPosition, t);

            // Wait until the next frame
            yield return null;
        }

        // Ensure Kyo's position is exactly at Iori's position after the duration
        kyoTransform.position = targetPosition;
    }


    #region action

    #region Roll
    private void RollController1(int num)
    {
        if (!Kyo_data.Instance.kyoFlipped)
            num *= -1;
        if (num == 1)
            body.linearVelocityX = 18f;
        else if (num == -1)
            body.linearVelocityX = -15f;
    }
    private void RollController2()
    {
        Kyo_data.Instance.interval = false;
        Kyo_physicsHandler.Instance.isLocomotionEnabled = false;
        kyo_flippedPhysicsHandler.Instance.isLocomotionEnabled = false;
        manualSpeedStopper();
    }
    #endregion

    #region PunchController
    private void PunchController()
    {
        if (Kyo_data.Instance.kyoFlipped)
            body.linearVelocityX = 3f;
        else
            body.linearVelocityX = -3f;
    }

    #endregion

    #region Tornado Twist

    private void TornadoTwistController()
    {
        body.linearVelocity = new Vector2(6f, 15f);
    }
    private void TornadoTwistEndController()
    {
        Debug.Log("End Controller");
        body.linearVelocityX = 0f;
        Kyo_physicsHandler.Instance.kyoGravityController(-30f, -40f);
    }
    #endregion
    
    #endregion




}
