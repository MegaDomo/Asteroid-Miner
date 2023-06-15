using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement3 : MonoBehaviour
{
    [Header("Unity References")]
    public Transform cam;
    public CharacterController controller;

    [Header("Attributes")]
    public float speed = 5f;

    public InputAction moveAction;

    public float turnSmoothTime;
    private float turnSmoothVelocity;

    private void Update()
    {
        float x = moveAction.ReadValue<Vector2>().x;
        float y = moveAction.ReadValue<Vector2>().y;
        Vector3 direction = new Vector3(x, 0, y);
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(x, y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            Vector3 dir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));


            controller.Move(dir * speed * Time.deltaTime);
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
