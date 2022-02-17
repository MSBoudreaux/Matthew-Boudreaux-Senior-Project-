using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Shield,
    Headwear,
    Armor,
    Consumable,
    Default

}

public abstract class ItemObject : ScriptableObject
{
    public string Name;
    public int Id;
    public GameObject prefab;
    public ItemType type;
    public Sprite uiDisplay;
    [TextArea(15,20)]
    public string description;
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public ItemType type;
    public Item(ItemObject _item)
    {
        Name = _item.name;
        Id = _item.Id;
        type = _item.type;
    }
}




