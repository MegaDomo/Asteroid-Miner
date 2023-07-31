using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript2 : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 vec = Utils.GetMouseWorldPosition();
            Debug.Log(vec);
            Utils.CreateWorldText(vec, "Here", 40, TextAnchor.MiddleCenter);
        }
    }
}
