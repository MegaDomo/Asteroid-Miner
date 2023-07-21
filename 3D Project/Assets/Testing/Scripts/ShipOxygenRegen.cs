using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipOxygenRegen : MonoBehaviour
{
    public PlayerManager manager;
    public PlayerOxygen oxygen;

    private bool inShip;

    // Update is called once per frame
    void Update()
    {
        inShip = manager.IsInShip();

        if (inShip)
            oxygen.InRechargeSpace();
        if (!inShip)
            oxygen.OutRechargeSpace();
    }
}
