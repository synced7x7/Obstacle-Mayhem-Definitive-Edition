using UnityEngine;

public class transitionThreeBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!animator.GetBool("fourth_move")) //not chained ///exit
        {
            //Debug.Log("action set to false");
            Iori_data.Instance.action = false;
        }
        Iori_data.Instance.interval = false;
        animator.SetBool("third_mov", false); //setting the previous one to false
        //Debug.Log("Exitting the third move");
        reset_keypress();
    }
    public void reset_keypress()
    {
        Iori_data.Instance.rightKeyPressCount = 1;
        Iori_data.Instance.leftKeyPressCount = 1;
    }
}
