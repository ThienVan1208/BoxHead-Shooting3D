using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContainer : MonoBehaviour
{
    [SerializeField] private BulletContainerSO _bulletContainerSO;
    [SerializeField] private List<GameObject> _bulletList = new List<GameObject>();
    private void OnEnable() {
        GetFire();
    }
    private void GetFire(){
        foreach (var bullet in _bulletList){
            bullet.SetActive(false);
            bullet.transform.position = transform.position;
            bullet.SetActive(true);
        }
    }
}
