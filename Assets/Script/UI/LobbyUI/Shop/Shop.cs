using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shop : MonoBehaviour
{

    public Transform slotRoot;
    private List<Slot> slots;
    [SerializeField]
    public List<Item> shopItems = new List<Item>();

    bool active = false;

    void Start()
    {

        slots = new List<Slot>();
        int slotCnt = slotRoot.childCount;

        for (int i = 0; i < slotCnt; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();

            if (i < shopItems.Count)
            {

                slot.AddItem(shopItems[i]);
            }
            else
            {
                slot.GetComponent<UnityEngine.UI.Button>().interactable = false;
            }

            slots.Add(slot);
        }
    }
    public void Clickshop()
    {
        active = !active;
        this.gameObject.SetActive(active);
    }

    public void OnClickSlot(Slot slot)
    {
        if (Inventory.instance.Add(slot.item))
        {
            if (GoldManager.instance.Gold >= slot.item.price)
            {
                GoldManager.instance.CalcGold(-(slot.item.price));
            }
        }
    }
}
