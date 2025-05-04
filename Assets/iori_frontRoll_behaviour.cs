using UnityEngine;

public class iori_frontRoll_behaviour : StateMachineBehaviour
{


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /* if (!Iori_data.Instance.isCollidingPlayer)
        {
            animator.SetBool("riot_of_the_blood", false);
            animator.SetBool("Blood Blossom of the Oblivion", false);
            Iori_data.Instance.interval = false;
        } */
    }

}
