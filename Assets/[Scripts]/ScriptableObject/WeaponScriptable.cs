using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Item/Weapon", order = 2)]
public class WeaponScriptable : EquippitbleScriptable
{
    public WeaponStats weaponStats;

    public override void UseItem(PlayerController playerController)
    {
        if (equipped)
        {
            //unequip from inventory here
            //remove from controller here too
        }
        else
        {
            //invoke OnWeaponEquipped from player here from inventory
            //equip weapon from weapon holder on playercontroller
        }

        base.UseItem(playerController);
    }
    
    
}
