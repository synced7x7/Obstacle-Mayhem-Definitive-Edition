using UnityEngine;

public class shattered_flowersBehaviour : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        speedyanimationBGBehaviour.Instance.speedyAnimationDeactivator(0.1f);
        Iori_data.Instance.interval = false;
        animator.SetBool("Shattered_Flowers", false);
        animator.SetBool("Blood Blossom of the Oblivion", false);
    }
}
