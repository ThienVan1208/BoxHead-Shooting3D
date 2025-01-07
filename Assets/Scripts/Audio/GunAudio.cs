using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAudio : MonoBehaviour
{
    [SerializeField] private AudioEventChannelSO _sfxEventSO;
    [SerializeField] private FloatEventChannelSO _sfxVolumeEventSO;
    [SerializeField] private AudioSource _audioSource;
    private void OnEnable() {
        _sfxEventSO.OnRaisedEvent += PlaySound;
        _sfxVolumeEventSO.OnRaisedEvent += ChangeVolume;
    }
    private void OnDisable() {
        _sfxEventSO.OnRaisedEvent -= PlaySound;
        _sfxVolumeEventSO.OnRaisedEvent -= ChangeVolume;
    }
    private void PlaySound(AudioGroupSO au){
        _audioSource.PlayOneShot(au.GetRandomClip(), au.Volume);
    }
    private void ChangeVolume(float amount){
        _audioSource.volume += amount;
    }
}
