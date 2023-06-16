using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandDrill : MonoBehaviour
{
    public InputAction drillAction;

    private void Update()
    {
        if (drillAction.IsPressed())
        {
            Debug.Log("Being Pressed");
        }
    }

    private void OnEnable()
    {
        drillAction.Enable();
    }

    private void OnDisable()
    {
        drillAction.Disable();
    }
}
