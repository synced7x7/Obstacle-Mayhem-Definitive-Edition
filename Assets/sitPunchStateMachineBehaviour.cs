using System.Data.Common;
using UnityEngine;

public class sitPunchStateMachineBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Sit Punch", false);
        Kyo_data.Instance.interval = false;
    }
}
