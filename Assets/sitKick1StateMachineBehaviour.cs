using UnityEngine;

public class sitKick1StateMachineBehaviour : StateMachineBehaviour
{
    private bool updateDisabled = false;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!updateDisabled)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                updateDisabled = true;
                animator.SetBool("Sit Kick 2", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Sit Kick 1", false);
        if (!animator.GetBool("Sit Kick 2"))
        {
            Kyo_data.Instance.interval = false;
        }
        updateDisabled = false;
    }
}
