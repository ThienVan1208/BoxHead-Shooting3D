using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "EventChannel/GunInfoEventChannelSO", fileName = "GunInfoEventChannelSO")]
public class GunInfoEventChannel : ScriptableObject
{
    public Action<string, int, int> OnRaisedEvent;
    public void RaiseEvent(string gunName, int maxBullet, int totalBullet){
        OnRaisedEvent?.Invoke(gunName, maxBullet, totalBullet);
    }
}
