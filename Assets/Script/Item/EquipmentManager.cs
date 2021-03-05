using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class EquipmentManager : MonoBehaviour
{
	#region Singleton
	public static EquipmentManager instance;

	void Awake()
	{
		instance = this;
	}

	#endregion

	[SerializeField]
    Inventory inventory;
	private void Start()
	{
        inventory = Inventory.instance;
    }



	//수정해야함 상시가 아닌 인벤토리에서 장착시로
	//캐릭터 스킬을 위한 기능
	public void Equip(Equipment newItem, int playerIndex = 0)
    {
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = null;

        switch (slotIndex) {
            case 0:
                oldItem = CharacterManager.instance.stats_List[playerIndex].weapon;
                inventory.Add(oldItem);
				CharacterManager.instance.stats_List[playerIndex].weapon = (EquipmentWeapon)newItem;
                break;
            case 1:
                oldItem = CharacterManager.instance.stats_List[playerIndex].armor;
                inventory.Add(oldItem);
				CharacterManager.instance.stats_List[playerIndex].armor = (EquipmentArmor)newItem;
                break;
        }
		CharacterManager.instance.UpdateUI();
	}
	
    public void Unequip (int playerIndex, int slotIndex)
    {
        switch (slotIndex)
        {
            case 0:
                if (CharacterManager.instance.stats_List[playerIndex].weapon != null)
                {
                    Equipment oldItem = CharacterManager.instance.stats_List[playerIndex].weapon;
                    inventory.Add(oldItem);

					CharacterManager.instance.stats_List[playerIndex].weapon = null;
                    
                }
                    break;
            case 1:
                if (CharacterManager.instance.stats_List[playerIndex].armor != null)
                {
                    Equipment oldItem = CharacterManager.instance.stats_List[playerIndex].armor;
                    inventory.Add(oldItem);

					CharacterManager.instance.stats_List[playerIndex].weapon = null;

                }
                break;
        }
       
        
    }

    public void UnequipAll(int playerIndex)
    {
        for(int i = 0; i < Constants.EquipmentItemSlotIndex; i++)
        {
            Unequip(playerIndex, i);
        }
    }
}