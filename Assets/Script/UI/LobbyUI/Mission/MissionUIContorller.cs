using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionUIContorller : MonoBehaviour
{
    private MissionSO missionSO;
    public GameObject missionUI;
    public Transform missionUIRoot;
    public List<MissionUI> missionUIs = new List<MissionUI>();

    private void Start()
    {
        missionSO = MissionManager.instance.missionSO;

        for (int i = 0; i < missionUIRoot.childCount; i++)
        {
            missionUIs.Add(missionUIRoot.GetChild(i).GetComponent<MissionUI>());
        }
        
        
    }
    public void UpdateUI()
    { 
		for (int i = 0; i < missionSO.missionList.Count; i++)
        {
			if (missionSO.missionList[i].clearMission && !missionSO.missionList[i].getReward)
				missionUIs[i].rewardbtn.interactable = true;
			else
				missionUIs[i].rewardbtn.interactable = false;


			missionUIs[i].UpdateUI(missionSO.missionList[i].missionid, missionSO.missionList[i].missiondescription, missionSO.missionList[i].currentValue, missionSO.missionList[i].needValue);
        }
    }

    public void ClickMission()
    {
        missionUI.SetActive(!missionUI.activeSelf);
        if (missionUI.activeSelf)
            UpdateUI();
    }
}
