using System;
using UnityEngine;

public class ToolInteraction : MonoBehaviour
{
    public Action toolInteractAction;

    public void ToolInteract()
    {
        toolInteractAction.Invoke();
    }
}
