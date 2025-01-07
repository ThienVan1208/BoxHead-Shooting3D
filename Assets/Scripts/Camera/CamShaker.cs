using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CamShaker : MonoBehaviour
{
    [SerializeField] private CamShakeEventChannelSO _camShakeEventSO;
    [SerializeField] private CinemachineVirtualCamera _virtualCam;
    private CinemachineBasicMultiChannelPerlin _noise;
    
    private void Awake() {
        _noise = _virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    private void OnEnable() {
        _camShakeEventSO.OnRaisedEvent += GetCamShake;
    }
    private void OnDisable() {
        _camShakeEventSO.OnRaisedEvent -= GetCamShake;
    }
    private void GetCamShake(float strength, float time){
        _noise.m_AmplitudeGain = strength;
        _noise.m_FrequencyGain = strength * 2;
        DOTween.To(() => _noise.m_AmplitudeGain, x => _noise.m_AmplitudeGain = x, 0f, time).SetEase(Ease.InOutQuad);
        DOTween.To(() => _noise.m_FrequencyGain, x => _noise.m_FrequencyGain = x, 0f, time).SetEase(Ease.InOutQuad); 
    }
}
