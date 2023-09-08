using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyAroundPlayer : MonoBehaviour
{
    [Header("Unity References")]
    public GameObject enemyPrefab;
    public Transform player;
    public Camera cam;

    [Header("Attributes")]
    public int maxEnemies;
    public float spawnRate;
    public float spawnDistance;
    public float minSpawnDistance;
    
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
            Vector3 spawnPoint = GetSpawnPoint();

            Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);

            yield return new WaitForSeconds(spawnRate);

            enemyCount += 1;
        }
    }

    private Vector3 GetSpawnPoint()
    {
        // use Random.Rotation instead
        xPos = Random.Range(-spawnDistance, spawnDistance) + player.position.x;
        yPos = Random.Range(-spawnDistance, spawnDistance) + player.position.y;
        zPos = Random.Range(-spawnDistance, spawnDistance) + player.position.z;

        Vector3 spawnPoint = new Vector3(xPos, yPos, zPos);
        Vector3 viewPos = cam.WorldToViewportPoint(spawnPoint);

        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            spawnPoint = GetSpawnPoint();
        }

        if (Vector3.Distance(spawnPoint, new Vector3(player.position.x, player.position.y, player.position.z)) < minSpawnDistance)
        {
            if (spawnPoint.x < 0)
                spawnPoint.x += (-minSpawnDistance - spawnPoint.x);
            else
                spawnPoint.x += (minSpawnDistance - spawnPoint.x);

            if (spawnPoint.y < 0)
                spawnPoint.y += (-minSpawnDistance - spawnPoint.y);
            else
                spawnPoint.y += (minSpawnDistance - spawnPoint.y);

            if (spawnPoint.z < 0)
                spawnPoint.z += (-minSpawnDistance - spawnPoint.z);
            else
                spawnPoint.z += (minSpawnDistance - spawnPoint.z);

            /*spawnPoint.x += (minSpawnDistance - spawnPoint.x);
            spawnPoint.y += (minSpawnDistance - spawnPoint.y);
            spawnPoint.z += (minSpawnDistance - spawnPoint.z);*/
        }

        return spawnPoint;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(player.position, spawnDistance);

        Gizmos.color = new Color(0, 1, 0, 1);
        Gizmos.DrawWireSphere(player.position, minSpawnDistance);
    }
}
