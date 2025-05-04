using UnityEngine;

public class special_effect1_stateMachineBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       Special_effect_behaviour.Instance.spriteRendererDeactivator();
    }
}
