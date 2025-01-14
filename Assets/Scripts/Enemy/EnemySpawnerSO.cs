using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnerSO", menuName = "Enemy/EnemySpawnerSO")]
public class EnemySpawnerSO : ScriptableObject {
    public int initNum;
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public float delaySpawnEnemy;
}
