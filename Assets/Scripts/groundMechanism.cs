using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private void Awake()
    {
        // Disable the renderer to make the object invisible
        GetComponent<Renderer>().enabled = false;
    }
}
