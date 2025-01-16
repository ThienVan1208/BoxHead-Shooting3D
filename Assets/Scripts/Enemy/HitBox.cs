using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private FloatEventChannelSO _changeHPEventSO;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == Constant.TAG_PLAYER){
            _changeHPEventSO.RaiseEvent(_damage * EnemyScaleDamageSO.scale);
        }
    }
}
