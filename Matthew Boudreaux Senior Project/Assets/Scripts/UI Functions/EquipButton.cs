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
    }
}
