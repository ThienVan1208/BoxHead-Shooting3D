using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawnerSO _enemySpawnerSO;
    [SerializeField] private List<Transform> _spawnPos;
    private int realNum;
    private void Awake() {
        Init();
    }
    private void Start() {
        ActiveSpawnPos(0, 50, 2f);
    }
    private void Init(){
        realNum = _enemySpawnerSO.initNum * _spawnPos.Count;
        _enemySpawnerSO.enemySpawnerList.Clear();
        for(int i = 0; i < realNum; i++){
            GameObject newEnemy = Instantiate(_enemySpawnerSO.enemyPrefab, _spawnPos[i % _spawnPos.Count].position, Quaternion.identity);
            _enemySpawnerSO.enemySpawnerList.Add(newEnemy);
            newEnemy.SetActive(false);
        }
    }
    private void ActiveSpawnPos(int index, int num, float time){
        StartCoroutine(WaitSpawner(index, num * _spawnPos.Count, time));
    }
    private IEnumerator WaitSpawner(int index, int num, float time){
        for(int i = 0; i < num; i++){
            if(index == (i % _spawnPos.Count)){
                _enemySpawnerSO.enemySpawnerList[i].transform.position = _spawnPos[index].position;
                yield return new WaitForSeconds(time);
                _enemySpawnerSO.enemySpawnerList[i].SetActive(true);
            }
        }
    }
    
}
