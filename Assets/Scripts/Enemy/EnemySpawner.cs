using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawnerSO _enemySpawnerSO;
    [SerializeField] private List<GameObject> _enemyList = new List<GameObject>();
    [SerializeField] private List<GameObject> _bossList = new List<GameObject>();
    private Coroutine _enemyCrt, _bossCrt;
    private EnemySpawnManager _enemySpawnManager;
    private void Awake()
    {
        //_enemySpawnManager = EnemySpawnManager.Instance;
        Init();
    }
    private void OnEnable()
    {
        //ReleaseFinalEnemy(50, true);
    }
    private void Init()
    {
        _enemyList.Clear();
        _bossList.Clear();
        // Initialize enemy.
        for (int i = 0; i < _enemySpawnerSO.initNum; i++)
        {
            GameObject newEnemy = Instantiate(_enemySpawnerSO.enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.SetActive(false);
            _enemyList.Add(newEnemy);
        }

        // Initialize boss.
        int bossNum = Mathf.Clamp(_enemySpawnerSO.initNum / 10, 1, 20);
        for (int i = 0; i < bossNum; i++)
        {
            GameObject bossEnemy = Instantiate(_enemySpawnerSO.bossPrefab, transform.position, Quaternion.identity);
            bossEnemy.SetActive(false);
            _bossList.Add(bossEnemy);
        }
    }

    public void ReleaseFinalEnemy(int num, bool hasBoss = false)
    {
        _enemyCrt = StartCoroutine(HelpReleasingEnemy(num, _enemySpawnerSO.delaySpawnEnemy));
        if (hasBoss)
        {
            float timeSpawner = Mathf.Clamp(_enemySpawnerSO.delaySpawnEnemy * (num / 10), 3, 10);
            _bossCrt = StartCoroutine(HelpReleasingBoss(num, timeSpawner));
        }
    }
    private IEnumerator HelpReleasingEnemy(int num, float time)
    {
        for (int i = 0; i < num; i++)
        {
            yield return new WaitForSeconds(time);

            //if (!_enemySpawnManager.CheckCurEnemyOnScene())
            {
                //StopCoroutine(_enemyCrt);
                //StopAllCoroutines();
                i--;
            }
            //else
            {
                GameObject enemy = GetEnemy();
                if (enemy != null)
                {
                    enemy.transform.position = transform.position;
                    enemy.SetActive(true);
                    //_enemySpawnManager._curEnemy++;
                }
                else i--;
            }
        }
    }
    private IEnumerator HelpReleasingBoss(int num, float time)
    {
        int bossNum = Mathf.Clamp(num / 10, 1, 20);
        for (int i = 0; i < bossNum; i++)
        {
            yield return new WaitForSeconds(time);
            GameObject boss = GetBoss();
            if (boss != null)
            {
                boss.transform.position = transform.position;
                boss.SetActive(true);
            }
            else i--;
        }
    }
    private GameObject GetEnemy()
    {
        for (int i = 0; i < _enemyList.Count; i++)
        {
            if (!_enemyList[i].activeSelf) return _enemyList[i];
        }
        return null;
    }
    private GameObject GetBoss()
    {
        for (int i = 0; i < _bossList.Count; i++)
        {
            if (!_bossList[i].activeSelf) return _bossList[i];
        }
        return null;
    }

}
