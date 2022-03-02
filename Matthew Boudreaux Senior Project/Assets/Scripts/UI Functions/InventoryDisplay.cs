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
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    public List<GameObject> myInventoryMenus = new List<GameObject>();
    public List<GameObject> buttons;

    //Display selected item info
    public Item myCurrentItem;
    public GameObject myCurrentItemButton;
    public Text selectedItemName;
    public Text selectedItemDescription;
    public Text selectedItemStat1;
    public Text selectedItemStat2;

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
                    if (!itemsDisplayed.ContainsKey(myInventory.WeaponInventory[i]))
                    {
                        GameObject newButton = Instantiate(buttonTemplate);
                        newButton.SetActive(true);
                        newButton.GetComponent<ButtonListButton>().SetItem(myInventory.WeaponInventory[i].item);

                        newButton.GetComponentInChildren<Text>().text = newButton.GetComponent<ButtonListButton>().myItem.Name;

                        newButton.transform.SetParent(myInventoryMenus[0].transform, false);


                        itemsDisplayed.Add(myInventory.WeaponInventory[i], newButton);
                        buttons.Add(newButton);
                    }
                }
                break;

            case ItemType.Shield:
                for (int i = 0; i < myInventory.ShieldInventory.Count; i++)
                {
                    if (!itemsDisplayed.ContainsKey(myInventory.ShieldInventory[i]))
                    {
                        GameObject newButton = Instantiate(buttonTemplate);
                        newButton.SetActive(true);
                        newButton.GetComponent<ButtonListButton>().SetItem(myInventory.ShieldInventory[i].item);


                        newButton.GetComponentInChildren<Text>().text = newButton.GetComponent<ButtonListButton>().myItem.Name;
                        newButton.transform.SetParent(myInventoryMenus[1].transform, false);

                        itemsDisplayed.Add(myInventory.ShieldInventory[i], newButton);
                        buttons.Add(newButton);
                    }
                }
                break;
            case ItemType.Armor:
                for (int i = 0; i < myInventory.ArmorInventory.Count; i++)
                {
                    if (!itemsDisplayed.ContainsKey(myInventory.ArmorInventory[i]))
                    {
                        GameObject newButton = Instantiate(buttonTemplate);
                        newButton.SetActive(true);
                        newButton.GetComponent<ButtonListButton>().SetItem(myInventory.ArmorInventory[i].item);


                        newButton.GetComponentInChildren<Text>().text = newButton.GetComponent<ButtonListButton>().myItem.Name;
                        newButton.transform.SetParent(myInventoryMenus[2].transform, false);

                        itemsDisplayed.Add(myInventory.ArmorInventory[i], newButton);
                        buttons.Add(newButton);
                    }
                }
                break;
            case ItemType.Headwear:
                for (int i = 0; i < myInventory.HeadwearInventory.Count; i++)
                {
                    if (!itemsDisplayed.ContainsKey(myInventory.HeadwearInventory[i]))
                    {
                        GameObject newButton = Instantiate(buttonTemplate);
                        newButton.SetActive(true);
                        newButton.GetComponent<ButtonListButton>().SetItem(myInventory.HeadwearInventory[i].item);


                        newButton.GetComponentInChildren<Text>().text = newButton.GetComponent<ButtonListButton>().myItem.Name;
                        newButton.transform.SetParent(myInventoryMenus[3].transform, false);

                        itemsDisplayed.Add(myInventory.HeadwearInventory[i], newButton);
                        buttons.Add(newButton);
                    }
                }
                break;
            case ItemType.Consumable:
                for (int i = 0; i < myInventory.ConsumableInventory.Count; i++)
                {
                    if (!itemsDisplayed.ContainsKey(myInventory.ConsumableInventory[i]))
                    {
                        GameObject newButton = Instantiate(buttonTemplate);
                        newButton.SetActive(true);
                        newButton.GetComponent<ButtonListButton>().SetItem(myInventory.ConsumableInventory[i].item);

                        newButton.GetComponentInChildren<Text>().text = newButton.GetComponent<ButtonListButton>().myItem.Name + "(" + myInventory.ConsumableInventory[i].amount + ")";
                        
                        newButton.transform.SetParent(myInventoryMenus[4].transform, false);

                        itemsDisplayed.Add(myInventory.ConsumableInventory[i], newButton);
                        buttons.Add(newButton);
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
