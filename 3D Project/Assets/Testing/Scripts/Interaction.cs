using System;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Action interactAction;

    public void Interact()
    {
        interactAction.Invoke();
    }
}
