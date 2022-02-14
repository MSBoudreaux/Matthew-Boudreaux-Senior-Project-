using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Headwear Object", menuName = "InventorySystem/Items/headwear")]

public class HeadwearObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Headwear;
    }
}
