using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraShake : MonoBehaviour
{
    // 单例
    public static CameraShake _instance;
    // 虚拟相机
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    // 一个参数
    private CinemachineBasicMultiChannelPerlin _cbmcp;
    // 抖动强度
    public float _shakeIntensity = 1.0f;
    // 抖动时长
    public float _shakeTime = 0.2f;

    void Awake() {
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        _instance = this;
    }
    void Start()
    {
        stopShake();
    }

    public void startShake()
    {
        StartCoroutine(shake());
    }

    IEnumerator shake()
    {
        _cbmcp = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        _cbmcp.m_AmplitudeGain = _shakeIntensity;

        yield return new WaitForSeconds(_shakeTime);

        stopShake();
    }

    void stopShake()
    {
        _cbmcp = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        _cbmcp.m_AmplitudeGain = 0.0f;
    }
}
