using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class CinemachineShake : MonoBehaviour 
{
    float ShakeAmplitude;       // Cinemachine Noise Profile Parameter
    float ShakeFrequency;        // Cinemachine Noise Profile Parameter
    float ShakeElapsedTime;

    
    CinemachineVirtualCamera VirtualCamera;
    CinemachineBasicMultiChannelPerlin virtualCameraNoise;
    bool isShaking = false;

    #region Singleton area

    public static CinemachineShake instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    #endregion

    private void Start()
    {
        SetVirtualCamera(GetComponent<CinemachineVirtualCamera>());
    }
    // Update is called once per frame
    void Update()
    {
        // If the Cinemachine componet is not set, avoid update
        if (VirtualCamera != null && virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (ShakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                // Update Shake Timer
                ShakeElapsedTime -= Time.deltaTime;

                isShaking = true;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                virtualCameraNoise.m_FrequencyGain = 0f;
                ShakeElapsedTime = 0f;

                isShaking = false;
            }
        }
    }

    public void ShakeCamera(float duration, float amplitude, float frequency)
    {
        if(!isShaking)
        {
            
            ShakeElapsedTime = duration;

            ShakeAmplitude = amplitude;

            ShakeFrequency = frequency;
        }  
    }

    public void SetVirtualCamera(CinemachineVirtualCamera cam)
    {
        VirtualCamera = cam;

        // Get Virtual Camera Noise Profile
        if (VirtualCamera != null)
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }
}