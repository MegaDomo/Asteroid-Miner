using UnityEngine;

public class Tool : ScriptableObject
{
    public enum ToolType { Drill, Repair, Ship }

    public GameObject prefab;
    public ToolType toolType = ToolType.Drill;

    public virtual void ToolInteraction() { }
}
