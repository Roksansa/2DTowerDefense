using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public Vector3 SpawnPoint;
    public GameObject EnemyPrefab;
    public int NumberOfEnemiesToSpawn = 50;
    public float MinSpawnTime = 1f;
    public float MaxSpawnTime = 3f;
    public static int CurEnemy;

    private void Start()
    {
        CurEnemy = NumberOfEnemiesToSpawn;
        StartCoroutine(SpawnEnemies(NumberOfEnemiesToSpawn));
    }

    private IEnumerator SpawnEnemies(int number)
    {
        for (int i = 0; i < number; i++) {
            Instantiate(EnemyPrefab, SpawnPoint, Quaternion.identity);
            float ratio = i*1f/(number - 1);
            float timeTowait = Mathf.Lerp(MinSpawnTime, MaxSpawnTime, 1 - ratio);
            yield return new WaitForSeconds(timeTowait);
        }
        bool isGameOver = false;
        while (!isGameOver) {
            if (CurEnemy <= 0) {
                isGameOver = true;
                //GameOver Screen (win)
            }
            else {
                yield return new WaitForSeconds(1);
            }
        }
    }
}