using System.Data.Common;
using UnityEngine;

public class intermediate_shinigamiBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Iori_data.Instance.fangsOfTheInferno = false;
        Iori_data.Instance.interval = false;
        animator.SetBool("intermediate_shinigami", false);
    }
}
