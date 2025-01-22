using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPosList = new List<Transform>();
    [SerializeField] private List<GameObject> _enemyList = new List<GameObject>();
    [SerializeField] private List<GameObject> _bossList = new List<GameObject>();
    [SerializeField] private GameObject _bossPrefab, _enemyPrefabs;
    [SerializeField] private int _initEnemyNum, _initBossNum;
    [SerializeField] private int _curStage = 1;

    // Used to limit enemy on scene.
    private int _maxEnemyOnScene = 50;

    // Used to count the current number of enemy on scene.
    private int _curEnemyOnScene = 0;

    // Used to count the number of died enemy, if this val is 0 => next scene.
    private int _countEnemyDie = 0;

    // Used to track how much enemy will present in particular stage.
    private int _EnemyEachStage = 10;

    private float _delayEachStage = 5f;

    private void Awake() {
        Init();
    }
    private void Init(){
        _enemyList.Clear();
        _bossList.Clear();

        for(int i = 0; i < _initEnemyNum; i++){
            GameObject newEnemy = Instantiate(_enemyPrefabs, transform.position, Quaternion.identity);
            newEnemy.SetActive(false);
            _enemyList.Add(newEnemy);
        }

        for(int i = 0; i < _initBossNum; i++){
            GameObject newBoss = Instantiate(_bossPrefab, transform.position, Quaternion.identity);
            newBoss.SetActive(false);
            _bossList.Add(newBoss);
        }
    }

    private IEnumerator HelpReleasingEnemy(int num, float time, int index, List<GameObject> list){
        while(true){
            yield return new WaitForSeconds(time);
            GameObject enemy = TakeOutObj(list);
            bool delayCondition = enemy == null || _curEnemyOnScene >= _maxEnemyOnScene;
            if(delayCondition) continue;
            enemy.transform.position = _spawnPosList[index].position;
            enemy.SetActive(true);
            _curEnemyOnScene++;
        }
    }

    private GameObject TakeOutObj(List<GameObject> list){
        for(int i = 0; i < list.Count; i++){
            if(!list[i].activeSelf) return list[i];
        }
        return null;
    }
}
