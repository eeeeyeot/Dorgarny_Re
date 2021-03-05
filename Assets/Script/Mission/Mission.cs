using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission : Observer
{
    public bool clearMission = false;
	public bool getReward = false;
    public string missionid;
    public string missiondescription;
    public int needValue;
    public int currentValue;
    public int rewardGold;
    public Item rewardItem;
    

    public override void OnNotify(string id, int value)
    {
        if (missionid == id && !clearMission) {
            currentValue += value;
            if (needValue <= currentValue)
            {
                clearMission = true;
            }
        }

        
    }
	public bool IsTarget(string target){
		return missionid == target ? true : false;
	}
    public void Reward()
    {
		getReward = true;
        GoldManager.instance.Gold += rewardGold;
        if (rewardItem != null)
            Inventory.instance.Add(rewardItem);
    }
}
