using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadObject : MonoBehaviour
{
    public bool isSaveObj;

    public void SaveLoad()
    {
        if (isSaveObj)
        {
            FindObjectOfType<PlayerStats>().Save();
        }
        else
        {
            FindObjectOfType<PlayerStats>().Load();
        }
    }


}
