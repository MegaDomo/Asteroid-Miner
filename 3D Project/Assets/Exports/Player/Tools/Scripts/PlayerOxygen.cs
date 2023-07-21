using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOxygen : MonoBehaviour
{
    [Header("Attributes")]
    public float maxOxygen = 10f;
    public float currentOxygen;
    public float defaultDrainValue = 1.5f;
    public float defaultDrainRate = 1;
    public float defaultRechargeValue = 1f;
    public float defaultRechargeRate = 2f;
    public bool safeSpace = false;
    public bool rechargeSpace = false;
    public bool draining = false;
    public bool recharging = false;

    private float drainValue;
    private float drainRate;
    private float rechargeValue;
    private float rechargeRate;

    public void Start()
    {
        currentOxygen = maxOxygen;
        ResetToDefaults();
    }

    public void Update()
    {
        if (!safeSpace && !draining)
        {
            draining = true;
            StartCoroutine("DrainOxygen");
        }

        if (rechargeSpace && !recharging)
        {
            recharging = true;
            StartCoroutine("RechargeOxygen");
        }
    }

    #region Enum
    IEnumerator DrainOxygen()
    {
        if (currentOxygen > 0f)
            currentOxygen -= drainValue;

        if (currentOxygen < 0f)
            currentOxygen = 0f;

        yield return new WaitForSeconds(drainRate);
        draining = false;
    }

    IEnumerator RechargeOxygen()
    {
        if (currentOxygen < maxOxygen)
            currentOxygen += rechargeValue;

        if (currentOxygen > maxOxygen)
            currentOxygen = maxOxygen;

        yield return new WaitForSeconds(rechargeRate);
        recharging = false; ;
    }
    #endregion

    #region Utility
    private void ResetToDefaults()
    {
        drainValue = defaultDrainValue;
        drainRate = defaultDrainRate;
        rechargeValue = defaultRechargeValue;
        rechargeRate = defaultRechargeRate;
    }
    public void InSafeSpace()
    {
        safeSpace = true;
    }
    public void OutSafeSpace()
    {
        safeSpace = false;
    }
    public void InRechargeSpace()
    {
        safeSpace = true;
        rechargeSpace = true;
    }
    public void OutRechargeSpace()
    {
        safeSpace = false;
        rechargeSpace = false;
    }
    public void setDrain(float value, float rate)
    {
        drainValue = value;
        drainRate = rate;
    }
    public void setRecharge(float value, float rate)
    {
        rechargeValue = value;
        rechargeRate = rate;
    }
    #endregion
}
