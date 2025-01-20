using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> _spawnList = new List<EnemySpawner>();
    [SerializeField] private int _curStage = 1;
    [SerializeField] private int _enemyEachStage;
    [SerializeField] private int _enemyFirstStage = 3;

    // Used to manager the current enemy on scene.
    public int _curEnemy = 0;

    [SerializeField] private IntEventChannelSO _updateStageEventSO;
    [SerializeField] private int _maxEnemyOnScene = 50;
    private float timeDelayEachStage = 5f;
    private bool _delayNextStage = false;

    [SerializeField] private int _countDieEnemy = 0;

    // Singleton
    public static EnemySpawnManager Instance { get; private set; }
    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else{
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        Init();
    }
    private void Start() {
        StartCoroutine(GetNextStage());
        //_spawnList[0].ReleaseFinalEnemy(1, true);
    }
    private void Init(){
        _curEnemy = 0;
        _curStage = 1;
        _enemyEachStage = _enemyFirstStage * _spawnList.Count;
        _updateStageEventSO.RaiseEvent(_curStage);
    }
    private IEnumerator GetNextStage(){
        _delayNextStage = true;
        
        yield return new WaitForSeconds(timeDelayEachStage);
        _countDieEnemy = 0;
        _delayNextStage = false;
        if(_curStage == 1) {
            //_curEnemy = _enemyEachStage;
            _spawnList[0].ReleaseFinalEnemy(_enemyEachStage, false);
        }
        else{
            _enemyEachStage = Mathf.CeilToInt((float)_enemyEachStage * 1.25f);
            //_curEnemy = _enemyEachStage;
            foreach(var spawner in _spawnList){
                yield return new WaitForSeconds(0.7f);
                spawner.ReleaseFinalEnemy(Mathf.CeilToInt(_enemyEachStage / 4), true);
            }
        }
    }
    public void CheckNextStage(){
        if(_delayNextStage) return;
        _curEnemy = _curEnemy > 0? _curEnemy - 1 : 0;
        _countDieEnemy++;
        if(_countDieEnemy >= _enemyEachStage){
            _curStage++;
            _updateStageEventSO.RaiseEvent(_curStage);
            StartCoroutine(GetNextStage());
        }
    }

    public bool CheckCurEnemyOnScene(){
        return _curEnemy <= _maxEnemyOnScene;
    }
}
