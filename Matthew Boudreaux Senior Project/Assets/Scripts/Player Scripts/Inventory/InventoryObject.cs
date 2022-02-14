using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Inventory", menuName = "InventorySystem/Inventory")]
public class InventoryObject : ScriptableObject

{
    public List<InventorySlot> WeaponInventory = new List<InventorySlot>();
    public List<InventorySlot> ShieldInventory = new List<InventorySlot>();
    public List<InventorySlot> ArmorInventory = new List<InventorySlot>();
    public List<InventorySlot> HeadwearInventory = new List<InventorySlot>();

    public void AddItem(ItemObject _item)
    {
        if(_item.type == ItemType.Weapon)
        {
            WeaponInventory.Add(new InventorySlot(_item));
        }
        else if (_item.type == ItemType.Shield)
        {
            ShieldInventory.Add(new InventorySlot(_item));

        }
        else if (_item.type == ItemType.Armor)
        {
            ArmorInventory.Add(new InventorySlot(_item));

        }
        else if (_item.type == ItemType.Headwear)
        {
            HeadwearInventory.Add(new InventorySlot(_item));

        }
    }
}


[System.Serializable]
public class InventorySlot
{
    public ItemObject item;

    public InventorySlot(ItemObject _item)
    {
        item = _item;
    }
}
