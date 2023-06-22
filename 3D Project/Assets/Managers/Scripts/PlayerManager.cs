using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerManager", menuName = "Managers/PlayerManager")]
public class PlayerManager : ScriptableObject
{
    [HideInInspector] public Transform player;

    private bool playerHasControl;
    private bool inShip;

    #region Utility
    private void OnEnable()
    {
        SetupPlayer();
    }

    private void SetupPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        SetPlayerHasControl(true);
    }

    public Transform GetPlayer()
    {
        return player;
    }

    public bool IsPlayerInControl()
    {
        return playerHasControl;
    }

    public void SetPlayerHasControl(bool value)
    {
        playerHasControl = value;
    }

    public bool IsInShip()
    {
        return inShip;
    }

    public void SetInShip(bool value)
    {
        inShip = value;
    }
    #endregion
}
