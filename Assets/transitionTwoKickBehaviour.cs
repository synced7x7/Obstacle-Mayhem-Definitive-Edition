using UnityEngine;

public class transitionTwoKickBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.GetBool("thirdKick"))
        {
            Iori_data.Instance.action = false;
        }

        animator.SetBool("secondKick", false);
        Iori_data.Instance.interval = false;
        reset_keypress();
    }
    public void reset_keypress()
    {
        Iori_data.Instance.rightKeyPressCount = 1;
        Iori_data.Instance.leftKeyPressCount = 1;
    }
}
