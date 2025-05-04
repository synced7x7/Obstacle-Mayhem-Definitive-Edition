using UnityEngine;

public class jump_kick_chain_behaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Jump Kick Chain", false);
    }
}
