using UnityEngine;

public class tornadoTwist_1_StateMachineBehaviour : StateMachineBehaviour
{
    private bool updateDisabled = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Kyo_keyHandler.Instance.LeftKeypressCount_dependent = 0; 
        Kyo_keyHandler.Instance.DownKeyPressCount = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!updateDisabled)
        {
            if (Kyo_keyHandler.Instance.LeftKeypressCount_dependent == 1 && Kyo_keyHandler.Instance.DownKeyPressCount == 1 && Input.GetKeyDown(KeyCode.S) )
            {
                updateDisabled = true;
                animator.SetBool("Tornado Twist 2", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Tornado Twist 1", false);
        if (!animator.GetBool("Tornado Twist 2"))
        {
            Kyo_data.Instance.interval = false;
        }
        updateDisabled = false;
    }


}
