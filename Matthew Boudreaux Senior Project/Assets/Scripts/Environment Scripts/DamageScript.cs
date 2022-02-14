using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public bool IsRepeatable;
    public bool IsTriggered;

    public int healthDamage;
    public int stressDamage;

    public PlayerStats player;

    public void Start()
    {
        player = FindObjectOfType<PlayerStats>();
    }
    public void DealDamage()
    {
        if(IsRepeatable)
        {
            player.AddHealth(-healthDamage);
            player.AddStress(stressDamage);
        }
        else if (!IsTriggered)
        {
            player.AddHealth(-healthDamage);
            player.AddStress(stressDamage);
            IsTriggered = true;
        }

    }



}
