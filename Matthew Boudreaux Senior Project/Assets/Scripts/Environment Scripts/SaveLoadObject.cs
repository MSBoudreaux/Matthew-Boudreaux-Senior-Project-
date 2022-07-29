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
            FindObjectOfType<SaveLoadSystem>().Save();
        }
        else
        {
            FindObjectOfType<SaveLoadSystem>().Load();
        }
    }


}
