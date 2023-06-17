using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidLoot : MonoBehaviour
{
    [Header("Unity References")]
    public Interaction interaction;

    public void PickUpLoot()
    {
        Debug.Log("Loot was picked up");
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        interaction.interactAction += PickUpLoot;
    }

    private void OnDisable()
    {
        interaction.interactAction -= PickUpLoot;
    }
}
