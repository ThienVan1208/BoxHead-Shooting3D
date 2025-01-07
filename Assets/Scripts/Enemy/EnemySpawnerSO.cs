using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnerSO", menuName = "Enemy/EnemySpawnerSO")]
public class EnemySpawnerSO : ScriptableObject {
    public int initNum;
    public GameObject enemyPrefab;
    public List<GameObject> enemySpawnerList = new List<GameObject>();
}
