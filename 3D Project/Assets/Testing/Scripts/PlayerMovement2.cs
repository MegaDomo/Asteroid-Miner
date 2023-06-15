using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2 : MonoBehaviour
{
    [Header("Unity References")]
    public Transform cam;
    public Rigidbody rb;

    [Header("Attributes")]
    public float speed = 5f;

    public InputAction moveAction;

    private void Update()
    {
        float x = moveAction.ReadValue<Vector2>().x;
        float y = moveAction.ReadValue<Vector2>().y;
        Vector3 direction = new Vector3(x, 0, y);
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(x, y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            Vector3 dir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            rb.MovePosition(rb.position + dir * speed * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }
}
