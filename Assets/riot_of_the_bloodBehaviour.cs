using System.Data.Common;
using UnityEngine;

public class riot_of_the_bloodBehaviour : StateMachineBehaviour
{
    public enum KeyAction
    {
        LeftArrow,
        RightArrow
    }

    private float timeWindow = 0.4f;
    private float lastKeyAPressTime;
    private float lasKeyDownPressTime;
    private float lastKeyRightPressTime;
    private float lastKeyLeftPressTime;
    private float offset = 0.4f;

    public int keyAPressCount;
    public int keyDownPressCount;
    public int keyRightPressCount;
    [SerializeField] private int keyLeftPressCount;
    [SerializeField] private bool UpdateEnabled;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        UpdateEnabled = true;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.GetBool("Shattered_Flowers") && !animator.GetBool("Blood Blossom of the Oblivion"))
            Iori_data.Instance.interval = false;
        animator.SetBool("riot_of_the_blood", false);
        KeyRefresher();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime > 0.5f)
        {
            UpdateEnabled = false;
        }
        else if (UpdateEnabled)
        {
            if (!animator.GetBool("Blood Blossom of the Oblivion"))
            {
                actionkeyPressTracker();
                if (!Iori_data.Instance.ioriFlipped)
                {
                    if (keyDownPressCount > 0)
                    {
                        if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            ActionManualKeyPressTracker(KeyAction.RightArrow);
                            keyAPressCount = 0;
                        }

                    }
                    if (keyDownPressCount >= 2 && keyRightPressCount >= 2 && keyAPressCount > 0)
                    {
                        animator.SetBool("Shattered_Flowers", true);
                        Debug.Log("Shatterred Flowers Activated");
                    }
                }
                else
                {
                    if (keyDownPressCount > 0)
                    {
                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            ActionManualKeyPressTracker(KeyAction.LeftArrow);
                            keyAPressCount = 0;
                        }

                    }
                    if (keyDownPressCount >= 2 && keyLeftPressCount >= 2 && keyAPressCount > 0)
                    {
                        animator.SetBool("Shattered_Flowers", true);
                        Debug.Log("Shatterred Flowers Activated");
                    }
                }
            }
        }
    }
    private void actionkeyPressTracker()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Time.time - lasKeyDownPressTime <= timeWindow)
            {
                keyDownPressCount++; // Increment count if within the window
            }
            else
            {
                keyDownPressCount = 1; // Reset count if outside the window
            }

            lasKeyDownPressTime = Time.time; // Update the last key press time
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            // Check if the time since the last key press is within the time window
            if (Time.time - lastKeyAPressTime <= timeWindow)
            {
                keyAPressCount++; // Increment count if within the window
            }
            else
            {
                keyAPressCount = 1; // Reset count if outside the window
            }

            lastKeyAPressTime = Time.time; // Update the last key press time
            //Debug.Log("Key A or S pressed: " + data.keyASPressCount + " times");
        }
    }

    private void ActionManualKeyPressTracker(KeyAction action)
    {
        switch (action)
        {
            case KeyAction.RightArrow:
                if (Time.time - lastKeyRightPressTime <= timeWindow + offset)
                {
                    keyRightPressCount++; // Increment count if within the window
                }
                else
                {
                    keyRightPressCount = 1; // Reset count if outside the window
                }
                lastKeyRightPressTime = Time.time; // Update the last key press time
                //Debug.Log("Key Left Arrow Pressed : " + data.keyLeftPressCount + " times");
                break;
            case KeyAction.LeftArrow:
                if (Time.time - lastKeyLeftPressTime <= timeWindow + offset)
                {
                    keyLeftPressCount++; // Increment count if within the window
                }
                else
                {
                    keyLeftPressCount = 1; // Reset count if outside the window
                }
                lastKeyLeftPressTime = Time.time; // Update the last key press time
                break;
            default:
                Debug.LogWarning("Unknown Key Action");
                break;
        }
    }

    private void KeyRefresher()
    {
        keyAPressCount = 0;
        keyDownPressCount = 0;
        keyRightPressCount = 0;
        keyLeftPressCount = 0;
    }

}
