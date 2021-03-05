using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "StageList", menuName = "StageList")]
public class StageList_SO : ScriptableObject
{
    [System.Serializable]
    public class StageCombine {
        public string stagename;
        public int count;
        public int rewardGold;
        public int rewardExp;
    }
    
    public List<StageCombine> stageStarList;
    
	public StageCombine GetStageInfo()
	{
		foreach(var stage in stageStarList)
		{
			if(stage.stagename == SceneManager.GetActiveScene().name)
			{
				return stage;
			}
		}

		return null;
	}
}
