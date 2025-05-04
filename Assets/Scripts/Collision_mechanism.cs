using UnityEngine;

public class Collision_mechanism : MonoBehaviour
{
    #region Variables
    [SerializeField] Iori_data data;
    [SerializeField] Iori_spriteActivator spriteActivator;
    [SerializeField] Iori_animationEndController animationEndController;
    //private Rigidbody2D body;
    private Rigidbody2D body;
    private Animator anim;
    [SerializeField] private Iori_main ioriMain; // Reference to `Iori_main`
    [SerializeField] private Kyo_data kyo_Data;

    private float gravity = -20f; // Simulated gravity value
    private float fallSpeed = 0f;  // Vertical velocity
    private float maxFallSpeed = -23.5f; // Maximum downward velocity
    [SerializeField] private string targetLayerName = "Kyo"; // The name of the layer for collision detection
    private int targetLayerMask;
    private Collider2D ioriCollider;
    [SerializeField] private float adjustSpeed = 20f;
    [SerializeField] private KnockbackPhysicsKKyo knockbackPhysicsKKyo;
    #endregion

    private void Awake()
    {
        // Use references from Iori_main
        if (ioriMain == null) ioriMain = GetComponent<Iori_main>();
        body = ioriMain.Body;
        anim = ioriMain.Anim;

        ioriCollider = GetComponent<Collider2D>();
        /* if (ioriCollider == null)
        {
            Debug.LogError("No Collider2D found on the Iori GameObject!");
        } */

        targetLayerMask = LayerMask.NameToLayer(targetLayerName);

        if (targetLayerMask == -1)
        {
            Debug.LogWarning($"Layer '{targetLayerName}' does not exist. Please check your Layer settings.");
        }
    }

    private void Update()
    {
        //gravity
        if (!anim.GetBool("grounded"))
        {
            fallSpeed += gravity * Time.deltaTime * 1.5f; // Apply gravity
            fallSpeed = Mathf.Max(fallSpeed, maxFallSpeed); // Clamp fall speed

            // Apply precision (rounded to 2 decimal places)
            fallSpeed = Mathf.Round(fallSpeed * 100f) / 100f;  // Round fallSpeed to 2 decimal places

            // Move the Rigidbody2D down
            body.position += new Vector2(0, fallSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //calls one time, only at the ontry point
    {

        /*  if (collision.gameObject.tag == "Side Boundary")
         {
             Debug.Log("position = " + body.position.x);
         } */
        //ground check

        if (collision.gameObject.tag == "Ground")
        {
            data.grounded = true;
            anim.SetBool("grounded", true); // Reset grounded status when landing
            fallSpeed = 0f; // Reset fall speed when grounded
            body.linearVelocity = new Vector2(body.linearVelocity.x, 0);
            //Debug.Log("vertical speed resetted");
            body.position = new Vector2(body.position.x, 1.48f);

            // After landing, check horizontal input to avoid switching to idle if moving
            if (Input.GetAxis("Horizontal") > 0)
            {
                spriteActivator.iori_sprite_activator(2); // Continue with right movement if moving right
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                spriteActivator.iori_sprite_activator(3); // Continue with left movement if moving left
            }
            else
            {
                spriteActivator.iori_sprite_activator(1); // Switch to idle if no horizontal movement
            }

            animationEndController.reset_keypress();
            data.isjumping = false;
            //Debug.Log("collision working");
        }
        if (Character_Base_Behaviour.Instance.ActiveCharacterIori)
        {
            if (collision.gameObject.layer == targetLayerMask)
            {
                data.isCollidingPlayer = true;
                //move: fangs of the inferno
                if (anim.GetBool("iori_run6") && data.interval && data.distanceToKyo < 11.1f) //if theres a change here remeber to make change on collision stay2d
                    spriteActivator.InTransitionMovesActivator(Iori_spriteActivator.InTransitionmoves.fangsOfTheInferno_shiki);
                else if (anim.GetBool("riot_of_the_blood"))
                {
                    spriteActivator.InTransitionMovesActivator(Iori_spriteActivator.InTransitionmoves.riotOfTheBlood);
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (Character_Base_Behaviour.Instance.ActiveCharacterIori)
        {
            if (other.gameObject.layer == targetLayerMask)
            {
                data.isCollidingPlayer = false;
                Rigidbody2D kyoBody = other.gameObject.GetComponent<Rigidbody2D>();
                //Debug.Log("Kyo and iori colliding");
                if (kyoBody != null)
                {
                    if (!knockbackPhysicsKKyo.isKnockbackActive)
                        kyoBody.linearVelocityX = 0f;
                }
                else
                    Debug.LogError("KyoBody null");
            }
        }
    }


    private void OnCollisionStay2D(Collision2D other)
    {
        if (Character_Base_Behaviour.Instance.ActiveCharacterIori)
        {
            #region boundary collsion check
            if (other.gameObject.tag == "Side Boundary")
            {
                //Debug.Log("Colliding with side boundary");
                if (body.position.x <= -56.45f)
                    body.position = new Vector2(-56.45f, body.position.y);
                else if (body.position.x >= 35.3f)
                    body.position = new Vector2(35.3f, body.position.y);
            }
            #endregion
            //Debug.Log("colliding");
            if (other.gameObject.layer == targetLayerMask)
            {
                Rigidbody2D kyoBody = other.gameObject.GetComponent<Rigidbody2D>();
                Collider2D kyoCollider = other.collider;
                HandlePush(kyoBody);
                if (!data.interval)
                {
                    if (!Iori_data.Instance.isdodging)
                        AdjustPositionBasedOnOverlap(kyoCollider);
                    else
                        AdjustPositionBasedOnDodgingOverlap(kyoCollider);

                }
                if (anim.GetBool("iori_run6") && data.interval && data.distanceToKyo < 11.1f && !data.fangsOfTheInferno)
                {
                    spriteActivator.InTransitionMovesActivator(Iori_spriteActivator.InTransitionmoves.fangsOfTheInferno_shiki);
                }

            }
        }
    }

    private void HandlePush(Rigidbody2D kyoBody)
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0f)
        {
            if (anim.GetBool("grounded"))
            {
                if (!Iori_data.Instance.ioriFlipped)
                {
                    if (anim.GetBool("Iori_right_mov2"))
                    {
                        kyoBody.linearVelocityX = horizontalInput * 1.5f; //if you change this change it also in iori Physics handler.  Keyword: horizontal_input
                    }
                    else if (anim.GetBool("iori_run6"))
                    {
                        kyoBody.linearVelocityX = 4.5f; //if you change this change it also in iori Sprite activator. Keyword: horizontal_input
                    }
                }
            }
        }
        else if (horizontalInput < 0f)
        {
            if (anim.GetBool("grounded"))
            {
                if (Iori_data.Instance.ioriFlipped)
                {
                    if (anim.GetBool("Iori_right_mov2"))
                    {
                        kyoBody.linearVelocityX = horizontalInput * 1.5f; //if you change this change it also in iori Physics handler.  Keyword: horizontal_input
                    }
                    else if (anim.GetBool("iori_run6"))
                    {
                        kyoBody.linearVelocityX = -4.5f; //if you change this change it also in iori Sprite activator. Keyword: horizontal_input
                    }
                }
            }
        }
    }
    private void AdjustPositionBasedOnOverlap(Collider2D kyoCollider)
    {
        Bounds ioriBounds = ioriCollider.bounds;
        Bounds kyoBounds = kyoCollider.bounds;

        float kyo_width = (kyoBounds.max.x - kyoBounds.min.x);
        //Debug.Log("kyo_width = " + kyo_width);
        float overlapWidth;
        if (!Iori_data.Instance.ioriFlipped) //controls crossing while jumping
        {
            overlapWidth = (ioriBounds.max.x - kyoBounds.min.x);
            if (overlapWidth > 0.7f) //+- error //might be reset when kyoAI is given // will be adjusted later after mirroring
            {
                //Debug.Log("Overexceeding min bar");
                if (overlapWidth < kyo_width)
                {
                    body.position -= new Vector2(adjustSpeed * Time.deltaTime, 0f);
                }
                else
                {
                    body.position += new Vector2(adjustSpeed * Time.deltaTime, 0f);
                }
            }
        }
        else
        {
            overlapWidth = (kyoBounds.max.x - ioriBounds.min.x);
            //Debug.Log("overlapWidth: " + overlapWidth);
            if (overlapWidth > 0.7f) //+- error //might be reset when kyoAI is given // will be adjusted later after mirroring
            {
                //Debug.Log("Overexceeding min bar");
                if (overlapWidth < kyo_width)
                {
                    body.position -= new Vector2(-adjustSpeed * Time.deltaTime, 0f);
                }
                else
                {
                    body.position += new Vector2(-adjustSpeed * Time.deltaTime, 0f);
                }
            }
        }

        //        Debug.Log("overlap width = " + overlapWidth);



    }
    private void AdjustPositionBasedOnDodgingOverlap(Collider2D kyoCollider)
    {
        Bounds ioriBounds = ioriCollider.bounds;
        Bounds kyoBounds = kyoCollider.bounds;
        float speed = 20f;

        float kyo_width = (kyoBounds.max.x - kyoBounds.min.x);
        //Debug.Log("kyo_width = " + kyo_width);
        if (!Iori_data.Instance.ioriFlipped)
        {
            float overlapWidth = (ioriBounds.max.x - kyoBounds.min.x);
            //Debug.Log("overlap width = " + overlapWidth);

            //Debug.Log("Overexceeding min bar");
            if (overlapWidth <= 0.4f)
            {
                body.position -= new Vector2(speed * Time.deltaTime, 0f);
            }
            else
            {
                body.position += new Vector2(speed * Time.deltaTime, 0f);
            }
        }
        else
        {
            float overlapWidth = (kyoBounds.max.x - ioriBounds.min.x);
            //Debug.Log("overlap width = " + overlapWidth);

            //Debug.Log("Overexceeding min bar");
            if (overlapWidth <= 0.4f)
            {
                body.position -= new Vector2(-speed * Time.deltaTime, 0f);
            }
            else
            {
                body.position += new Vector2(-speed * Time.deltaTime, 0f);
            }
        }

    }

}
