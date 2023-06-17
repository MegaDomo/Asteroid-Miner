using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Unity References")]
    public GameObject dropPrefab;

    public void DestroyAsteroid()
    {
        Destroy(gameObject);
    }

    private void spawnLoot()
    {
        Instantiate(dropPrefab, transform.position, transform.rotation);
    }

    private void OnDestroy()
    {
        spawnLoot();
    }
}
