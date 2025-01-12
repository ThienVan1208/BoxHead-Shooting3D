using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed;
    [SerializeField] private Sparkle _sparkleEffect;
    [SerializeField] protected LayerMask _layer;
    private void OnEnable() {
        MoveForward();
    }
    private void OnDisable() {
        _sparkleEffect.transform.position = transform.position;
        _sparkleEffect.transform.forward = transform.forward;
        _sparkleEffect.ActiveSpark();
        _rb.velocity = Vector3.zero;
    }
    private void MoveForward(){
        _rb.velocity = transform.forward * _speed;
    }
    protected virtual void OnTriggerEnter(Collider other) {
        // bool condition = other.gameObject.layer == Constant.LAYER_GROUND 
        // || other.gameObject.layer == Constant.LAYER_WALL
        // || other.gameObject.layer == Constant.LAYER_ENEMY;
        if (((1 << other.gameObject.layer) & _layer) != 0){
            gameObject.SetActive(false);
        }
    }
}
