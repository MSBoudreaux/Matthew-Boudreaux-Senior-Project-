using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipButton : MonoBehaviour
{


    public PlayerStats myStats;
    public InventoryDisplay myInventory;


    public void OnClick()
    {
        myStats.Equip(myInventory.myCurrentItem);
        foreach(GameObject button in myInventory.buttons)
        {
            if(button.GetComponent<ButtonListButton>().myItem.type == myInventory.myCurrentItem.type)
            {
                button.GetComponent<ButtonListButton>().SetEquipIcon(false);
            }
        }
        myInventory.myCurrentItemButton.GetComponent<ButtonListButton>().SetEquipIcon(true);

    }
}
