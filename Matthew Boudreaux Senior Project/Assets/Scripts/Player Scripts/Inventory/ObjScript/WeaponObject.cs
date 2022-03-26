using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Object", menuName = "InventorySystem/Items/weapon")]

public class WeaponObject : ItemObject
{
    public int damage;
    public float attackSpeed;
    public bool isTwoHanded;
    public AnimationData attackAnim;
    public AnimatorOverrideController myAnimController;
    

    private void Awake()
    {
        type = ItemType.Weapon;
        attackSpeed = attackAnim.myAnim.length;
    }
}
