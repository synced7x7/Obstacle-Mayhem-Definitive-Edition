using UnityEngine;

public class heavyPunchThreeBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Iori_data.Instance.action = false;
        Iori_data.Instance.interval = false;
        animator.SetBool("heavy_punch3", false); //setting the previous one to false
        //Debug.Log("Exitting the third move");
    }
}
