using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> _spawnList = new List<EnemySpawner>();
    [SerializeField] private int _curStage = 1;
    [SerializeField] private int _enemyEachStage = 10;
    [SerializeField] private int _curEnemy = 0;
    [SerializeField] private IntEventChannelSO _updateStageEventSO;
    private float timeDelayEachStage = 5f;
    private bool _delayNextStage = false;

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
        _enemyEachStage = 10;
        _updateStageEventSO.RaiseEvent(_curStage);
    }
    private IEnumerator GetNextStage(){
        _delayNextStage = true;
        
        yield return new WaitForSeconds(timeDelayEachStage);
        _delayNextStage = false;
        if(_curStage == 1) {
            _curEnemy = _enemyEachStage;
            _spawnList[0].ReleaseFinalEnemy(10, false);
        }
        else{
            _enemyEachStage = Mathf.CeilToInt((float)_enemyEachStage * 1.25f);
            _curEnemy = _enemyEachStage;
            foreach(var spawner in _spawnList){
                spawner.ReleaseFinalEnemy(_enemyEachStage, true);
            }
        }
    }
    public void CheckNextStage(){
        if(_delayNextStage) return;
        _curEnemy--;
        if(_curEnemy <= 0){
            _curStage++;
            _updateStageEventSO.RaiseEvent(_curStage);
            StartCoroutine(GetNextStage());
        }
    }
}
