using UnityEngine;

public class KyoKick3StateMachineBehviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Kick 3", false);
        Kyo_data.Instance.interval = false;
        
    }
}
