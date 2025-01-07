using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownReloadUI : MonoBehaviour
{
    [SerializeField] private FloatEventChannelSO _reloadCountDownEventSO;
    [SerializeField] private TextMeshProUGUI _timerTxt;
    
    private float _curTime;

    private void OnEnable() {
        _reloadCountDownEventSO.OnRaisedEvent += CountDown;
    }
    private void OnDisable() {
        _reloadCountDownEventSO.OnRaisedEvent -= CountDown;
    }
    private void CountDown(float begin){
        _curTime = begin;
        _timerTxt.text = _curTime.ToString("F2");
    }
    private void Update() {
        if(_curTime <= 0) gameObject.SetActive(false);
        _curTime -= Time.deltaTime;
        _timerTxt.text = _curTime.ToString("F2");
    }
}
