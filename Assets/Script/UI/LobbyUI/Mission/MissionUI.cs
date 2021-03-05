using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour
{
    public Text title;
    public Text description;
    public Image rewardSprite;
    public Button rewardbtn;
    
    public void UpdateUI(string title, string description, int currentValue, int needValue)
    {
        string[] temp;
        this.title.text = title;

        temp = description.Split(' ');
        this.description.text = temp[0] + needValue + temp[1] + "( " + currentValue + " / " + needValue + " )";
    }

    public void Reward()
    {
        MissionManager.instance.Reward(title.text);
        rewardbtn.interactable = false;
    }
}
