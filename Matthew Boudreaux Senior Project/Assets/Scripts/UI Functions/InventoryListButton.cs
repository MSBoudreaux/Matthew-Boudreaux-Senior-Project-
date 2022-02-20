using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryListButton : MonoBehaviour
{
    [SerializeField]
    public ItemType myMenuType;
    public InventoryDisplay myInventoryDisplay;

    public void OnClick()
    {
        myInventoryDisplay.SetInventoryToDisplay(myMenuType);
    }


}
