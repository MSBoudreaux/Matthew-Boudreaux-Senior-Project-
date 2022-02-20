using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListButton : MonoBehaviour
{

    [SerializeField]
    public Item myItem;



    public void SetItem(Item _item)
    {
        myItem = _item;
    }
    public void OnClick()
    {

    }
}
