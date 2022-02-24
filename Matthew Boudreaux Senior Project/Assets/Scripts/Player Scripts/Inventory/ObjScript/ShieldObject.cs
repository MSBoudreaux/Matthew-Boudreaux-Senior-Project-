using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shield Object", menuName = "InventorySystem/Items/shield")]

public class ShieldObject : ItemObject
{
    public int blockRating;
    
    private void Awake()
    {
        type = ItemType.Shield;
    }
}
