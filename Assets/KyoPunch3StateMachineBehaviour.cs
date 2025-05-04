using UnityEngine;

public class KyoPunch3StateMachineBehaviour : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Punch 3", false);
        Kyo_data.Instance.interval = false;
    }
}
