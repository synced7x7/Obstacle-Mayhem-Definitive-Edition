using UnityEngine;

public class sit_punch_behaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!animator.GetBool("Sit Punch Chain"))
        {
            Iori_data.Instance.movementFlag = false;
            keyReset();
            Kyo_data.Instance.kyo_hit = 0;
        }
       animator.SetBool("Sit Punch", false);
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
