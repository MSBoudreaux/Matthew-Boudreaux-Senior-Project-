using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListButton : MonoBehaviour
{

    public ItemObject itemToDisplay;
    public ItemDatabaseObject myItemList;
    public InventoryDisplay myInventoryDisplay;

    [SerializeField]
    public Item myItem;

    public void Awake()
    {
        myInventoryDisplay = GameObject.Find("InventoryMenu").GetComponent<InventoryDisplay>();
    }


    public void SetItem(Item _item)
    {
        myItem = _item;
    }
    public void OnClick()
    {
        itemToDisplay = myItemList.Items[myItem.Id];
        myInventoryDisplay.selectedItemName.text = myItem.Name;
        myInventoryDisplay.selectedItemDescription.text = itemToDisplay.description;

        switch (itemToDisplay.type)
        {
            case ItemType.Weapon:
                WeaponObject wepToDisplay = (WeaponObject)myItemList.Items[myItem.Id];
                myInventoryDisplay.selectedItemStat1.text = "Base Damage : " + wepToDisplay.damage.ToString();
                break;
            case ItemType.Shield:
                ShieldObject shieldToDisplay = (ShieldObject)myItemList.Items[myItem.Id];
                myInventoryDisplay.selectedItemStat1.text = "Block Rating : " + shieldToDisplay.blockRating.ToString();
                break;
            case ItemType.Armor:
                ArmorObject armorToDisplay = (ArmorObject)myItemList.Items[myItem.Id];
                myInventoryDisplay.selectedItemStat1.text = "Armor Rating : " + armorToDisplay.defenseRating.ToString();
                break;
            case ItemType.Headwear:
                break;
            case ItemType.Consumable:
                ConsumableObject consumableToDisplay = (ConsumableObject)myItemList.Items[myItem.Id];
                if(consumableToDisplay.healValue != 0)
                {
                    
                }
                break;




        }





    }
}
