using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{

    public Item item;
    public UnityEngine.UI.Image icon = null;
    public UnityEngine.UI.Text text_Count;
    public UnityEngine.UI.Text text_Price = null;


    public void AddItem(Item newItem, int count = 1)
    {
        if (item == null)
        {
            item = newItem;
        }
        
        icon.sprite = item.icon;
        icon.enabled = true;

        if (item.price != 0 && text_Price != null)
            text_Price.text = item.price.ToString();

        if (text_Count != null)
            if (item.category == Category.Consume)
            {
                text_Count.gameObject.SetActive(true);
                text_Count.text = count.ToString();
            }
    }

    public void UpdateUI()
    {
        if (item == null)
            return;

        icon.sprite = item.icon;
        icon.enabled = true;
    }



    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }
}
