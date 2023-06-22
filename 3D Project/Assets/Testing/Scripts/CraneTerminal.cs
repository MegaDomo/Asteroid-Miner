using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneTerminal : MonoBehaviour
{
    [Header("Scriptable References")]
    public PlayerManager playerManager;

    [Header("Unity References")]
    public Interaction interaction;

    public void InteractWithTerminal()
    {
        Debug.Log("Touched Terminal");
    }

    private void OnEnable()
    {
        interaction.interactAction += InteractWithTerminal;
    }

    private void OnDisable()
    {
        interaction.interactAction -= InteractWithTerminal;
    }
}
