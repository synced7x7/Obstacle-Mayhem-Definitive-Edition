using UnityEngine;

public class explodeEffect1StateMachineBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       Special_effect_behaviour.Instance.spriteRendererDeactivator();
    }
}
