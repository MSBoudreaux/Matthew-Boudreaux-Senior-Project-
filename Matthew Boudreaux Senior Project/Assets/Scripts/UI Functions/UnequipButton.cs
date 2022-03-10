using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnequipButton : MonoBehaviour
{

    public PlayerStats myStats;
    public InventoryDisplay myInventory;

    public void OnClick()
    {
        myStats.Unequip(myInventory.myCurrentItem);

    }





}
