using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Inventory", menuName = "InventorySystem/Inventory")]
public class InventoryObject : ScriptableObject

{
    public ItemDatabaseObject database;
    public List<InventorySlot> WeaponInventory = new List<InventorySlot>();
    public List<InventorySlot> ShieldInventory = new List<InventorySlot>();
    public List<InventorySlot> ArmorInventory = new List<InventorySlot>();
    public List<InventorySlot> HeadwearInventory = new List<InventorySlot>();
    public List<InventorySlot> ConsumableInventory = new List<InventorySlot>();

    public void AddItem(Item _item)
    {
        if(_item.type == ItemType.Weapon)
        {
            WeaponInventory.Add(new InventorySlot(_item.Id, _item, _item.amount));
        }
        else if (_item.type == ItemType.Shield)
        {
            ShieldInventory.Add(new InventorySlot(_item.Id, _item, _item.amount));

        }
        else if (_item.type == ItemType.Armor)
        {
            ArmorInventory.Add(new InventorySlot(_item.Id, _item, _item.amount));

        }
        else if (_item.type == ItemType.Headwear)
        {
            HeadwearInventory.Add(new InventorySlot(_item.Id, _item, _item.amount));

        }
        else if(_item.type == ItemType.Consumable)
        {
            for(int i = 0; i < ConsumableInventory.Count; i++)
            {
                if(ConsumableInventory[i].item.Id == _item.Id)
                {
                    ConsumableInventory[i].item.amount += _item.amount;
                    return;
                }
            }
            ConsumableInventory.Add(new InventorySlot(_item.Id, _item, _item.amount));
        }
    }

    public void RemoveItem(InventorySlot _slot)
    {

            if(_slot.item.type == ItemType.Weapon)
            {
                WeaponInventory.Remove(_slot);
            }
            else if(_slot.item.type == ItemType.Shield)
            {
                ShieldInventory.Remove(_slot);
            }
            else if(_slot.item.type == ItemType.Armor)
            {
                ArmorInventory.Remove(_slot);
            }
            else if(_slot.item.type == ItemType.Headwear)
            {
                HeadwearInventory.Remove(_slot);
            }
            else if(_slot.item.type == ItemType.Consumable)
            {
                ConsumableInventory.Remove(_slot);
            }
        
    }
}


[System.Serializable]
public class InventorySlot
{
    public int ID;
    public Item item;
    public int amount;

    public InventorySlot(int _id, Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
}
