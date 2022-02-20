using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{

    public InventoryObject myInventory;
    public ItemType inventoryToDisplay;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    public List<GameObject> myInventoryMenus = new List<GameObject>();

    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    public void CreateDisplay()
    {
        for(int i = 0; i < myInventory.HeadwearInventory.Count; i++)
        {

        }
    }

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
    }

    public void SetInventoryToDisplay(ItemType inType)
    {
        inventoryToDisplay = inType;
    }
}
