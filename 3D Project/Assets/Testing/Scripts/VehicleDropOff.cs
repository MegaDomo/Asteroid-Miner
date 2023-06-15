using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDropOff : MonoBehaviour
{
    [Header("Unity References")]
    public Transform playerDropOff;

    [Header("Scriptable References")]
    public PlayerManager playerManager;

    private Transform player;

    public void DropOffPlayer()
    {
        player = playerManager.GetPlayer();
        player.gameObject.SetActive(true);

        player.position = playerDropOff.position;
        playerManager.SetPlayerHasControl(true);
    }
}
