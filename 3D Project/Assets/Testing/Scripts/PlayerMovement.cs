using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Unity References")]
    public Transform camera;

    [Header("Attributes")]
    public float speed = 10f;
    public float jumpForce = 10f;
    public float gravity = 5f;
    public bool cameraFollowMouse;
    public float cameraLookSpeed = 4f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public InputAction mouseInput;
    public InputAction moveAction;
    public InputAction jumpAction;

    private Rigidbody rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();

        jumpAction.performed += ctx => { OnJump(ctx); };
    }

    public void Update()
    {
        Move();
        Camera();
        Gravity();
    }

    private void Move()
    {
        float x = moveAction.ReadValue<Vector2>().x;
        float y = moveAction.ReadValue<Vector2>().y;
        Vector3 direction = new Vector3(x, 0, y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
            if (!cameraFollowMouse)
                targetAngle += camera.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.AddForce(moveDir * speed * Time.deltaTime, ForceMode.Impulse);
        }
    }

    private void Camera()
    {
        if (!cameraFollowMouse)
            return;

        float x = mouseInput.ReadValue<Vector2>().x;

        if (x > 0)
        {
            float y = transform.eulerAngles.y;
            camera.rotation = Quaternion.Euler(new Vector3(0, cameraLookSpeed + y, 0));
        }
            
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Vector3 dir = new Vector3(0, jumpForce, 0);
        rb.AddForce(dir, ForceMode.Impulse);
    }

    private void Gravity()
    {
        Vector3 dir = new Vector3(0, -1, 0);
        transform.Translate(dir * gravity * Time.deltaTime);
    }

    public void OnEnable()
    {
        mouseInput.Enable();
        moveAction.Enable();
        jumpAction.Enable();
    }

    public void OnDisable()
    {
        mouseInput.Disable();
        moveAction.Disable();
        jumpAction.Disable();
    }
}
