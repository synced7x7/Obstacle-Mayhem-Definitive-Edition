using UnityEngine;

public class SitKickChainBeheviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Iori_data.Instance.movementFlag = false;
        animator.SetBool("Sit Kick Chain", false);
        keyReset();
    }
    private void keyReset()
    {
        Iori_data.Instance.keyAPressCount = 0;
        Iori_data.Instance.keySPressCount = 0;
        Iori_data.Instance.keyDownPressCount = 0;
        Iori_data.Instance.keyLeftPressCount = 0;
        Iori_data.Instance.keyRightPressCount = 0;
        Iori_data.Instance.rightKeyPressCount = 0;
        Iori_data.Instance.leftKeyPressCount = 0;
    }
}
