using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Object", menuName = "InventorySystem/Items/weapon")]

public class WeaponObject : ItemObject
{
    public int damage;
    public bool isTwoHanded;

    private void Awake()
    {
        type = ItemType.Weapon;
    }
}
