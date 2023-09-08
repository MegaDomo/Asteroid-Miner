using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyAroundPlayer : MonoBehaviour
{
    [Header("Unity References")]
    public GameObject enemyPrefab;
    public Transform player;

    [Header("Attributes")]
    public int maxEnemies;
    public float spawnRate;
    public int spawnDistance;
    
    private int enemyCount = 0;
    
    private float xPos;
    private float yPos;
    private float zPos;

    void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        while (enemyCount < maxEnemies)
        {
            xPos = Random.Range(-spawnDistance, spawnDistance) + player.position.x;
            yPos = Random.Range(-spawnDistance, spawnDistance) + player.position.y;
            zPos = Random.Range(-spawnDistance, spawnDistance) + player.position.z;

            Instantiate(enemyPrefab, new Vector3(xPos, yPos, zPos), Quaternion.identity);

            yield return new WaitForSeconds(spawnRate);

            enemyCount += 1;
        }
    }
}
