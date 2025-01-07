using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunUI : MonoBehaviour
{
    [SerializeField] private GunInfoEventChannel _gunInfoEventSO;

    // Used to change UI for the current bullet.
    [SerializeField] private IntEventChannelSO _curBulletEventSO;

    // Used to change UI for the total bullet.
    [SerializeField] private IntEventChannelSO _totalBulletUIEventSO;
    
    [SerializeField] private TextMeshProUGUI _gunNameTxt, _curBulletTxt, _maxBulletTxt, _totalBulletTxt;
    private void OnEnable() {
        _gunInfoEventSO.OnRaisedEvent += ChangeGunUI;
        _curBulletEventSO.OnRaisedEvent += ChangeCurBulletUI;
        _totalBulletUIEventSO.OnRaisedEvent += ChangeTotalBulletUI;
    }
    private void OnDisable() {
        _gunInfoEventSO.OnRaisedEvent -= ChangeGunUI;
        _curBulletEventSO.OnRaisedEvent -= ChangeCurBulletUI;
        _totalBulletUIEventSO.OnRaisedEvent -= ChangeTotalBulletUI;
    }
    private void ChangeGunUI(string gunName, int maxBullet, int totalBullet){
        _gunNameTxt.text = gunName;
        _maxBulletTxt.text = "/ " + maxBullet.ToString();
        _totalBulletTxt.text = totalBullet.ToString();
    }
    private void ChangeCurBulletUI(int curBullet){
        _curBulletTxt.text = curBullet.ToString();
    }
    private void ChangeTotalBulletUI(int totalBullet){
        _totalBulletTxt.text = totalBullet.ToString();
    }

}
