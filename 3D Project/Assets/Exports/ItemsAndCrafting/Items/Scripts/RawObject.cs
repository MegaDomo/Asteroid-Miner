using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRawItem", menuName = "Items/Raw Item")]
public class RawObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Raw;
    }
}
