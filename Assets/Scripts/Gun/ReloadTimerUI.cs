using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReloadTimerUI : MonoBehaviour
{
    [SerializeField] private GameObject _reloadTimer;
    [SerializeField] private VoidEventChannelSO _InactiveReloadTimerEventSO;
    [SerializeField] private FloatEventChannelSO _reloadCountDownEventSO, _activeReloadTimerEventSO;
    private void OnEnable() {
        _activeReloadTimerEventSO.OnRaisedEvent += CountDown;
        _InactiveReloadTimerEventSO.OnRaisedEvent += InactiveReloadTimerUI;
    }
    private void OnDisable() {
        _activeReloadTimerEventSO.OnRaisedEvent -= CountDown;
        _InactiveReloadTimerEventSO.OnRaisedEvent -= InactiveReloadTimerUI;
    }
    private void CountDown(float begin){
        _reloadTimer.SetActive(true);
        _reloadCountDownEventSO.RaiseEvent(begin);
    }
    private void InactiveReloadTimerUI(){
        _reloadTimer.SetActive(false);
    }   
}
