using UnityEngine;

public class KyoJumpPunchStateMachineBehaviour : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Kyo_data.Instance.kyoGrounded)
        {
            animator.Play("kyo_idle0");
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Kyo_data.Instance.interval = false;
    }

}
