using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "EventChannel/VoidEventChannelSO")]
public class VoidEventChannelSO : ScriptableObject
{
    public Action OnRaisedEvent;
    public void RaiseEvent(){
        OnRaisedEvent?.Invoke();
    }
}
