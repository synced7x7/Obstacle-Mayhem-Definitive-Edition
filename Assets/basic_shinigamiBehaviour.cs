using System.Data.Common;
using UnityEngine;

public class basic_shinigamiBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       Iori_data.Instance.interval = false;
       animator.SetBool("basic_shinigami", false);
    }

    
}
