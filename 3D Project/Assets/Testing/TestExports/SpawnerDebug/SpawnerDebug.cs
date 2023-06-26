using UnityEngine;

public class SpawnerDebug : MonoBehaviour
{
    [Header("Unity References")]
    public GameObject prefabToSpawn;

    private void Start()
    {
        if (prefabToSpawn)
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }
}
