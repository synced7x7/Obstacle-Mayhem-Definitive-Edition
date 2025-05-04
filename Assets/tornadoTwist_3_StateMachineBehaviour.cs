using UnityEngine;

public class tornadoTwist_3_StateMachineBehaviour : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Tornado Twist 3", false);
        Kyo_data.Instance.interval = false;
    }
}
