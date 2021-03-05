using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemInfo {
	public enum Category
	{
		Weapon,
		Shield,
		Potion
	}

	public string id;
	public string name;
	public string description;
	public Category category;
    public Sprite sprite;
    public int price;
}

public abstract class ItemData
{
	public ItemInfo info;
}

public abstract class EquipmentItemData : ItemData
{
	public int seq;
}