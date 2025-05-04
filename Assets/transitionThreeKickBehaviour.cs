using UnityEngine;

public class transitionThreeKickBehaviour : StateMachineBehaviour
{
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Iori_data.Instance.action = false;
        Iori_data.Instance.interval = false;
        animator.SetBool("thirdKick", false); //setting the previous one to false
        //Debug.Log("Exitting the fourth move");
        reset_keypress();
    }
    public void reset_keypress()
    {
        Iori_data.Instance.rightKeyPressCount = 1;
        Iori_data.Instance.leftKeyPressCount = 1;
    }
}
