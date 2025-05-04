using UnityEngine;

public class KyoKick1StateMachineBehviour : StateMachineBehaviour
{
    private bool updateDisabled = false;
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!updateDisabled)
        {
            if (Input.GetKeyDown(KeyCode.S) && Iori_data.Instance.iori_hit > 0)
            {
                updateDisabled = true;
                animator.SetBool("Kick 2", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Kick 1", false);
        if (!animator.GetBool("Kick 2"))
        {
            Kyo_data.Instance.interval = false;
        }
        updateDisabled = false;
    }
}
