using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Object", menuName = "InventorySystem/Items/consumable")]

public class ConsumableObject : ItemObject
{

    public bool IsConsumable;

    public int healValue;
    //true is health, false is stress
    public bool healType;
    public string ItemDescription;

    private void Awake()
    {
        type = ItemType.Consumable;
    }
}
