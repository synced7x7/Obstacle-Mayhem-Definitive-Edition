using UnityEngine;

public class KyoTimerReset : MonoBehaviour
{
    [SerializeField] Kyo_data Data;
    public void KyoHitTimerReset()
    {
        if (Data.kyo_hit>0 && (Time.time - Data.last_hit_time >= Data.max_hit_time))
        {
            Data.kyo_hit = 0; // Reset the hit state
            //Debug.Log("Kyo hit state reset after max hit time.");
        }
        else if(Data.bloom_of_desolation_hit>0 && (Time.time - Data.blood_of_desolation_lastHitTime >= Data.max_hit_time+0.2f))
        {
            Data.bloom_of_desolation_hit = 0;
        }
    }
}
