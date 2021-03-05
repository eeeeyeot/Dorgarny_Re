using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class QuestManager : Singleton<QuestManager>
{
    public Quest quest;

    public GameObject questWindow;
    public Text titleText;
    public Text descriptionText;
    public Text experienceText;
    public Text goldText;

    public void ClickQuest()
    {
        questWindow.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        experienceText.text = quest.experienceReward.ToString();
        goldText.text = quest.glodReward.ToString();
    }
}
