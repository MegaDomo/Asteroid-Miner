using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript2 : MonoBehaviour
{
    public float x;
    public float y;

    private void Update()
    {
        Debug.Log(Mathf.PerlinNoise(x, y));
    }
}
