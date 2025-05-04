using System.Data.Common;
using UnityEngine;

public class around_the_worldBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Iori_data.Instance.interval = false;
        animator.SetBool("around_the_world", false);
    }
}
