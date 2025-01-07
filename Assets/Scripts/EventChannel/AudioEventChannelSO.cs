using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannel/AudioEventChannelSO")]
public class AudioEventChannelSO : ScriptableObject
{
    public Action<AudioGroupSO> OnRaisedEvent;
    public void RaiseEvent(AudioGroupSO clip){
        OnRaisedEvent?.Invoke(clip);
    }
}
