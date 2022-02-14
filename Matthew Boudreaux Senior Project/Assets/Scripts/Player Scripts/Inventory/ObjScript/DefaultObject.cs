using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "InventorySystem/Items/default") ]
public class DefaultObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Default;
    }
}
