using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class EquipmentWeapon : Equipment
{
	public ActiveSkill[] skill = new ActiveSkill[2];
	
	public override void Use(int playerIndex)
	{
        base.Use(playerIndex);
	}
}
