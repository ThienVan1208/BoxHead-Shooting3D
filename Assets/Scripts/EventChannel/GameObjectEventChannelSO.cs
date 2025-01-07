using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "EventChannel/GameObjectEventChannelSO")]
public class GameObjectEventChannelSO : ScriptableObject
{
    public Action<GameObject> OnRaisedEvent;
    public void RaiseEvent(GameObject arg){
        OnRaisedEvent?.Invoke(arg);
    }
}
