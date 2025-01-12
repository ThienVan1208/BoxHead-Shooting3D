using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBallBullet : Bullet
{
    [SerializeField] private AudioGroupSO _fireAudioSO;
    [SerializeField] private AudioSource _fireAudioSource;
    protected override void OnTriggerEnter(Collider other){
        if (((1 << other.gameObject.layer) & _layer) != 0){
            gameObject.SetActive(false);
        }
        if(other.gameObject.tag == Constant.TAG_OUTSIDE_AUDIO_PLAYER){
            _fireAudioSource.PlayOneShot(_fireAudioSO.GetRandomClip(), _fireAudioSO.Volume);
        }
    }
}
