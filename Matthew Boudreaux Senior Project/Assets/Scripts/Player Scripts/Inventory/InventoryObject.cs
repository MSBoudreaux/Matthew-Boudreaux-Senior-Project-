using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;


[CreateAssetMenu(fileName = "New Inventory", menuName = "InventorySystem/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver

{

    public string savePath;

    private ItemDatabaseObject database;
    public List<InventorySlot> WeaponInventory = new List<InventorySlot>();
    public List<InventorySlot> ShieldInventory = new List<InventorySlot>();
    public List<InventorySlot> ArmorInventory = new List<InventorySlot>();
    public List<InventorySlot> HeadwearInventory = new List<InventorySlot>();
    public List<InventorySlot> ConsumableInventory = new List<InventorySlot>();

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));
#else
        database = Resources.Load<ItemDatabaseObject>("Database");
#endif
    }

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

    public void OnAfterDeserialize()
    {
        //wep
        for(int i = 0; i < WeaponInventory.Count; i++)
        {
            WeaponInventory[i].item = new Item(database.GetItem[WeaponInventory[i].ID]);
        }
        //shield
        for (int i = 0; i < ShieldInventory.Count; i++)
        {
            ShieldInventory[i].item = new Item(database.GetItem[ShieldInventory[i].ID]);
        }
        //armor
        for (int i = 0; i < ArmorInventory.Count; i++)
        {
            ArmorInventory[i].item = new Item(database.GetItem[ArmorInventory[i].ID]);
        }
        //accessory
        for (int i = 0; i < HeadwearInventory.Count; i++)
        {
            HeadwearInventory[i].item = new Item(database.GetItem[HeadwearInventory[i].ID]);
        }

        //consumable
        for (int i = 0; i < ConsumableInventory.Count; i++)
        {
            ConsumableInventory[i].item = new Item(database.GetItem[ConsumableInventory[i].ID]);
        }
    }

    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();

    }

    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }
    


    public void OnBeforeSerialize()
    {

    }
}
[System.Serializable]
public class InvItemAttributes
{

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
