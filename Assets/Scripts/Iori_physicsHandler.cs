using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Iori_physicsHandler : MonoBehaviour
{
    public enum KeyAction
    {
        LeftArrow,
        RightArrow,
        S,
        A
    }
    public float offset = 0.2f;
    [SerializeField] Iori_data data; //#including iori_data.cs //including c header file was a lot easier in sdl2.0 C.
    //sometimes old is gold:(
    [SerializeField] Iori_spriteActivator S_A;
    private Rigidbody2D body;
    private Animator anim;
    [SerializeField] private Iori_main ioriMain;

    // Define the time threshold for devotion_to_the_inferno to transition to Flame Ritual
    private float maxDevotionTime = 1.0f; // Time in seconds before it transitions

    // Inside your Update() method or wherever you are checking input
    private float devotionStartTime = -1f; // Time when devotion started
    private float jumpPunchActivationHeight = 2.5f; // Set to desired height
    private Coroutine resetCoroutine;
    private float deceleration = 0f;
    private float decelerationRate = 5f;
    [SerializeField] private GameObject Kyo;
    [SerializeField] private SlowMotionEffect slowMotion;
    private Transform ioriTransform;

    private void Awake()
    {
        // Use references from Iori_main
        if (ioriMain == null) ioriMain = GetComponent<Iori_main>();
        body = ioriMain.Body;
        anim = ioriMain.Anim;
        ioriTransform = GetComponent<Transform>();
    }

    public void xy_physics()
    {
        float horizontal_input = Input.GetAxis("Horizontal");
        KeyRefresher();
        //Debug.Log("Horizontal input = " + horizontal_input);
        ///<hardcoded manual update> 
        if (data.interval && anim.GetBool("iori_run6") && !anim.GetBool("intermediate_shinigami"))
        {
            body.linearVelocity = new Vector2(Mathf.Max(20f - deceleration, 0f), body.linearVelocity.y);
            deceleration += decelerationRate * Time.deltaTime;

            if (resetCoroutine == null)
            {
                resetCoroutine = StartCoroutine(ResetRunAfterDelay(0.5f));
            }
            return;
        }
        if (!anim.GetBool("grounded") && data.interval)
        {
            Debug.Log("interval due to flipping set to false");
            data.interval = false;
            data.isbackjumping = false;
        }
        ///<end>

        //Debug.Log($"Dodging: {data.isdodging}, Backjumping: {data.isbackjumping}, Grounded: {data.grounded}, Jumping: {data.isjumping}, Action: {data.action}");
        // Debug.Log("Second Move Trigger: " + anim.GetBool("second_move_trigger"));
        //Debug.Log($"Adouble press: {data.keyApressed}");
        ///action
        if (!data.interval)
        {
            if (!data.isbackjumping && !data.isdodging && !data.isjumping)
            {
                if (!data.devotion_to_the_inferno)
                    actionkeyPressTracker();
                //Punch animation //Combo A, A+A, A+A+A, A+A+A+A. Frame measurement chain algorithm is used for the combo.
                if (!anim.GetBool("iori_sit7") && !(data.leftKeyPressCount > 0 && data.keyDownPressCount > 0) && !data.devotion_to_the_inferno && !data.movementFlag)
                {
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        if (!data.action && data.keyAPressCount <= 1)
                        {
                            S_A.basic_combo(1); //basic punch 1
                        }
                        else if (data.action && data.keyAPressCount == 2 && !anim.GetBool("second_move") && !anim.GetBool("third_mov"))
                        {
                            S_A.basic_combo(2); //basic punch 2
                        }
                        else if (data.action && data.keyAPressCount == 3 && anim.GetBool("second_move") && !anim.GetBool("third_mov") && Kyo_data.Instance.kyo_hit > 0)
                        {
                            S_A.basic_combo(3); //basic punch 3
                        }
                        else if (data.action && data.keyAPressCount == 4 && anim.GetBool("third_mov") && !anim.GetBool("fourth_move") && Kyo_data.Instance.kyo_hit > 0)
                        {
                            S_A.basic_combo(4); //basic punch 4
                        }

                        return;
                    }
                    //Kick animation //Combo S, S+S, S+S+S //Frame measurement chain algorithm is used for the combo //Will be interchained later 
                    else if (Input.GetKeyDown(KeyCode.S))
                    {
                        if (!data.action && data.keySPressCount <= 1)
                        {
                            S_A.basic_combo(5); //basick kick 1 
                        }
                        else if (data.action && data.keySPressCount > 1 && !anim.GetBool("secondKick") && anim.GetBool("firstKick"))
                        {
                            S_A.basic_combo(6); //basick kick 2
                        }
                        else if (data.action && data.keySPressCount > 1 && anim.GetBool("secondKick") && !anim.GetBool("thirdKick") && !anim.GetBool("firstKick") && Kyo_data.Instance.kyo_hit > 0)
                        {
                            S_A.basic_combo(7); //basick kick 3
                        }
                    }
                }
                //Combo -- Down Arrow + Left Arrow + A, Down Arrow + Left Arrow + A + Down Arrow + Left Arrow + A, Down Arrow + Left Arrow + A + Down Arrow + Left Arrow + A + Down Arrow + Left Arrow + A 
                else if (data.keyDownPressCount > 0 && !data.movementFlag) // Down Arrow is pressed first
                {
                    // Handle Left Arrow press
                    if (Input.GetKeyDown(KeyCode.LeftArrow) && !data.devotion_to_the_inferno)
                    {
                        ActionManualKeyPressTracker(KeyAction.LeftArrow);
                        data.keyAPressCount = 0; // Reset A key press count since we need a fresh sequence
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow) && !data.devotion_to_the_inferno)
                    {
                        ActionManualKeyPressTracker(KeyAction.RightArrow);
                    }
                    else
                    {
                        if (!data.devotion_to_the_inferno)
                        {
                            // Basic combo sequence checks
                            // Execute first move (Down + Left + A)
                            #region Bloom of Desolation
                            if (data.keyRightPressCount == 0 && data.keyAPressCount > 0 && data.keyLeftPressCount == 1 && !anim.GetBool("heavy_punch1"))
                            {
                                S_A.basic_combo(8);
                            }
                            // Execute second move (Down + Left + A, repeated)
                            else if (data.keyRightPressCount == 0 && data.keyLeftPressCount == 2 && data.keyAPressCount > 0 && anim.GetBool("heavy_punch1") && !anim.GetBool("heavy_punch2") && Kyo_data.Instance.bloom_of_desolation_hit > 0)
                            {
                                S_A.basic_combo(9);
                            }
                            // Execute third move (Down + Left + A, repeated again)
                            else if (data.keyRightPressCount == 0 && data.keyLeftPressCount == 3 && data.keyAPressCount > 0 && !anim.GetBool("heavy_punch1") && anim.GetBool("heavy_punch2") && !anim.GetBool("heavy_punch3") && Kyo_data.Instance.bloom_of_desolation_hit > 0)
                            {
                                S_A.basic_combo(10);
                            }
                            #endregion

                            // Intermediate Shinigami sequence (Right + Down + Left + A) // Fangs of the inferno
                            else if (data.keyLeftPressCount == 2 && data.keyDownPressCount == 2 && data.keySPressCount > 0 && !anim.GetBool("iori_run6"))
                            {
                                slowMotion.TriggerSlowMotion(0.1f, 0.2f);
                                storeDistance();
                                S_A.basic_combo(11);
                                return;
                            }
                            //basic Shinigami (DownArrow + Right Arrow + A)
                            else if (data.keyDownPressCount == 1 && data.keyRightPressCount == 1 && data.keyAPressCount > 0)
                            {
                                S_A.basic_combo(12);
                                return;
                            }
                            //around the world
                            else if (data.keyDownPressCount == 2 && data.keyRightPressCount == 2 && data.keyAPressCount > 0)
                            {
                                S_A.basic_combo(13);
                                return;
                            }
                            //riot of the blood //( down + right + down + left + A )
                            else if (data.keyDownPressCount == 2 && data.keyRightPressCount == 1 && data.keyLeftPressCount == 1 && data.keyAPressCount > 0)
                            {
                                S_A.basic_combo(14);
                                RiotOfTheBloodSPBehaviour.Instance.StartBackgroundMove(startPosition: ioriTransform.position.x - 10f, endPosition: ioriTransform.position.x + 20f);
                                return;
                            }
                            //Blood Blossom of the Oblivion // down + right + down + left + Q
                            else if (data.keyDownPressCount == 2 && data.keyRightPressCount == 1 && data.keyLeftPressCount == 1 && Input.GetKeyDown(KeyCode.Q))
                            {
                                speedyanimationBGBehaviour.Instance.speedyAnimationActivator();
                                bloodBlossomoftheOblivionSPBehaviour.Instance.StartBackgroundMove(startPosition: ioriTransform.position.x - 10f, endPosition: ioriTransform.position.x + 20f);
                                S_A.basic_combo(15);
                                return;
                            }
                        }
                        //Devotion to the Inferno // Down + Left + Down + Right + W
                        if (data.keyDownPressCount == 2 && data.keyRightPressCount == 1 && data.keyLeftPressCount == 1)
                        {
                            if (Input.GetKey(KeyCode.W))
                            {
                                data.devotionTime = Time.time - devotionStartTime;
                                if (!data.devotion_to_the_inferno)
                                {
                                    deovtionToTheFlameSPBehaviour.Instance.StartBackgroundMove(startPosition: ioriTransform.position.x - 10f, endPosition: ioriTransform.position.x + 20f);
                                    data.devotion_to_the_inferno = true;
                                    devotionStartTime = Time.time;
                                    S_A.basic_combo(16);
                                }
                                else
                                {
                                    // Check if the devotion time has exceeded the threshold
                                    if (Time.time - devotionStartTime > maxDevotionTime)
                                    {
                                        // Trigger the Flame Ritual after the max time is exceeded
                                        if (data.devotion_to_the_inferno)
                                        {
                                            S_A.basic_combo(17);
                                            Debug.Log("Flame Ritual (Timeout)");
                                            data.devotion_to_the_inferno = false;
                                            KeyRefresher(); // Reset key states
                                        }
                                    }
                                }
                            }

                            // End devotion and trigger Flame Ritual when the key is released
                            if (Input.GetKeyUp(KeyCode.W) && data.devotion_to_the_inferno)
                            {
                                S_A.basic_combo(17);
                                Debug.Log("Flame Ritual (Key Released)");
                                data.devotion_to_the_inferno = false;
                                KeyRefresher();
                            }
                            return;
                        }
                        //infernal blood lust
                        else if (data.keyLeftPressCount == 2 && data.keyDownPressCount == 2 && Input.GetKeyDown(KeyCode.Q))
                        {
                            speedyanimationBGBehaviour.Instance.speedyAnimationActivator();
                            ionfernalBloodLustSPBehaviour.Instance.StartBackgroundMove(startPosition: ioriTransform.position.x - 10f, endPosition: ioriTransform.position.x + 20f);
                            S_A.basic_combo(18);
                            return;
                        }
                    }
                }
            }

            ///action <end>
            /// 
            ///basic movement
            if (!data.action && !data.devotion_to_the_inferno)
            {
                // Handle sit
                if (Input.GetKey(KeyCode.DownArrow) && !data.isbackjumping && !data.isdodging && !data.isjumping && data.grounded && Mathf.Approximately(body.linearVelocity.x, 0f))
                {
                    //Debug.Log("iori sit set");
                    S_A.iori_sprite_activator(7);
                    //Debug.Log("sit animation activated");
                    if (data.keyAPressCount == 1 && !anim.GetBool("Sit Punch") && !anim.GetBool("Sit Kick") && !anim.GetBool("Sit Kick Chain"))
                    {
                        S_A.Additional_Move(3);
                    }
                    else if (data.keySPressCount == 1 && !anim.GetBool("Sit Kick") && !anim.GetBool("Sit Punch") && !anim.GetBool("Sit Punch Chain"))
                    {
                        S_A.Additional_Move(5);
                    }
                    else if (data.keySPressCount == 2 && anim.GetBool("Sit Kick") && !anim.GetBool("Sit Kick Chain"))
                    {
                        S_A.Additional_Move(6);
                    }
                    return;
                }
                if (!data.movementFlag)
                {
                    #region LR movement velocity (not sprite activator)
                    //Debug.Log("physics working");
                    if (!anim.GetBool("iori_sit7") && data.isbackjumping == false && !data.isdodging)
                    {

                        if (data.rightKeyPressCount < 2 && (anim.GetBool("Iori_right_mov2") || anim.GetBool("Iori_left3"))) //LR movement controlled by input 
                        {
                            if (!data.isCollidingPlayer)
                            {
                                if (anim.GetBool("Iori_right_mov2"))
                                    body.linearVelocity = new Vector2(5, body.linearVelocity.y);
                                else
                                    body.linearVelocity = new Vector2(-5, body.linearVelocity.y);
                            }

                            else
                                body.linearVelocityX = horizontal_input * 1.5f;
                        }
                    }
                    #endregion

                    //jump
                    if (Input.GetKeyDown(KeyCode.Space) && data.grounded && !data.isbackjumping && !data.isdodging)
                    {
                        //                        Debug.Log("Jump working");
                        body.linearVelocity = new Vector2(body.linearVelocity.x, 15); //jump speed
                        data.grounded = false;
                        S_A.iori_sprite_activator(4); // Use 4 to indicate jump
                    }
                    //front_roll
                    else if (Input.GetKey(KeyCode.RightArrow) && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && !data.isdodging && !data.isjumping && !data.isbackjumping)
                    {
                        //Debug.Log("Front roll animation is being called.");
                        data.isdodging = true;
                        S_A.iori_sprite_activator(9);

                    }
                    //backroll
                    else if (Input.GetKey(KeyCode.LeftArrow) && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && !data.isdodging && !data.isjumping && !data.isbackjumping)
                    {
                        //Debug.Log("Back roll animation is being called.");

                        data.isdodging = true;
                        S_A.iori_sprite_activator(10);

                    }

                    //jump punch //Jump + A
                    else if (!data.grounded && Input.GetKeyDown(KeyCode.A) && !data.movePerformed)
                    {
                        float currentHeight = transform.position.y;
                        //Debug.Log("currentHeight = " + currentHeight);
                        if (currentHeight >= jumpPunchActivationHeight)
                        {
                            data.movePerformed = true;
                            S_A.Additional_Move(1);
                        }
                    }
                    //Jump Kick // Jump + S
                    else if (!data.grounded && Input.GetKeyDown(KeyCode.S) && !data.movePerformed)
                    {
                        float currentHeight = transform.position.y;
                        //Debug.Log("currentHeight = " + currentHeight);
                        if (currentHeight >= jumpPunchActivationHeight)
                        {
                            data.movePerformed = true;
                            S_A.Additional_Move(2);
                        }
                    }
                    //move performed bool reset
                    else if (data.grounded && data.movePerformed)
                    {
                        data.movePerformed = false;
                        //Debug.Log("move Performed bool resetted");
                    }




                    //Left, right, backjump, run ,action
                    else if (!data.isdodging)
                    {
                        ///<key press time counter>
                        if (Input.GetKeyDown(KeyCode.RightArrow))
                            HandleRightKeyPress();
                        if (Input.GetKeyDown(KeyCode.LeftArrow) && !data.isbackjumping)
                        {
                            HandleLeftKeyPress();
                        }
                        ///<counter/end>


                        // Handle horizontal movement
                        if (horizontal_input > 0) // Moving right
                        {
                            if (data.rightKeyPressCount >= 2) // Running
                            {
                                S_A.iori_sprite_activator(5);
                            }
                            else
                            {
                                S_A.iori_sprite_activator(2); // Walking right
                            }
                        }
                        else if (horizontal_input < 0) // Moving left
                        {
                            if (data.leftKeyPressCount == 2) // Back jump
                            {
                                if (!anim.GetBool("grounded"))
                                    return;
                                data.isbackjumping = true;
                                S_A.iori_sprite_activator(8);
                            }
                            else
                            {
                                S_A.iori_sprite_activator(3); // Walking left
                            }
                        }
                        else if (horizontal_input == 0) // Idle state
                        {
                            S_A.iori_sprite_activator(1);
                        }

                    }
                }
            }
        }


        anim.SetBool("grounded", data.grounded);
    }

    //Double Key press counter
    private void HandleRightKeyPress()
    {
        if (Time.time - data.lastKeyRightPressTime_independent < 0.2f && data.grounded) // Detect double press within 0.2 seconds
        {
            data.rightKeyPressCount++;
            // Debug.Log("rightkey incremented");
        }
        else
        {
            data.rightKeyPressCount = 1; // Reset the counter if too much time has passed
                                         //Debug.Log("rightkey resetted");
        }

        data.lastKeyRightPressTime_independent = Time.time;
    }

    private void HandleLeftKeyPress()
    {
        if (Time.time - data.lastKeyLeftPressTime_independent < 0.2f && data.grounded) // Detect double press within 0.2 seconds
        {
            data.leftKeyPressCount++;
            //Debug.Log("leftkey incremented");
        }
        else
        {
            data.leftKeyPressCount = 1; // Reset the counter if too much time has passed
                                        //Debug.Log("leftkey resetted");
        }

        data.lastKeyLeftPressTime_independent = Time.time;
    }

    private void actionkeyPressTracker()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Check if the time since the last key press is within the time window
            if (Time.time - data.lastKeyAPressTime <= data.timeWindow)
            {
                data.keyAPressCount++; // Increment count if within the window
            }
            else
            {
                data.keyAPressCount = 1; // Reset count if outside the window
            }

            data.lastKeyAPressTime = Time.time; // Update the last key press time
            //Debug.Log("Key A or S pressed: " + data.keyASPressCount + " times");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (Time.time - data.lastKeySPressTime <= data.timeWindow)
            {
                data.keySPressCount++; // Increment count if within the window
            }
            else
            {
                data.keySPressCount = 1; // Reset count if outside the window
            }

            data.lastKeySPressTime = Time.time; // Update the last key press time
            //Debug.Log("Key A or S pressed: " + data.keyASPressCount + " times");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Time.time - data.lasKeyDownPressTime <= data.timeWindow)
            {
                data.keyDownPressCount++; // Increment count if within the window
            }
            else
            {
                data.keyDownPressCount = 1; // Reset count if outside the window
            }

            data.lasKeyDownPressTime = Time.time; // Update the last key press time
        }
    }

    private void ActionManualKeyPressTracker(KeyAction action)
    {
        switch (action)
        {
            case KeyAction.LeftArrow:
                if (Time.time - data.lasKeyLeftPressTime <= data.timeWindow + offset)
                {
                    data.keyLeftPressCount++; // Increment count if within the window
                }
                else
                {
                    data.keyLeftPressCount = 1; // Reset count if outside the window
                }
                data.lasKeyLeftPressTime = Time.time; // Update the last key press time
                //Debug.Log("Key Left Arrow Pressed : " + data.keyLeftPressCount + " times");
                break;
            case KeyAction.RightArrow:
                if (Time.time - data.lastKeyRightPressTime <= data.timeWindow + offset)
                {
                    data.keyRightPressCount++; // Increment count if within the window
                }
                else
                {
                    data.keyRightPressCount = 1; // Reset count if outside the window
                }
                data.lastKeyRightPressTime = Time.time; // Update the last key press time
                //Debug.Log("Key Left Arrow Pressed : " + data.keyLeftPressCount + " times");
                break;
            case KeyAction.S:
                if (Time.time - data.lastKeySPressTime <= data.timeWindow)
                {
                    data.keySPressCount++; // Increment count if within the window
                }
                else
                {
                    data.keySPressCount = 1; // Reset count if outside the window
                }

                data.lastKeySPressTime = Time.time; // Update the last key press time
                //Debug.Log("Key A or S pressed: " + data.keyASPressCount + " times");
                break;
            case KeyAction.A:
                if (Time.time - data.lastKeyAPressTime <= data.timeWindow)
                {
                    data.keyAPressCount++; // Increment count if within the window
                }
                else
                {
                    data.keyAPressCount = 1; // Reset count if outside the window
                }

                data.lastKeyAPressTime = Time.time; // Update the last key press time
                break;
            default:
                Debug.LogWarning("Unknown Key Action");
                break;
        }
    }
    private void KeyRefresher()
    {
        if (!data.devotion_to_the_inferno)
        {
            // Key A Refresher
            if (Time.time - data.lastKeyAPressTime > data.timeWindow && data.keyAPressCount > 0)
            {
                data.keyAPressCount = 0;
            }
            // Key S Refresher
            if (Time.time - data.lastKeySPressTime > data.timeWindow && data.keySPressCount > 0)
            {
                data.keySPressCount = 0;
            }
            // Key Down Arrow Refresher
            if (Time.time - data.lasKeyDownPressTime > data.timeWindow + offset && data.keyDownPressCount > 0)
            {
                data.keyDownPressCount = 0;
            }
            // Key Left Arrow Refresher
            if (Time.time - data.lasKeyLeftPressTime > data.timeWindow + offset && data.keyLeftPressCount > 0)
            {
                data.keyLeftPressCount = 0;
            }
            // Key Right Refresher 
            if (Time.time - data.lastKeyRightPressTime > data.timeWindow + offset && data.keyRightPressCount > 0)
            {
                data.keyRightPressCount = 0;
            }
        }
    }

    private void storeDistance()
    {
        if (Kyo != null)
        {
            // Calculate distance between Kyo and Iori
            data.distanceToKyo = Vector2.Distance(transform.position, Kyo.transform.position);

            // Log the distance in the console
            Debug.Log("Distance between Kyo and Iori: " + data.distanceToKyo);
        }
        else
        {
            Debug.LogWarning("Iori GameObject is not assigned!");
        }
    }

    /* private void keyReset()
    {
        data.keyAPressCount = 0;
        data.keySPressCount = 0;
        data.keyDownPressCount = 0;
        data.keyLeftPressCount = 0;
        data.keyRightPressCount = 0;
        data.rightKeyPressCount = 0;
        data.leftKeyPressCount = 0;
    } */

    private IEnumerator ResetRunAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Run resetted");
        anim.SetBool("iori_run6", false);
        body.linearVelocityX = 0f;
        if (!data.fangsOfTheInferno)
            data.interval = false;
        deceleration = 0f;
        resetCoroutine = null;
    }

    /* private void ResetKeyCounts()
    {
        data.keyDownPressCount = 0;
        data.keyLeftPressCount = 0;
        data.keyASPressCount = 0;
        data.lasKeyDownPressTime = Time.time; // Update last press time
        data.lasKeyLeftPressTime = Time.time;
        data.lastKeyASPressTime = Time.time;
    } */
    ///////////////////


}
