using UnityEngine;

public class heavyPunchTwoBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.GetBool("heavy_punch3"))
        {
            Iori_data.Instance.action = false;
        }
        animator.SetBool("heavy_punch2", false);
        Iori_data.Instance.interval = false;
    }
}
