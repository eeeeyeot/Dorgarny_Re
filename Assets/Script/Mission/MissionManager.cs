using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    #region Singleton
    public static MissionManager instance = null;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion
    public MissionSO missionSO;
    

    private void Start()
    {
        missionSO = (MissionSO)Resources.Load("ScriptableObject/Mission/MissionSO");
    }

    public void Reward(string title)
    {
        foreach (Mission mission in missionSO.missionList) {
            if (mission.missionid == title)
					mission.Reward();
        }
    }
}

