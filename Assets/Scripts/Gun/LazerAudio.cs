using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerAudio : MonoBehaviour
{
    [SerializeField] private AudioEventChannelSO _gunAudioEventSO;
    [SerializeField] private AudioGroupSO _lazerAudio;
    [SerializeField] private float _timeAudio;
    private void OnEnable() {
        StartCoroutine(WaitForNextLazerAudio(_timeAudio));
    }


    private IEnumerator WaitForNextLazerAudio(float time)
    {
        while (true)
        {
            _gunAudioEventSO.RaiseEvent(_lazerAudio);
            yield return new WaitForSeconds(time);
        }
    }
}
