using System;
using UnityEngine;

public class ToolInteraction : MonoBehaviour
{
    public Tool.ToolType toolType;
    
    public Action toolInteractAction;
    public Func<float> timeToFinishAction;

    public void ToolInteractionComplete()
    {
        toolInteractAction.Invoke();
    }

    public float GetTimeToFinish()
    {
        return timeToFinishAction.Invoke();
    }
}
