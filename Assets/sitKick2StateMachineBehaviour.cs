using UnityEngine;

public class sitKick2StateMachineBehaviour : StateMachineBehaviour
{
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Sit Kick 2", false);
        Kyo_data.Instance.interval = false;
    }
}
