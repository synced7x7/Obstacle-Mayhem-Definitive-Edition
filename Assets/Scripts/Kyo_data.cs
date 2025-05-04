using UnityEngine;

public class Kyo_data : MonoBehaviour
{
    public static Kyo_data Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    #region Variables
    public bool kyo_interval;
    public int kyo_hit; //blocks all input for kyo //input will be added later after the first release 
    public float max_hit_time = 0.5f; [HideInInspector] //recoil
    public float last_hit_time;
    public int bloom_of_desolation_hit;
    public float blood_of_desolation_lastHitTime;
    public bool kyoGrounded;
    public bool kyoFlipped; //when right false, when left true. //default true(left)
    public bool interval; // prevents everything
    public bool action; // prevents locomotion when active
    public bool movement;

    #endregion



    #region key 

    [HideInInspector] public float lastKeyRightPressTime_independent;
    [HideInInspector] public float lastKeyLeftPressTime_independent;
    public float rightKeyPressCount_independent;
    public float leftKeyPressCount_independent;
    #endregion

}
