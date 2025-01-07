using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(menuName = "EventChannel/FloatEventChannelSO")]
public class FloatEventChannelSO : ScriptableObject
{
    public Action<float> OnRaisedEvent;
    public void RaiseEvent(float arg){
        OnRaisedEvent?.Invoke(arg);
    }
}

