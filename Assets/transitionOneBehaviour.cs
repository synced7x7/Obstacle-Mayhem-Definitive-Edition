using System.Data.Common;
using UnityEngine;

public class transitionOneBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!animator.GetBool("second_move"))
        {
            Iori_data.Instance.action = false;
            Iori_data.Instance.keyAPressCount = 0;
        }
        
        animator.SetBool("first_move", false);
        Iori_data.Instance.interval = false;
        reset_keypress();
    }
    public void reset_keypress()
    {
        Iori_data.Instance.rightKeyPressCount = 1;
        Iori_data.Instance.leftKeyPressCount = 1;
    }
    
}
