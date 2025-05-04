using UnityEngine;

public class infernalBloodLustChainStateMachineBehaviour : StateMachineBehaviour
{
   

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       Iori_data.Instance.interval = false;
       animator.SetBool("Infernal Blood Lust", false);
       animator.SetBool("Infernal Blood Lust Chain", false);
    }

   
}
