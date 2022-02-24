using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Object", menuName = "InventorySystem/Items/consumable")]

public class ConsumableObject : ItemObject
{

    public int healValue;

    private void Awake()
    {
        type = ItemType.Consumable;
    }
}
