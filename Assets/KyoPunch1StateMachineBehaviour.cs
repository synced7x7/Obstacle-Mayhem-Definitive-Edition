using System.Data.Common;
using UnityEngine;

public class KyoPunch1StateMachineBehaviour : StateMachineBehaviour
{
    private bool updateDisabled = false;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!updateDisabled)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                updateDisabled = true;
                animator.SetBool("Punch 2", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Punch 1", false);
        if (!animator.GetBool("Punch 2"))
        {
            Kyo_data.Instance.interval = false;
        }
        updateDisabled = false;
        Kyo_physicsHandler.Instance.isLocomotionEnabled = false;
    }
}
