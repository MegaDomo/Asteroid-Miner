using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Unity References")]
    public GameObject dropPrefab;
    public AsteroidProperties asteroidProperties;
    public ToolInteraction toolInteraction;

    public void CompleteDrilling()
    {
        Destroy(gameObject);
    }

    private void spawnLoot()
    {
        Instantiate(dropPrefab, transform.position, transform.rotation);
    }

    public float GetTimeToCompleteDrilling()
    {
        return asteroidProperties.timeToDrill;
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            spawnLoot();
        }
    }

    private void OnEnable()
    {
        toolInteraction.toolInteractAction += CompleteDrilling;
        toolInteraction.timeToFinishAction += GetTimeToCompleteDrilling;
    }

    private void OnDisable()
    {
        toolInteraction.toolInteractAction -= CompleteDrilling;
        toolInteraction.timeToFinishAction -= GetTimeToCompleteDrilling;
    }
}
