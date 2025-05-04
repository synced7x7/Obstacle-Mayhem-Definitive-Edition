using UnityEngine;

public class heavyPunchOneBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!animator.GetBool("heavy_punch2"))
        {
            Iori_data.Instance.action = false;
        }
        
        animator.SetBool("heavy_punch1", false);
        Iori_data.Instance.interval = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
