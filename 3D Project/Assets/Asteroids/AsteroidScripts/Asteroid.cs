using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IToolInteraction
{
    [Header("Unity References")]
    public GameObject dropPrefab;
    public AsteroidProperties asteroidProperties;
    public ToolInteraction toolInteraction;

    private void spawnLoot()
    {
        Instantiate(dropPrefab, transform.position, transform.rotation);
    }

    public void CompleteInteraction()
    {
        Destroy(gameObject);
    }

    public float GetCompletionTime()
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
        toolInteraction.toolInteractAction += CompleteInteraction;
        toolInteraction.timeToFinishAction += GetCompletionTime;
    }

    private void OnDisable()
    {
        toolInteraction.toolInteractAction -= CompleteInteraction;
        toolInteraction.timeToFinishAction -= GetCompletionTime;
    }
}
