using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _curHP, _maxHP;
    [SerializeField] private float _hitBackForce;
    [SerializeField] private PlayerTransformSO _playerTransformSO;
    [SerializeField] private GunManagerSO _gunManagerSO;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private float _timeToUpdateChase = 1f;

    // Used to change damage taken.
    [SerializeField] private FloatEventChannelSO _DamageEventChannelSO;
    protected bool _isAttack = false;
    private float _damageTaken;
    protected virtual void Awake(){
        _playerTransformSO.Init(transform);
    }
    protected virtual void OnEnable()
    {
        _isAttack = false;
        _damageTaken = _gunManagerSO.curDamage;
        _curHP = _maxHP;
        _DamageEventChannelSO.OnRaisedEvent += ChangeDamageTaken;

        StartCoroutine(WaitUpdateChasePlayer(_timeToUpdateChase));
    }
    private void OnDisable()
    {
        _DamageEventChannelSO.OnRaisedEvent -= ChangeDamageTaken;
    }

    private void ChasePlayer()
    {
        // float curSpeed = _isAttack ? 0 : _speed;
        // //if(_isAttack) return;
        // Vector3 dir = _playerTransformSO.playerTransform.transform.position - transform.position;
        // _rb.velocity = dir * curSpeed;
        // transform.forward = -dir;
        _navMeshAgent.SetDestination(_playerTransformSO.playerTransform.position);
    }
    private IEnumerator WaitUpdateChasePlayer(float time)
    {
        while (true)
        {
            ChasePlayer();
            yield return new WaitForSeconds(time);
        }
    }
    // private void Update()
    // {
    //     ChasePlayer();
    // }
    private void ChangeDamageTaken(float damageTaken)
    {
        _damageTaken = damageTaken;
    }
    private void GetHitEffect(Transform dir, float hitBack)
    {
        _rb.AddForce(dir.forward * hitBack, ForceMode.Impulse);
        _curHP -= _damageTaken;
        CheckDie();
    }
    private void CheckDie()
    {
        if (_curHP <= 0)
        {
            _curHP = 0;
            Die();
        }
    }
    private void Die()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Constant.TAG_BULLET)
        {
            GetHitEffect(other.transform, _hitBackForce);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == Constant.TAG_LAZER_BULLET)
        {
            GetHitEffect(other.transform, _damageTaken);
        }
    }
}
