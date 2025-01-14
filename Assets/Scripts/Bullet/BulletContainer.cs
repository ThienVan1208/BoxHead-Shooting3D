using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContainer : MonoBehaviour
{
    [SerializeField] private BulletContainerSO _bulletContainerSO;
    [SerializeField] private List<GameObject> _bulletList = new List<GameObject>();
    [SerializeField] private float _timeExist;

    private void OnEnable() {
        GetFire();
        StartCoroutine(WaitForInactive(_timeExist));
    }
    private void GetFire(){
        foreach (var bullet in _bulletList){
            bullet.SetActive(false);
            bullet.transform.position = transform.position;
            bullet.SetActive(true);
        }
    }

    private IEnumerator WaitForInactive(float time){
        yield return new WaitForSeconds(time);
        foreach (var bullet in _bulletList){
            bullet.SetActive(false);
            bullet.transform.position = transform.position;
        }
        gameObject.SetActive(false);
    }
}
