using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
	public EquipmentSlot equipSlot;

	public int damageModifier;
    public int armorModifier;

    public override void Use(int playerIndex = 0)
    {
        //Equip item
        base.Use();
        EquipmentManager.instance.Equip(this, playerIndex);
        RemoveFromInventory();
    }
}


public enum EquipmentSlot { Weapon, Armor }