using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipInventoryUI : MonoBehaviour
{
    private Equipment item;
    public Transform rootSlot; // slotroot
    public EquipmentManager equipment = EquipmentManager.instance;
    public GameObject equipInventoryUI;
    public GameObject itemInfoUI;
    private string type = "";
    Inventory inventory;

    Slot[] slots;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        inventory = Inventory.instance;
        slots = rootSlot.GetComponentsInChildren<Slot>();
    }

    public void UpdateUI()
    {
        int count = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ClearSlot();
            if (i < inventory.items.Count)
            {
                if (inventory.items[i] != null && inventory.items[i].category == Category.Equipment)
                {
                    Equipment equipment = inventory.items[i] as Equipment;
                    
                    switch (type)
                    {
                        case "Weapon":
                            if (equipment.equipSlot == EquipmentSlot.Weapon)
                            {
                                slots[count++].AddItem(inventory.items[i]);
                            }
                            break;
                        case "Armor":
                            if (equipment.equipSlot == EquipmentSlot.Armor)
                            {
                                slots[count++].AddItem(inventory.items[i]);
                            }
                            break;
                    }
                }
            }
            slots[i].UpdateUI();
        }

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].UpdateUI();
        }
    }

    public void EquipItem()
    {
        item.Use(CharacterManager.instance.playerIndex);
        UpdateUI();
        CharacterManager.instance.UpdateUI();
        itemInfoUI.SetActive(false);
    }

    public void ItemInfoUI(Slot slot)
    {
        if (slot.item == null)
            return;

        ItemInfoUIBtn();
        item = slot.item as Equipment;
        itemInfoUI.GetComponent<ItemInfoUI>().UpdateUI(item);
    }
    public void ItemInfoUIBtn()
    {
        itemInfoUI.SetActive(!itemInfoUI.activeSelf);
    }

    public void Clickequipinventory(string type)
    {
        this.type = type;
        equipInventoryUI.SetActive(!equipInventoryUI.activeSelf);
        if (equipInventoryUI.activeSelf)
            UpdateUI();
    }
}
