using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerManager", menuName = "Managers/PlayerManager")]
public class PlayerManager : ScriptableObject
{
    private Transform player;
    private Camera playerCam;

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
        playerCam = player.GetComponent<FirstPersonPlayerMovement>()?.cam.GetComponent<Camera>();
        SetPlayerHasControl(true);
    }

    public Transform GetPlayer()
    {
        return player;
    }

    public Camera GetPlayerCamera()
    {
        return playerCam;
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
