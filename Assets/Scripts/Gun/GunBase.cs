using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour, IGun
{
    [SerializeField] protected GunAttributesSO _gunAttributesSO;
    [SerializeField] protected InputReaderSO _inputReaderSO;

    // Used to change UI for the current bullet in gun. This is subscribed in GunUI class.
    [SerializeField] protected IntEventChannelSO _curBulletEventSO;

    // Used to change UI for the total bullet.
    [SerializeField] protected IntEventChannelSO _totalBulletUIEventSO;

    [SerializeField] protected FloatEventChannelSO _activeReloadTimerEventSO;

    [SerializeField] protected AudioEventChannelSO _gunAudioEventSO;
    [SerializeField] protected Transform _bulletPos;
    [SerializeField] protected Animator _anim;
    [SerializeField] protected GameObject _gunFireEffect;
    
    protected int _shootTriggerAnim = Animator.StringToHash("Shoot");


    protected virtual void Awake()
    {
        InitBulletPool();
        _gunAttributesSO.outOfBullet = false;
        ReloadBulletStack();
    }
    protected virtual void OnEnable()
    {
        _inputReaderSO.shootAction += GetShooting;
        _gunAttributesSO.changeTotalBulletEventSO.OnRaisedEvent += ChangeTotalBullet;

        // Display current bullets on UI.
        _curBulletEventSO.RaiseEvent(_gunAttributesSO.bulletStack.Count);
        
        if (_gunAttributesSO.outOfBullet)
        {
            ReloadBulletStack();
        }
        _gunAttributesSO.canShoot = true;
    }
    protected virtual void OnDisable()
    {
        _inputReaderSO.shootAction -= GetShooting;
        _gunAttributesSO.changeTotalBulletEventSO.OnRaisedEvent -= ChangeTotalBullet;
    }

    

    // Used in inputReader.
    public virtual void GetShooting()
    {
        if (_gunAttributesSO.outOfBullet) return;

        if (!_gunAttributesSO.canShoot) return;

        // Shoot
        _gunAudioEventSO.RaiseEvent(_gunAttributesSO.shootSoundSO);
        _gunAttributesSO.canShoot = false;
        GameObject bullet = _gunAttributesSO.bulletStack.Pop();
        bullet.transform.position = _bulletPos.position;
        bullet.transform.forward = transform.forward;
        bullet.SetActive(true);
        _gunAttributesSO.listBullet.Add(bullet);

        _anim.SetTrigger(_shootTriggerAnim);
        StartCoroutine(WaitForShootTime(_gunAttributesSO.timeShoot));

        // Change UI for curBullet.
        _curBulletEventSO.RaiseEvent(_gunAttributesSO.bulletStack.Count);

        // When getting run of out bullet.
        CheckCurrentBullet();
    }

    protected virtual void CheckCurrentBullet()
    {
        if (_gunAttributesSO.bulletStack.Count == 0)
        {
            _gunAttributesSO.outOfBullet = true;
            ReloadBulletStack();
        }
    }

    private void ChangeTotalBullet(int amount)
    {
        _gunAttributesSO.totalBullet += amount;
        _totalBulletUIEventSO.RaiseEvent(_gunAttributesSO.totalBullet);
    }
    private IEnumerator WaitForShootTime(float time)
    {
        _gunFireEffect.SetActive(true);
        yield return new WaitForSeconds(time);
        _gunFireEffect.SetActive(false);
        _gunAttributesSO.canShoot = true;
    }

    private IEnumerator WaitForReloading(float time, int reloadAmount)
    {
        _activeReloadTimerEventSO.RaiseEvent(time);
        _gunAudioEventSO.RaiseEvent(_gunAttributesSO.reloadSoundSO);
        yield return new WaitForSeconds(time);
        for (int i = 0; i < reloadAmount; i++)
        {
            GameObject bullet = TakeOut(0);
            bullet.transform.position = _bulletPos.position;
            bullet.SetActive(false);
            _gunAttributesSO.bulletStack.Push(bullet);
        }
        _gunAttributesSO.outOfBullet = false;
        _curBulletEventSO.RaiseEvent(_gunAttributesSO.bulletStack.Count);
        _gunAttributesSO.changeTotalBulletEventSO.RaiseEvent(-reloadAmount);
    }

    // Used to initialize bullet pool in Awake.
    protected virtual void InitBulletPool()
    {
        _gunAttributesSO.listBullet.Clear();
        _gunAttributesSO.totalBullet = _gunAttributesSO.constTotalBullet;
        for (int i = 0; i < _gunAttributesSO.numberBullet + 1; i++)
        {
            GameObject _addBullet = Instantiate(_gunAttributesSO.bulletContainerSO.bulletContainer, _bulletPos.position, Quaternion.identity);
            _addBullet.SetActive(false);
            if(i >= _gunAttributesSO.numberBullet) _gunAttributesSO.listBullet.Add(_addBullet);
            else _gunAttributesSO.bulletStack.Push(_addBullet);
        }
    }


    // Used to reload bullet when bulletStack is empty.
    protected virtual void ReloadBulletStack()
    {
        if(_gunAttributesSO.totalBullet == 0) return;
        
        int reloadAmount = _gunAttributesSO.totalBullet >= _gunAttributesSO.numberBullet ?
                            _gunAttributesSO.numberBullet : _gunAttributesSO.totalBullet;
        
        StartCoroutine(WaitForReloading(_gunAttributesSO.timeReload, reloadAmount));
    }

    // Used to get an object in listBullet and then remove it.
    // Used in ReloadBulletStack method.
    private GameObject TakeOut(int idx)
    {
        GameObject obj = _gunAttributesSO.listBullet[idx];
        _gunAttributesSO.listBullet.RemoveAt(idx);
        return obj;
    }

    



}
