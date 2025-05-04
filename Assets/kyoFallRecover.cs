using UnityEngine;

public class kyoFallRecover : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Kyo_data.Instance.kyo_interval = false;
    }

}
