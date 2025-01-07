using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "BoolEventChannelSO", menuName = "EventChannel/BoolEventChannelSO")]
public class BoolEventChannelSO : ScriptableObject {
    public Action<bool> OnRaisedEvent;
    public void RaiseEvent(bool arg){
        OnRaisedEvent?.Invoke(arg);
    }
}
