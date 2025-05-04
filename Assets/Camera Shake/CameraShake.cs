using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    private CinemachineCamera cinemachineCamera; // Cinemachine 3.x Camera
    private CinemachineBasicMultiChannelPerlin noise;

    private float shakeTimer;
    private float shakeTimerTotal;
    private float shakeIntensity;
    private float originalFieldOfView;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Get the CinemachineCamera component
        cinemachineCamera = GetComponent<CinemachineCamera>();
        if (cinemachineCamera == null)
        {
            Debug.LogError("CinemachineCamera component is missing. Make sure this script is attached to a CinemachineCamera GameObject.");
            return;
        }

        // Find the Noise Extension
        noise = cinemachineCamera.TryGetComponent<CinemachineBasicMultiChannelPerlin>(out var perlin) ? perlin : null;

        // Ensure the Noise Extension is present
        if (noise == null)
        {
            Debug.LogError("CinemachineBasicMultiChannelPerlin is not attached. Please add it in the Unity Editor.");
        }

        originalFieldOfView = cinemachineCamera.Lens.FieldOfView;
    }

    public void ShakeCamera(float intensity, float duration)
    {
        if (noise == null) return;

        shakeIntensity = intensity;
        shakeTimer = duration;
        shakeTimerTotal = duration;

        noise.AmplitudeGain = intensity; // Set the shake intensity
    }

    private void Update()
    {
        if (noise == null) return;

        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            // Smoothly reduce shake intensity over time
            noise.AmplitudeGain = Mathf.Lerp(0f, shakeIntensity, shakeTimer / shakeTimerTotal);
        }
        else if (noise.AmplitudeGain > 0f)
        {
            // Reset shake intensity when the timer ends
            noise.AmplitudeGain = 0f;
        }
    }

    public void zoomCamera(float zoomAmount, float zoomINDuration, float zoomOutDuration) //amount to zoomIN and zoomOUT in certain time durations.
    {
        StartCoroutine(ZoomInAndOut(zoomAmount, zoomINDuration, zoomOutDuration));
    }

     private IEnumerator ZoomInAndOut(float zoomAmount, float zoomINduration, float zoomOutDuration)
    {
        // Smoothly zoom in
        float elapsed = 0f;
        float startFieldOfView = cinemachineCamera.Lens.FieldOfView;
        float targetFieldOfView = startFieldOfView - zoomAmount;

        while (elapsed <  zoomINduration)
        {
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFieldOfView, targetFieldOfView, elapsed / zoomINduration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cinemachineCamera.Lens.FieldOfView = targetFieldOfView; // Ensure final zoom level

        yield return new WaitForSeconds(0.5f); // Adjust hold time if needed

        elapsed = 0f;
        while (elapsed < zoomOutDuration)
        {
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(targetFieldOfView, originalFieldOfView, elapsed / zoomOutDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cinemachineCamera.Lens.FieldOfView = originalFieldOfView; // Ensure back to original
    }
}
