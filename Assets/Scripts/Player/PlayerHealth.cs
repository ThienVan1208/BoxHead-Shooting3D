using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerAttributesSO _playerAttributesSO;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private FloatEventChannelSO _changeHPEventSO;
    [SerializeField] private VoidEventChannelSO _playerDieEventSO;
    private void OnEnable() {
        InitHP();
        Debug.Log("init health");
        _changeHPEventSO.OnRaisedEvent += ChangeHP;
        _playerDieEventSO.OnRaisedEvent += Die;
    }
    private void OnDisable() {
        _changeHPEventSO.OnRaisedEvent -= ChangeHP;
        _playerDieEventSO.OnRaisedEvent -= Die;
    }
    private void InitHP(){
        _hpSlider.maxValue = _playerAttributesSO.maxHP;
        _hpSlider.value = _playerAttributesSO.maxHP;
        _playerAttributesSO.curHP = _playerAttributesSO.maxHP;
        _playerAttributesSO.isDie = false;
    }

    private void ChangeHP(float amount){
        _playerAttributesSO.curHP -= amount;
        _hpSlider.value = _playerAttributesSO.curHP;
        CheckDie();
    }
    private void CheckDie(){
        if(_playerAttributesSO.curHP <= 0){
            _playerAttributesSO.curHP = 0f;
            _playerDieEventSO.RaiseEvent();
        }
    }
    private void Die(){
        _playerAttributesSO.isDie = true;
        Debug.Log("Player Dies");
    }
    
}
