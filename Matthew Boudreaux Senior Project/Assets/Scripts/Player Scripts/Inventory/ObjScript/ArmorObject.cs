using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Object", menuName = "InventorySystem/Items/armor")]

public class ArmorObject : ItemObject
{

    private void Awake()
    {
        type = ItemType.Armor;
    }
}
