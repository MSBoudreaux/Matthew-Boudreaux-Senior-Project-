using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    public int health;
    public int maxHealth = 100;
    public int stress;
    public int stamina;
    public int maxStamina = 3;
    public float stamRegen;

    public WeaponObject equippedWeapon;
    public ShieldObject equippedShield;
    public ArmorObject equippedArmor;
    public HeadwearObject equippedHeadwear;

    //UI element references
    public Slider healthBar;
    public Slider stressBar;
    public List<Image> stamOrbs;
    public Slider stamRegenBar;

    //Define unique abilities to check your items for here
    public enum Ability
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (100 - stress < maxHealth && health > 100 - stress)
        {
            health = 100 - stress;
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }


        updateHealthBar();
        updateStressBar();

    }

    public void updateHealthBar()
    {
        healthBar.value = health;
    }
    
    public void updateStressBar()
    {
        stressBar.value = stress;
    }

    public void AddHealth(int _health)
    {
        health += _health;
    }
    
    public void AddStress(int _stress)
    {
        stress += _stress;
    }
}
