using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxManager : MonoBehaviour
{
    [SerializeField] private GameObject _hitBox;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == Constant.TAG_PLAYER){
            _hitBox.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == Constant.TAG_PLAYER){
            _hitBox.SetActive(false);
        }
    }
}
