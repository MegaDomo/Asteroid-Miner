using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIngredientItem", menuName = "Items/Ingredient")]
public class IngredientObject : ItemObject
{

    private void Awake()
    {
        type = ItemType.Ingredients;
    }
}
