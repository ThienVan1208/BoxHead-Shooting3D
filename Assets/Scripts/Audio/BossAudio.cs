using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAudio : MonoBehaviour
{
    [SerializeField] private FloatEventChannelSO _sfxVolumeEventSO;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioGroupSO _shootAudioSO;
    private void OnEnable() {
        _sfxVolumeEventSO.OnRaisedEvent += ChangeVolume;
    }
    private void OnDisable() {
        _sfxVolumeEventSO.OnRaisedEvent -= ChangeVolume;
    }
    private void PlaySound(AudioGroupSO au){
        _audioSource.PlayOneShot(au.GetRandomClip(), au.Volume);
    }
    private void ChangeVolume(float amount){
        _audioSource.volume += amount;
    }

    // Used in BossEnemy class.
    public void GetShootAudio(){
        PlaySound(_shootAudioSO);
    }
}
