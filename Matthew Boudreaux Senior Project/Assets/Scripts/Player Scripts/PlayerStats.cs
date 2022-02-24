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

    public ItemDatabaseObject itemDB;

    public Item equippedWeapon;
    public Item equippedShield;
    public Item equippedArmor;
    public Item equippedHeadwear;

    public WeaponObject weaponObject;
    public ShieldObject shieldObject;
    public ArmorObject armorObject;
    public HeadwearObject headwearObject;

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

    public void Equip(Item _item)
    {
        switch (_item.type)
        {
            case ItemType.Weapon:
                if(equippedWeapon != _item)
                {
                    equippedWeapon = _item;
                    weaponObject = (WeaponObject)itemDB.Items[_item.Id];
                }
                break;
            case ItemType.Shield:
                if (equippedShield != _item)
                {
                    equippedShield = _item;
                    shieldObject = (ShieldObject)itemDB.Items[_item.Id];
                }
                break;
            case ItemType.Armor:
                if (equippedArmor != _item)
                {
                    equippedArmor = _item;
                    armorObject = (ArmorObject)itemDB.Items[_item.Id];
                }
                break;
            case ItemType.Headwear:
                if (equippedHeadwear != _item)
                {
                    equippedHeadwear = _item;
                    headwearObject = (HeadwearObject)itemDB.Items[_item.Id];
                }
                break;
            case ItemType.Consumable:
                break;



        }
    }
}
