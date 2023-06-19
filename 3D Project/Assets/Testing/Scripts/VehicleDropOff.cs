using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDropOff : MonoBehaviour
{
    [Header("Scriptable References")]
    public PlayerManager playerManager;

    [Header("Unity References")]
    public Transform playerDropOff;

    private Transform player;

    public void DropOffPlayer()
    {
        player = playerManager.GetPlayer();
        player.gameObject.SetActive(true);

        player.position = playerDropOff.position;
        playerManager.SetPlayerHasControl(true);
    }
}
