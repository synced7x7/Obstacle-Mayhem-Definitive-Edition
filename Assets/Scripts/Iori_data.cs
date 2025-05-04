using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

public class Iori_data : MonoBehaviour
{
    public static Iori_data Instance { get; private set; }
    [HideInInspector]

    public float lastKeyRightPressTime_independent; [HideInInspector]
    public float lastKeyLeftPressTime_independent;
    public int rightKeyPressCount;  //independent
    public int leftKeyPressCount; // independent
    ///Animation Properties
    public bool isbackjumping = false;
    public bool isdodging = false; [HideInInspector]
    public bool isjumping = false; [HideInInspector]
    public bool grounded = true;
    public bool action = false;  // Is an action in progress
    public bool interval;
    public bool devotion_to_the_inferno;
    public float devotionTime;
    public bool movementFlag = false;
    public float timeWindow = 0.4f; [HideInInspector]
    public bool movePerformed; [HideInInspector]

    /// <related to keypress>
    ///Key Timer
    public float lastKeyAPressTime; [HideInInspector]
    public float lasKeyDownPressTime; [HideInInspector]
    public float lasKeyLeftPressTime; [HideInInspector]
    public float lastKeySPressTime; [HideInInspector]
    public float lastKeyRightPressTime;

    ///Key Counter
    public int keyAPressCount; //A
    public int keySPressCount; //S
    public int keyDownPressCount; //Down Arrow
    public int keyLeftPressCount; //Left Arrow
    public int keyRightPressCount; //Right arrow //dependent on down arrow

    /// </end>
    public bool dummyCondition;
    public bool fangsOfTheInferno;

    public bool isCollidingPlayer;
    public float distanceToKyo;
    public bool ioriFlipped;


    /// <Hit Variables>
    public int iori_hit;
    public float max_hit_time = 0.5f; [HideInInspector] 
    public float last_hit_time;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
