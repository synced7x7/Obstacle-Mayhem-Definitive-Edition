using UnityEngine;

public class infernalBloodLustJumpStateMachineBehaviour : StateMachineBehaviour
{
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      Iori_animationEndController.Instance.infernalBloodLustJumpController();
   }

   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if (!animator.GetBool("Infernal Blood Lust Chain")) //couldn't devour kyo
      {
         Iori_data.Instance.interval = false;
         animator.SetBool("Infernal Blood Lust", false);
      }
   }
}
