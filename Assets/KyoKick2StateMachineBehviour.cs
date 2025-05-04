using UnityEngine;

public class KyoKick2StateMachineBehviour : StateMachineBehaviour
{
     private bool updateDisabled = false;
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!updateDisabled)
        {
            if (Input.GetKeyDown(KeyCode.S) )
            {
                updateDisabled = true;
                animator.SetBool("Kick 3", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Kick 2", false);
        updateDisabled = false;
        if (!animator.GetBool("Kick 3"))
        {
            Kyo_data.Instance.interval = false;
            
        }
    }
}
