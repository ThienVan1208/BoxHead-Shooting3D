using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] private GunManagerSO _gunManagerSO;
    [SerializeField] private BoolEventChannelSO _canChangeGunEventSO;
    [SerializeField] private InputReaderSO _inputReaderSO;

    // Used to change gun damage causing for enemy. This event is subcribed in Enemy class.
    [SerializeField] private FloatEventChannelSO _DamageEventChannelSO;

    
    //[SerializeField] private GunInfoEventChannel _gunInfoEventSO;

    [SerializeField] private VoidEventChannelSO _inactiveReloadTimerEventSO;

    // Used to store gun list in play mode.
    private List<KeyValuePair<GameObject, float>> _inGameList = new List<KeyValuePair<GameObject, float>>();
    
    private void Awake()
    {
        Init();
    }
    private void OnEnable()
    {
        ActiveCurGun();
        _inputReaderSO.forwardGunAction += ChangeForwardGunIndex;
        _inputReaderSO.backwardGunAction += ChangeBackwardGunIndex;
        _gunManagerSO.playerDieEventSO.OnRaisedEvent += StopWhenPlayerDie;
        _canChangeGunEventSO.OnRaisedEvent += CanChangeGun;
    }
    private void OnDisable() {
        _inputReaderSO.forwardGunAction -= ChangeForwardGunIndex;
        _inputReaderSO.backwardGunAction -= ChangeBackwardGunIndex;
        _gunManagerSO.playerDieEventSO.OnRaisedEvent -= StopWhenPlayerDie;
        _canChangeGunEventSO.OnRaisedEvent -= CanChangeGun;
    }

    private void CanChangeGun(bool arg){
        _gunManagerSO.canChangeGun = arg;
    }

    // Press C to change gun forward.
    // Used in InputReaderSO.
    private void ChangeForwardGunIndex()
    {
        if(!_gunManagerSO.canChangeGun) return;

        _gunManagerSO.prevGun = _gunManagerSO.curGun;
        _gunManagerSO.curGun = (_gunManagerSO.curGun + 1) % _inGameList.Count;
        ChangeGunModel();
    }

    // Press X to change gun backward.
    // Used in InputReaderSO.
    private void ChangeBackwardGunIndex()
    {
        if(!_gunManagerSO.canChangeGun) return;

        _gunManagerSO.prevGun = _gunManagerSO.curGun;
        _gunManagerSO.curGun--;
        if(_gunManagerSO.curGun < 0) _gunManagerSO.curGun = _inGameList.Count - 1;
        ChangeGunModel();
    }

    // Used when change gun.
    // Used in ChangeForwardGunIndex and ChangeBackwardGunIndex.
    private void ChangeGunModel()
    {
        // Inactive reload timer when change gun.
        _inactiveReloadTimerEventSO.RaiseEvent();

        // Inactive previous gun and then active current gun.
        _inGameList[_gunManagerSO.prevGun].Key.SetActive(false);
        _inGameList[_gunManagerSO.curGun].Key.SetActive(true);

        // Get damage of current gun and then change damage taken of enemy when changing gun.
        _gunManagerSO.curDamage = _inGameList[_gunManagerSO.curGun].Value;
        _DamageEventChannelSO.RaiseEvent(_gunManagerSO.curDamage);

        // Change UI for gun infor.
        _gunManagerSO.gunInfoEventSO.RaiseEvent(_gunManagerSO.gunList[_gunManagerSO.curGun].gunName
                                , _gunManagerSO.gunList[_gunManagerSO.curGun].numberBullet
                                , _gunManagerSO.gunList[_gunManagerSO.curGun].totalBullet);
    }

    // Used to initializa bullet pool.
    // Used in Awake method.
    private void Init()
    {
        _gunManagerSO.curGun = 0;
        _gunManagerSO.prevGun = 0;
        foreach (var gunSO in _gunManagerSO.gunList)
        {
            GameObject gun = Instantiate(gunSO.gunModel, transform.position, Quaternion.identity);
            gun.SetActive(false);
            gun.transform.SetParent(transform);
            gun.transform.localPosition = Vector3.zero;
            KeyValuePair<GameObject, float> newGun = new KeyValuePair<GameObject, float>(gun, gunSO.damage);
            _inGameList.Add(newGun);
        }
        _DamageEventChannelSO.RaiseEvent(_inGameList[_gunManagerSO.curGun].Value);
    }

    // Used to active gun model when changing gun.
    // Used in OnEnable method to initialize gun model when first playing. 
    private void ActiveCurGun()
    {
        _gunManagerSO.canChangeGun = true;
        _inGameList[_gunManagerSO.curGun].Key.SetActive(true);
        _gunManagerSO.GetElemGunAttribute().canShoot = true;
        _gunManagerSO.curDamage = _inGameList[_gunManagerSO.curGun].Value;
        _DamageEventChannelSO.RaiseEvent(_inGameList[_gunManagerSO.curGun].Value);
        _gunManagerSO.gunInfoEventSO.RaiseEvent(_gunManagerSO.gunList[_gunManagerSO.curGun].gunName
                                , _gunManagerSO.gunList[_gunManagerSO.curGun].numberBullet
                                , _gunManagerSO.gunList[_gunManagerSO.curGun].totalBullet);
    }


    private void StopWhenPlayerDie(){
        _gunManagerSO.GetElemGunAttribute().canShoot = false;
    }
}
