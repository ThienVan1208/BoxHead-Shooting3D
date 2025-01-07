using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "CamShakeEventChannelSO", menuName = "EventChannel/CamShakeEventChannelSO")]
public class CamShakeEventChannelSO : ScriptableObject {
    public Action<float, float> OnRaisedEvent;
    public void RaiseEvent(float strength, float time){
        OnRaisedEvent?.Invoke(strength, time);
    }
}
