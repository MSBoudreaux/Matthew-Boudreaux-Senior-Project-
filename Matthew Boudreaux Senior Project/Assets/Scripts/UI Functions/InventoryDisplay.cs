using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField]
    public GameObject buttonTemplate;

    public InventoryObject myInventory;
    public ItemType inventoryToDisplay;
    public int currentInventory;
    public Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    public List<GameObject> myInventoryMenus = new List<GameObject>();
    public List<GameObject> buttons;

    //Display selected item info
    public Item myCurrentItem;
    public GameObject myCurrentItemButton;
    public Text selectedItemName;
    public Text selectedItemDescription;
    public Text selectedItemStat1;
    public Text selectedItemStat2;

    GameObject buttonToRemove;


    void Start()
    {
        //CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    /*public void CreateDisplay()
    {
        switch (inventoryToDisplay)
        {
            case ItemType.Weapon:
                for(int i = 0; i < myInventory.WeaponInventory.Count; i++)
                {
                    GameObject obj = Instantiate(buttonTemplate);
                    obj.SetActive(true);
                    obj.GetComponent<ButtonListButton>().SetItem(myInventory.WeaponInventory[i].item);
                    buttons.Add(obj);
                }
                break;
            case ItemType.Shield:
                break;
            case ItemType.Armor:
                break;
            case ItemType.Headwear:
                break;
            case ItemType.Consumable:
                break;
        }
    }*/

    public void UpdateDisplay()
    {
        for(int i = 0; i < myInventoryMenus.Count; i++)
        {
            myInventoryMenus[i].SetActive(false);
            if(myInventoryMenus[i].GetComponent<SubInventoryDisplay>().myType == inventoryToDisplay)
            {
                myInventoryMenus[i].SetActive(true);
            }
        }

        switch (inventoryToDisplay)
        {
            case ItemType.Weapon:

                for (int i = 0; i < myInventory.WeaponInventory.Count; i++)
                {
                    if (!itemsDisplayed.ContainsValue(myInventory.WeaponInventory[i]))
                    {
                        GameObject newButton = Instantiate(buttonTemplate);
                        newButton.SetActive(true);
                        newButton.GetComponent<ButtonListButton>().SetItem(myInventory.WeaponInventory[i].item);

                        newButton.GetComponentInChildren<Text>().text = newButton.GetComponent<ButtonListButton>().myItem.Name;

                        newButton.transform.SetParent(myInventoryMenus[0].transform, false);


                        itemsDisplayed.Add(newButton, myInventory.WeaponInventory[i]);
                        buttons.Add(newButton);
                    }
                }
                break;

            case ItemType.Shield:
                for (int i = 0; i < myInventory.ShieldInventory.Count; i++)
                {
                    if (!itemsDisplayed.ContainsValue(myInventory.ShieldInventory[i]))
                    {
                        GameObject newButton = Instantiate(buttonTemplate);
                        newButton.SetActive(true);
                        newButton.GetComponent<ButtonListButton>().SetItem(myInventory.ShieldInventory[i].item);


                        newButton.GetComponentInChildren<Text>().text = newButton.GetComponent<ButtonListButton>().myItem.Name;
                        newButton.transform.SetParent(myInventoryMenus[1].transform, false);

                        itemsDisplayed.Add(newButton, myInventory.ShieldInventory[i]);
                        buttons.Add(newButton);
                    }
                }
                break;
            case ItemType.Armor:
                for (int i = 0; i < myInventory.ArmorInventory.Count; i++)
                {
                    if (!itemsDisplayed.ContainsValue(myInventory.ArmorInventory[i]))
                    {
                        GameObject newButton = Instantiate(buttonTemplate);
                        newButton.SetActive(true);
                        newButton.GetComponent<ButtonListButton>().SetItem(myInventory.ArmorInventory[i].item);


                        newButton.GetComponentInChildren<Text>().text = newButton.GetComponent<ButtonListButton>().myItem.Name;
                        newButton.transform.SetParent(myInventoryMenus[2].transform, false);

                        itemsDisplayed.Add(newButton, myInventory.ArmorInventory[i]);
                        buttons.Add(newButton);
                    }
                }
                break;
            case ItemType.Headwear:
                for (int i = 0; i < myInventory.HeadwearInventory.Count; i++)
                {
                    if (!itemsDisplayed.ContainsValue(myInventory.HeadwearInventory[i]))
                    {
                        GameObject newButton = Instantiate(buttonTemplate);
                        newButton.SetActive(true);
                        newButton.GetComponent<ButtonListButton>().SetItem(myInventory.HeadwearInventory[i].item);


                        newButton.GetComponentInChildren<Text>().text = newButton.GetComponent<ButtonListButton>().myItem.Name;
                        newButton.transform.SetParent(myInventoryMenus[3].transform, false);

                        itemsDisplayed.Add(newButton, myInventory.HeadwearInventory[i] );
                        buttons.Add(newButton);
                    }
                }
                break;
            case ItemType.Consumable:
                for (int i = 0; i < myInventory.ConsumableInventory.Count; i++)
                {
                    if (!itemsDisplayed.ContainsValue(myInventory.ConsumableInventory[i]))
                    {
                        if (myInventory.ConsumableInventory[i].item.amount == 0)
                        {
                            
                            foreach(var j in itemsDisplayed)
                            {
                                if(j.Value == myInventory.ConsumableInventory[i])
                                {
                                    buttonToRemove = j.Key;
                                    itemsDisplayed.Remove(buttonToRemove);
                                    buttons.Remove(buttonToRemove);
                                    myInventory.RemoveItem(myInventory.ConsumableInventory[i]);
                                    Destroy(buttonToRemove);

                                }
                            }
                            break;
                        }

                        GameObject newButton = Instantiate(buttonTemplate);
                        newButton.SetActive(true);
                        newButton.GetComponent<ButtonListButton>().SetItem(myInventory.ConsumableInventory[i].item);

                        newButton.GetComponentInChildren<Text>().text = newButton.GetComponent<ButtonListButton>().myItem.Name + "(" + myInventory.ConsumableInventory[i].item.amount + ")";

                        newButton.transform.SetParent(myInventoryMenus[4].transform, false);

                        itemsDisplayed.Add(newButton, myInventory.ConsumableInventory[i]);
                        buttons.Add(newButton);
                    }

                    if (myInventory.ConsumableInventory[i].amount == 0)
                    {

                        itemsDisplayed.Remove(buttons[i]);
                        buttons.Remove(buttons[i]);
                        myInventory.RemoveItem(myInventory.ConsumableInventory[i]);
                    }
                }
                break;
        }
            
    }

    public void SetInventoryToDisplay(ItemType inType)
    {
        inventoryToDisplay = inType;
    }

    public void SetCurrentItem(Item _item)
    {
        myCurrentItem = _item;
    }
}
