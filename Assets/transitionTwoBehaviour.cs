using UnityEngine;

public class transitionTwoBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!animator.GetBool("third_mov")) //not chained ///exit
        {
            Iori_data.Instance.action = false;
        }
        Iori_data.Instance.interval = false;
        animator.SetBool("second_move", false); //setting the previous one to false
        //Debug.Log("Exit");
        reset_keypress();
    }
    public void reset_keypress()
    {
        Iori_data.Instance.rightKeyPressCount = 1;
        Iori_data.Instance.leftKeyPressCount = 1;
    }
}
