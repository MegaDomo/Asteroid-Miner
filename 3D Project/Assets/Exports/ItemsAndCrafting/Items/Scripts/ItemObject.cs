using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Ingredients,
    Usable,
    Story,
    Extra
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public ItemType type;
    public int maxStackSize = 30;
    [TextArea(15, 20)]
    public string description;
}
