using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using Util;

public class ItemManager : Singleton<ItemManager>{
	private Dictionary<string, ItemInfo> dictItemInfo;
	public ItemManager() { }

	public void Init()
	{
		dictItemInfo = new Dictionary<string, ItemInfo>();
		TextAsset resource = Resources.Load("Json/ItemInfo") as TextAsset;
		JSONNode root = JSON.Parse(resource.text);

        //InitWeaponItemInfo(root);
        //InitSheildItemInfo(root);
        InitPotionItemInfo(root);

        Debug.Log("init complete ItemManager");
	}

	private void InitWeaponItemInfo(JSONNode root)
	{
		JSONNode itemInfos = root["weapon"];
		for(int i = 0; i < itemInfos.Count; i++)
		{
			JSONNode jsonInfo = itemInfos[i];
			WeaponItemInfo itemInfo = new WeaponItemInfo();
			itemInfo.id = jsonInfo["id"];
			itemInfo.name = jsonInfo["name"];
			itemInfo.description = jsonInfo["description"];
			dictItemInfo.Add(itemInfo.id, itemInfo);
		}
	}

	private void InitSheildItemInfo(JSONNode root)
	{
		JSONNode itemInfos = root["Sheild"];
		for(int i = 0; i < itemInfos.Count; i++)
		{
			JSONNode jsonInfo = itemInfos[i];
			SheildItemInfo itemInfo = new SheildItemInfo();
			itemInfo.id = jsonInfo["id"];
			itemInfo.name = jsonInfo["name"];
			itemInfo.description = jsonInfo["description"];
			dictItemInfo.Add(itemInfo.id, itemInfo);
		}
	}

    private void InitPotionItemInfo(JSONNode root)
	{
        JSONNode itemInfos = root["potion"];
        for (int i = 0; i < itemInfos.Count; i++)
        {
            JSONNode jsonInfo = itemInfos[i];
            PotionItemInfo itemInfo = new PotionItemInfo();
            itemInfo.id = jsonInfo["id"];
            itemInfo.name = jsonInfo["name"];
            itemInfo.sprite = Resources.Load<Sprite>("ItemSprite/Item_" + itemInfo.name);
            itemInfo.price = jsonInfo["price"];
            itemInfo.description = jsonInfo["description"];
            
            dictItemInfo.Add(itemInfo.id, itemInfo);
        }
    }

    public ItemInfo Find(string id)
    {
        return dictItemInfo[id];
    }
}
