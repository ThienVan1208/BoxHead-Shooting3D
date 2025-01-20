using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyBase
{
    [SerializeField] private int _bulletNum;
    [SerializeField] private float _timeAttack, _timeReload;
    [SerializeField] private bool _outOfBullet = false;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private List<GameObject> _bulletPool = new List<GameObject>();
    [SerializeField] private Transform _bulletPos;
    [SerializeField] private BossAudio _bossAudio;
    private Stack<GameObject> _bulletStack = new Stack<GameObject>();
    private Coroutine _crt;
    protected override void Awake(){
        base.Awake();
        Init();
    }
    protected override void OnEnable(){
        base.OnEnable();
        if(_outOfBullet) StartCoroutine(WaitForReloading(_timeReload));
    }
    private void Init()
    {
        _outOfBullet = false;
        for (int i = 0; i < _bulletNum; i++)
        {
            GameObject newBullet = Instantiate(_bulletPrefab, Vector3.zero, Quaternion.identity);
            newBullet.SetActive(false);
            _bulletStack.Push(newBullet);
        }
    }

    // Use StartCorountine(WaitForReloading(time)) instead of ReloadBullet method.
    private void ReloadBullet(){
        for (int i = 0; i < _bulletPool.Count; i++){
            GameObject newBullet = _bulletPool[0];
            newBullet.SetActive(false);
            _bulletStack.Push(newBullet);
            _bulletPool.RemoveAt(0);
        }
    }
    private IEnumerator WaitForReloading(float time){
        yield return new WaitForSeconds(time);
        ReloadBullet();
        _outOfBullet = false;
    }


    
    private void HelpAttacking()
    {
        if(_outOfBullet) return;

        _isAttack = true;
        _bossAudio.GetShootAudio();
        if (_bulletStack.Count != 0)
        {
            GameObject usedBullet = _bulletStack.Pop();
            usedBullet.transform.forward = transform.forward;
            usedBullet.transform.position = _bulletPos.position;
            
            usedBullet.SetActive(true);
            _bulletPool.Add(usedBullet);
        }
        else{
            _outOfBullet = true;
            _isAttack = false;
            StartCoroutine(WaitForReloading(_timeReload));
        }

    }
    private IEnumerator AttackMain(float time){
        while(true){
            HelpAttacking();
            yield return new WaitForSeconds(time);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == Constant.TAG_PLAYER){
            _crt = StartCoroutine(AttackMain(_timeAttack));
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == Constant.TAG_PLAYER){
            StopCoroutine(_crt);
        }
    }
}
