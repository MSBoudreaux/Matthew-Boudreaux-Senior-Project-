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

    //Base stats: if no items equipped, reset to these.

    public float baseAttackSpeed = 0.5f;
    public int baseDamage = 3;
    public float baseParryLength = 0.5f;
    public int baseBlockRating = 15;

    
    //Character Stats
    public float attackSpeed;
    public float parryLength;
    public int Damage;
    public int Defense;
    public int BlockRating;
    
    
    


    //Equipped Item Stats
    public ItemDatabaseObject itemDB;

    public Item equippedWeapon;
    public Item equippedShield;
    public Item equippedArmor;
    public Item equippedHeadwear;
    public Item equippedConsumable;

    public WeaponObject weaponObject;
    public ShieldObject shieldObject;
    public ArmorObject armorObject;
    public HeadwearObject headwearObject;
    public ConsumableObject consumableObject;

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
                    equippedWeapon.isEquipped = false;

                    _item.isEquipped = true;

                    equippedWeapon = _item;
                   
                    weaponObject = (WeaponObject)itemDB.Items[_item.Id];

                    Damage = weaponObject.damage;
                    attackSpeed = weaponObject.attackSpeed;

                    if (weaponObject.isTwoHanded)
                    {
                        Unequip(equippedShield);
                    }
                }
                break;
            case ItemType.Shield:
                if (equippedShield != _item)
                {
                    equippedShield.isEquipped = false;

                    _item.isEquipped = true;

                    equippedShield = _item;
                    shieldObject = (ShieldObject)itemDB.Items[_item.Id];

                    BlockRating = shieldObject.blockRating;
                    parryLength = shieldObject.parryTime;

                    if (weaponObject != null && weaponObject.isTwoHanded)
                    {
                        Unequip(equippedWeapon);
                    }
                }
                break;
            case ItemType.Armor:
                if (equippedArmor != _item)
                {
                    equippedArmor.isEquipped = false;

                    _item.isEquipped = true;

                    equippedArmor = _item;
                    armorObject = (ArmorObject)itemDB.Items[_item.Id];

                    Defense = armorObject.defenseRating;
                }
                break;
            case ItemType.Headwear:
                if (equippedHeadwear != _item)
                {
                    equippedHeadwear.isEquipped = false;

                    _item.isEquipped = true;

                    equippedHeadwear = _item;
                    headwearObject = (HeadwearObject)itemDB.Items[_item.Id];
                }
                break;
            case ItemType.Consumable:
                if(equippedConsumable != _item)
                {
                    equippedConsumable.isEquipped = false;

                    _item.isEquipped = true;

                    equippedConsumable = _item;
                    consumableObject = (ConsumableObject)itemDB.Items[_item.Id];
                }
                break;

        }


    }

    public void Unequip(Item inItem)
    {
        switch (inItem.type)
        {
            case ItemType.Weapon:
                equippedWeapon.isEquipped = false;
                equippedWeapon = null;
                weaponObject = null;
                break;
            case ItemType.Shield:
                equippedShield.isEquipped = false;
                equippedShield = null;
                shieldObject = null;
                break;
            case ItemType.Armor:
                equippedArmor.isEquipped = false;
                equippedArmor = null;
                armorObject = null;
                Defense = 0;
                break;
            case ItemType.Headwear:
                equippedHeadwear.isEquipped = false;
                equippedHeadwear = null;
                headwearObject = null;
                break;
            case ItemType.Consumable:
                break;

        }

        if (equippedWeapon == null)
        {
            attackSpeed = baseAttackSpeed;
            Damage = baseDamage;

        }

        if (equippedShield == null)
        {
            BlockRating = baseBlockRating;
            parryLength = baseParryLength;
        }
    }

    public void UseItem(Item _item) 
    {
        if(_item.type == ItemType.Consumable && _item.amount != 0)
        {
            if (consumableObject.healType)
            {
                AddHealth(consumableObject.healValue);
            }
            else
            {
                AddStress(-consumableObject.healValue);
            }

            _item.amount--;

        }

    }
}
