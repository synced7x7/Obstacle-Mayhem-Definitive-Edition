using UnityEngine;
using System.Collections;

public class kyo_slowMotion : MonoBehaviour
{
    public static kyo_slowMotion Instance { get; private set; } 

    public void Awake()
    {
        #region instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        #endregion
    }
    public void TriggerSlowMotion(float duration, float slowScale)
    {
        StartCoroutine(SlowMotionRoutine(duration, slowScale));
    }

    private IEnumerator SlowMotionRoutine(float duration, float slowScale) //duration = how long the slow mo will last 
                                                                           // slowScale: The speed of the game during slow motion (e.g., 0.2f makes the game run at 20% speed).
    {
        Time.timeScale = slowScale; //time.timescale controls the overall speed of the game. Normal speed  = 1f.
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // Update physics timestep to match new time scale to sync physics with the system scale. important!!!

        // Wait for the duration
        yield return new WaitForSecondsRealtime(duration);

        // Restore time scale
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f; // Reset to default physics timestep
    }
}
