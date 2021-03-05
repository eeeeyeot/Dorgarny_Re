using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionUI : MonoBehaviour
{
    public Transform optionUI;
	private MissionSO missionSO;
	private StageList_SO stageList_SO;
	private QuestInfo questInfoSO;
	public PlayerStats_SO[] playerStats_SO;
	public Slider volumeSlider;

	private void Start()
	{
		missionSO = Resources.Load("ScriptableObject/Mission/MissionSO") as MissionSO;
		stageList_SO = Resources.Load("ScriptableObject/Stage/StageList") as StageList_SO;
		questInfoSO = Resources.Load("ScriptableObject/Stage/QuestList") as QuestInfo;
	}
	public void ClickOption()
    {
        optionUI.gameObject.SetActive(!optionUI.gameObject.activeSelf);
    }

    public void ClickGameExitBtn()
    {
        Application.Quit();
    }

	public void ClickInitBtn(){
		foreach (var stage in stageList_SO.stageStarList)
		{
			stage.count = 0;
		}

		foreach (var quest in questInfoSO.questList)
		{
			for (int i = 0; i < quest.quest.Length; i++)
				quest.quest[i].complete = false;
		}

		foreach (var mission in missionSO.missionList)
		{
			mission.clearMission = false;
			mission.getReward = false;
			mission.currentValue = 0;
		}

		foreach (var playerstat in playerStats_SO)
		{
			playerstat.level = 1;
			if(playerstat.job != "Wizard" && playerstat.job != "Archer")
				playerstat.weapon = null;
		}

		SceneManager.LoadScene("IntroScene");
	}

	public void VolumeController(){
		AudioListener.volume = volumeSlider.value;
	}
}
