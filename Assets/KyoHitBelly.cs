using UnityEngine;

public class KyoHitBelly : StateMachineBehaviour
{
    

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       Kyo_data.Instance.kyo_hit = 0;
       Kyo_data.Instance.bloom_of_desolation_hit = 0;
    }

    
}
