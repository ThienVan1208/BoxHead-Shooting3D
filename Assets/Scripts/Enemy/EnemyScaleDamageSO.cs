using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyScaleDamageSO", menuName = "Enemy/EnemyScaleDamage")]
public class EnemyScaleDamageSO : ScriptableObject {
    public float scale = 1;
    [SerializeField] private FloatEventChannelSO _ScaleEnemyDamageEventSO;
    private void OnEnable() {
        _ScaleEnemyDamageEventSO.OnRaisedEvent += ChangeScale;
    }
    private void OnDisable() {
        _ScaleEnemyDamageEventSO.OnRaisedEvent -= ChangeScale;
    }
    private void ChangeScale(float _amount){
        scale = _amount;
    }
}
