using System.Data.Common;
using UnityEngine;

public class flame_ritual_behaviour : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       Iori_data.Instance.interval = false; 
    }

}
