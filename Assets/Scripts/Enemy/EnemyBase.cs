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
    [SerializeField] private int _score;
    [SerializeField] private IntEventChannelSO _updateScoreEventSO;

    // Used to change damage taken.
    [SerializeField] private FloatEventChannelSO _DamageEventChannelSO;
    //[SerializeField] protected EnemySpawnManager _enemySpawnManager = EnemySpawnManager.Instance;
    protected bool _isAttack = false;
    private float _damageTaken;
    protected Vector3 _initPos;
    protected virtual void Awake()
    {

        //_playerTransformSO.Init(transform.position);
        _initPos = transform.position;
    }
    protected virtual void Start()
    {
        //_enemySpawnManager = EnemySpawnManager.Instance;
    }
    protected virtual void OnEnable()
    {
        _isAttack = false;
        _damageTaken = _gunManagerSO.curDamage;
        _maxHP *= EnemyScaleDamageSO.scale;
        _curHP = _maxHP;
        _DamageEventChannelSO.OnRaisedEvent += ChangeDamageTaken;

        StartCoroutine(WaitUpdateChasePlayer(_timeToUpdateChase));
    }
    private void OnDisable()
    {
        _DamageEventChannelSO.OnRaisedEvent -= ChangeDamageTaken;
    }
    private bool IsPointOnNavMesh(Vector3 position)
    {
        NavMeshHit hit;
        return NavMesh.SamplePosition(position, out hit, 1f, NavMesh.AllAreas);
    }
    private void ChasePlayer()
    {
        if (!_navMeshAgent.isOnNavMesh)
        {
            transform.position = _initPos;
            return;
        }

        _navMeshAgent.SetDestination(_playerTransformSO.playerPos);

    }
    private IEnumerator WaitUpdateChasePlayer(float time)
    {
        while (true)
        {
            _initPos = _playerTransformSO.playerPos * 2f;
            _initPos.y = _playerTransformSO.playerPos.y;
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
        _rb.AddForce(new Vector3(0f, 0f, dir.forward.z) * hitBack);
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
        _updateScoreEventSO.RaiseEvent(_score);
        //if (_enemySpawnManager != null) _enemySpawnManager.CheckNextStage();
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
            GetHitEffect(other.transform, _damageTaken * Time.deltaTime);
        }
    }
}
