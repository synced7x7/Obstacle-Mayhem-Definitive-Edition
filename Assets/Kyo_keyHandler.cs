using UnityEngine;

public class Kyo_keyHandler : MonoBehaviour
{
    public static Kyo_keyHandler Instance { get; private set; }
    [SerializeField] Kyo_data data;
    public int DownKeyPressCount;
    private float lastDownkeyPressTime;
    public int RightKeypressCount_dependent;
    private float lastRightKeypressTime_dependent;
    public int LeftKeypressCount_dependent;
    private float lastLeftKeypressTime_dependent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void HandleRightKeyPressIndependent()
    {
        if (Time.time - data.lastKeyRightPressTime_independent < 0.2f && data.kyoGrounded) // Detect double press within 0.2 seconds
        {
            data.rightKeyPressCount_independent++;
            // Debug.Log("rightkey incremented");
        }
        else
        {
            data.rightKeyPressCount_independent = 1; // Reset the counter if too much time has passed
                                                     //Debug.Log("rightkey resetted");
        }

        data.lastKeyRightPressTime_independent = Time.time;
    }

    public void HandleLeftKeyPressIndependent()
    {
        if (Time.time - data.lastKeyLeftPressTime_independent < 0.2f && data.kyoGrounded) // Detect double press within 0.2 seconds
        {
            data.leftKeyPressCount_independent++;
            //Debug.Log("leftkey incremented");
        }
        else
        {
            data.leftKeyPressCount_independent = 1; // Reset the counter if too much time has passed
                                                    //Debug.Log("leftkey resetted");
        }

        data.lastKeyLeftPressTime_independent = Time.time;
    }

    public void HandleDownKeyPressIndependent()
    {
        lastDownkeyPressTime = Time.time;
        if (Time.time - lastDownkeyPressTime < 0.2f) // Detect double press within 0.2 seconds
        {
            DownKeyPressCount++;
            //Debug.Log("leftkey incremented");
        }
        else
        {
            DownKeyPressCount = 0; // Reset the counter if too much time has passed
                                   //Debug.Log("leftkey resetted");
        }

    }

    public void HandleRightKeyPressDependent()
    {
        lastRightKeypressTime_dependent = Time.time;
        if (Time.time - lastRightKeypressTime_dependent < 0.2f) // Detect double press within 0.2 seconds
        {
            RightKeypressCount_dependent++;
            // Debug.Log("rightkey incremented");
        }
        else
        {
            RightKeypressCount_dependent = 0; // Reset the counter if too much time has passed
                                              //Debug.Log("rightkey resetted");
        }
    }

    public void HandleLeftKeyPressDependent()
    {
        lastLeftKeypressTime_dependent = Time.time;
        if (Time.time - lastLeftKeypressTime_dependent < 0.2f ) // Detect double press within 0.2 seconds
        {
            LeftKeypressCount_dependent++;
            // Debug.Log("rightkey incremented");
        }
        else
        {
            LeftKeypressCount_dependent = 0; // Reset the counter if too much time has passed
                                             //Debug.Log("rightkey resetted");
        }
    }


    public void KeyRefresher()
    {
        // Key A Refresher
        if (DownKeyPressCount > 0 && Time.time - lastDownkeyPressTime > 0.2f)
        {
            DownKeyPressCount = 0;
        }
        if (RightKeypressCount_dependent > 0 && Time.time - lastRightKeypressTime_dependent > 0.2f)
        {
            RightKeypressCount_dependent = 0;
        }
        if (LeftKeypressCount_dependent > 0 && Time.time - lastLeftKeypressTime_dependent > 0.2f)
        {
            LeftKeypressCount_dependent = 0;
        }
    }



    public void resetKeypress()
    {
        data.leftKeyPressCount_independent = 1;
        data.rightKeyPressCount_independent = 1;
    }

}
