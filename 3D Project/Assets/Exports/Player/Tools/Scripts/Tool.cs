using UnityEngine;

public class Tool : ScriptableObject
{
    public enum ToolType { Drill, Repair, Flashlight, Scanner }

    public GameObject prefab;
    public ToolType toolType = ToolType.Drill;

    public virtual void ToolInteraction() { }

    public virtual void ToolInteraction(GameObject tool) { }
}
