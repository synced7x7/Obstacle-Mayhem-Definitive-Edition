using UnityEngine;

public class KyoPunch2StateMachineBehaviour : StateMachineBehaviour
{
    private bool updateDisabled = false;
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!updateDisabled)
        {
            if (Input.GetKeyDown(KeyCode.A) && Iori_data.Instance.iori_hit > 0)
            {
                updateDisabled = true;
                animator.SetBool("Punch 3", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Punch 2", false);
        updateDisabled = false;
        if (!animator.GetBool("Punch 3"))
        {
            Kyo_data.Instance.interval = false;
        }
    }

}
