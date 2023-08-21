using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewComponentItem", menuName = "Items/Component")]
public class ComponentObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Component;
    }
}
