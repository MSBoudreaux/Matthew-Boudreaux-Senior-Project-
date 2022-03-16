using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public PlayerStats myStats;
    public int damage;
    
    // Update is called once per frame
    void Update()
    {
        damage = myStats.Damage;
    }
}
