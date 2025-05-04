using UnityEngine;

public class infernalBloodLustStateMachineBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Kyo_spriteActivator.Instance.PauseAnimation();
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        speedyanimationBGBehaviour.Instance.speedyAnimationDeactivator(0.1f);
        Kyo_spriteActivator.Instance.ResumeAnimation();
    }
}
