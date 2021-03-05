

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory");
        }
        
        instance = this;
        
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback = null;

    public int space = 80;                      //가방 공간
    
    public Dictionary<Item, int> items_count = new Dictionary<Item, int>();
    public List<Item> items = new List<Item>();
    int count = 0;
    
    public bool Add(Item item)
    {
        if (item == null)
            return false;

        if (!item.isDefaultItem)
        {
            if (items.Count >= space)           //가방이 꽉찼는지 확인
            {
                return false;
            }
            if (item.category == Category.Consume)
            {
                if (items_count.TryGetValue(item, out count))
                {
                    count++;
                    items_count[item] = count;
                }
                else
                {
                    count++;
                    items.Add(item);
                    items_count.Add(item, count);
                }
            }
            else
            {
                count++;
                items.Add(item);
            }

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }

        return true;
    }

    // 아이템 강제 추가
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
            Add((Equipment)Resources.Load("ScriptableObject/Items/Warrior/WeaponSword"));
        if (Input.GetKeyDown(KeyCode.L))
            Add((Equipment)Resources.Load("ScriptableObject/Items/Wizard/WeaponStaff"));
		if (Input.GetKeyDown(KeyCode.J))
			Add((Equipment)Resources.Load("ScriptableObject/Items/Archer/WeaponBow"));
	}

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
