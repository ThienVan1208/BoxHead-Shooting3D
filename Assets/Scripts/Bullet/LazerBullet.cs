using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBullet : MonoBehaviour
{
    [SerializeField] private float _length;
    [SerializeField] private GameObject _lazer, _preLazer;
    [SerializeField] private AudioEventChannelSO _gunAudioEventSO;
    [SerializeField] private AudioGroupSO _preLazerAudioSO;
    [SerializeField] private VoidEventChannelSO _lazerShootEventSO;
    private void OnEnable()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, _length);
        _lazer.SetActive(false);
        _preLazer.SetActive(false);
    }



    public void GetFire(bool canShoot){
        if(canShoot){
            _preLazer.SetActive(true);
            _gunAudioEventSO.RaiseEvent(_preLazerAudioSO);
        }
        else{
            _lazer.SetActive(false);
        }
    }

    // When preLazer is active true, it will be active false in animation and then active lazer true.
    public void InactivePreLazer(){
        _preLazer.SetActive(false);
        _lazer.SetActive(true);
        _lazerShootEventSO.RaiseEvent();
    }
    private void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.layer == Constant.LAYER_ENEMY)
        {
            float dis = Vector3.Distance(other.transform.position, transform.position);
            if (dis > _length)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, _length);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, dis);
            }
        }
    }
}
