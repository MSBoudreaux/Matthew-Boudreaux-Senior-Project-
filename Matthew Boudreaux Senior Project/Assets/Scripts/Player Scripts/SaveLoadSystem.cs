using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadSystem : MonoBehaviour
{
    public PlayerStats myStats;
    public InventoryObject myInventory;

    public void Save()
    {
        myInventory.Save();
        myStats.Save();
    }

    public void Load()
    {
        myInventory.Load();
        myStats.Load();
    }


}
