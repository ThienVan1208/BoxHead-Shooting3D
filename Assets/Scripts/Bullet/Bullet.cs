using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed;
    void Start()
    {
        
    }
    private void OnEnable() {
        MoveForward();
    }
    private void OnDisable() {
        _rb.velocity = Vector3.zero;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void MoveForward(){
        _rb.velocity = transform.forward * _speed;
    }
    private void OnTriggerEnter(Collider other) {
        bool condition = other.gameObject.layer == Constant.LAYER_GROUND 
        || other.gameObject.layer == Constant.LAYER_WALL
        || other.gameObject.layer == Constant.LAYER_ENEMY;
        if (condition){
            gameObject.SetActive(false);
        }
    }
}
