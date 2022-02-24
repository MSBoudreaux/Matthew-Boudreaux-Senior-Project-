using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Headwear Object", menuName = "InventorySystem/Items/headwear")]

public class HeadwearObject : ItemObject
{
    public int statIncrease;
    public ItemType itemTypeToBuff;

    private void Awake()
    {
        type = ItemType.Headwear;
    }
}
