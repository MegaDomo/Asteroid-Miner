using UnityEngine;

[CreateAssetMenu(fileName = "NewTool", menuName = "Items/Tool")]
public class Tool : ScriptableObject
{
    public enum ToolType { Drill, Repair }

    public GameObject prefab;
    public ToolType toolType = ToolType.Drill;

    public virtual void ToolInteraction() { }
}
