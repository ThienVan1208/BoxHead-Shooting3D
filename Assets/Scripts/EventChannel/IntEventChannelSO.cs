using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(menuName = "EventChannel/IntEventChannelSO")]
public class IntEventChannelSO : ScriptableObject
{
    public Action<int> OnRaisedEvent;
    public void RaiseEvent(int arg){
        OnRaisedEvent?.Invoke(arg);
    }
}
