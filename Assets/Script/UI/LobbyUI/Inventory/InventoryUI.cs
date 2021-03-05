using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    Inventory inventory;

    Slot[] slots;
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<Slot>();
        UpdateUI();
    }

    void UpdateUI()
    {
        int count = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                inventory.items_count.TryGetValue(inventory.items[i], out count);

                slots[i].AddItem(inventory.items[i], count);

            }
            else
            {
                slots[i].ClearSlot();
                slots[i].text_Count.text = null;
            }
        }
    }

    public void ClickInventory()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }
}
