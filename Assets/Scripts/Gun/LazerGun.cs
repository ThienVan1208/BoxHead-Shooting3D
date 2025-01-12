using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerGun : GunBase
{
    [SerializeField] private LazerBullet _lazerBullet;

    // Subcribed in GunManager.
    [SerializeField] private BoolEventChannelSO _canChangGunEventSO;

    [SerializeField] private VoidEventChannelSO _lazerShootEventSO;
    [SerializeField] private CamShakeEventChannelSO _camShakeEventSO;
    [SerializeField] private float _strengthCamShake, _timeCamShake;
    private int _curBullet;
    public bool _isPrepared;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnEnable()
    {
        _lazerShootEventSO.OnRaisedEvent += WaitShootBullet;
        base.OnEnable();
        _curBulletEventSO.RaiseEvent(_curBullet);
        _isPrepared = false;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        _lazerShootEventSO.OnRaisedEvent -= WaitShootBullet;
    }
    public override void GetShooting()
    {
        if (_gunAttributesSO.outOfBullet || _isPrepared) return;

        if (_gunAttributesSO.canShoot)
        {
            _canChangGunEventSO.RaiseEvent(false);
            _isPrepared = true;
            _gunAttributesSO.canShoot = false;
            //WaitShootBullet(_gunAttributesSO.timeShoot);
            _anim.SetTrigger(_shootTriggerAnim);
            _lazerBullet.GetFire(true);
        }
        else
        {
            _gunAttributesSO.canShoot = true;
            _lazerBullet.GetFire(false);
            _canChangGunEventSO.RaiseEvent(true);
        }


    }

    // Used in animation.
    public void InactivePreLazer()
    {
        _lazerBullet.InactivePreLazer();
    }

    // Used in animation.
    // When _isPrepared is true, preLazer can not be active false.
    public void SetLogicForIsPrepared(int isTrue)
    {
        _isPrepared = isTrue != 0 ? true : false;
    }
    public void GetCamShake()
    {
        _camShakeEventSO.RaiseEvent(_strengthCamShake, _timeCamShake);
    }

    private void WaitShootBullet(){
        StartCoroutine(WaitShootBulletCoroutine(_gunAttributesSO.timeShoot));
    }
    // Used to descrease current bullet while active true lazer.
    // Used in WaitShootBullet method.
    private IEnumerator WaitShootBulletCoroutine(float time)
    {
        while (!_gunAttributesSO.canShoot)
        {
            _curBullet--;
            _curBulletEventSO.RaiseEvent(_curBullet);
            CheckCurrentBullet();
            yield return new WaitForSeconds(time);
            //_crt = null;
        }
    }
    protected override void CheckCurrentBullet()
    {
        if (_curBullet <= 0)
        {
            _canChangGunEventSO.RaiseEvent(true);
            _lazerBullet.GetFire(false);
            _gunAttributesSO.canShoot = true;
            _gunAttributesSO.outOfBullet = true;
            ReloadBulletStack();
        }
    }
    protected override void ReloadBulletStack()
    {
        if (_gunAttributesSO.totalBullet <= 0) return;
        StartCoroutine(WaitForReload(_gunAttributesSO.timeReload));
    }
    private IEnumerator WaitForReload(float time)
    {
        _gunAudioEventSO.RaiseEvent(_gunAttributesSO.reloadSoundSO);
        yield return new WaitForSeconds(time);
        _curBullet = _gunAttributesSO.numberBullet;
        _curBulletEventSO.RaiseEvent(_curBullet);
        _gunAttributesSO.totalBullet--;
        _totalBulletUIEventSO.RaiseEvent(_gunAttributesSO.totalBullet);
        _gunAttributesSO.outOfBullet = false;
    }
    protected override void InitBulletPool()
    {
        _gunAttributesSO.totalBullet = _gunAttributesSO.constTotalBullet;
        _curBullet = _gunAttributesSO.numberBullet;
    }
}
