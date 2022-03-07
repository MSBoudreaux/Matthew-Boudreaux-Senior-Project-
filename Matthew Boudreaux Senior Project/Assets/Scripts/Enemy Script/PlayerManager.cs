using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update


    #region Singleton
    public static PlayerManager instance;
    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<FPSController>().gameObject;
    }
    #endregion

    public GameObject player;

}
