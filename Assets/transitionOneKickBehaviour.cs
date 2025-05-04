using UnityEngine;

public class transitionOneKickBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!animator.GetBool("secondKick"))
        {
            Iori_data.Instance.action = false;
            //Debug.Log("ending after first kick");
        }
        
        animator.SetBool("firstKick", false);
        Iori_data.Instance.interval = false;
        reset_keypress();
    }
    public void reset_keypress()
    {
        Iori_data.Instance.rightKeyPressCount = 1;
        Iori_data.Instance.leftKeyPressCount = 1;
    }
}
