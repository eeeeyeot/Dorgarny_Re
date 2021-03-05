using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndUI : Subject
{
    public Sprite fillstar;
    public QuestInfo QuestInfoSO;
    public StageList_SO StageList_SO;
    public Transform stars;
    public List<Text> missiontxtList;
    public List<Text> rewardtxtList;
	public Text[] previousLevelText = new Text[3];
	public Text[] presentlyLevelText = new Text[3];

	int starCount;
	int[] previousLvl = new int[3];
	int[] presentlyLvl = new int[3];

	List<Image> starList = new List<Image>();
    Transform WinUI;
    Transform LoseUI;
	Transform LevelUpUI;

	bool doOnce = true;

	public bool IsPlayerDied { get; set; }


	#region Singleton
	public static EndUI instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("More than one instance of EndUI");
		}
		instance = this;
	}
	#endregion

	void Start()
    {
		AddObserver(GameManager.Instance.missionSO.missionList[2]);
        starCount = 0;
        for (int i = 0; i < stars.childCount; i++)
        {
            starList.Add(stars.GetChild(i).GetComponent<Image>());
        }
        WinUI = this.transform.Find("WinUI").GetComponent<Transform>();
        LoseUI = this.transform.Find("LoseUI").GetComponent<Transform>();
		LevelUpUI = this.transform.Find("LevelUpUI").GetComponent<Transform>();

	}

    public void WinUIOn()
    {
		if (doOnce)
		{
			StartCoroutine(WaitForClear());
		}
	}

	public void LoseUIOn()
    {
        LoseUI.gameObject.SetActive(true);
    }

    public void CheckQuest()
    {
        for (int i = 0; i < QuestInfoSO.questList.Count; i++)
        {
            if (QuestInfoSO.questList[i].stagename == SceneManager.GetActiveScene().name)
            {
                // 아무 캐릭터가 안죽었을때
                if (!IsPlayerDied)
                {
                    QuestInfoSO.questList[i].quest[0].complete = true;
                    starCount++;
                }
                // 포션을 사용안한 경우
                if (GameManager.Instance.potionUseCount == 0)
                {
                    QuestInfoSO.questList[i].quest[1].complete = true;
                    starCount++;
                }
                // 시간내에 클리어
                if (GameManager.Instance.time.GetTimeSecond() <= 150)
                {
                    QuestInfoSO.questList[i].quest[2].complete = true;
                    starCount++;
                }
            }
        }
    }

    void DrawWinUI()
    {
        int stageStar = 0;
		
        for (int i = 0; i < starCount; i++)
            starList[i].sprite = fillstar;

        for (int i = 0; i < QuestInfoSO.questList.Count; i++)
        {
            if (QuestInfoSO.questList[i].stagename == SceneManager.GetActiveScene().name)
            {
                for (int j = 0; j < QuestInfoSO.questList[i].quest.Length; j++)
                {
                    missiontxtList[j].text = QuestInfoSO.questList[i].quest[j].quest;
                    if (QuestInfoSO.questList[i].quest[j].complete == true)
                        stageStar++;
                }
            }
        }
        for (int i = 0; i < StageList_SO.stageStarList.Count; i++)
        {
            if (StageList_SO.stageStarList[i].stagename == SceneManager.GetActiveScene().name)
            {
                rewardtxtList[0].text = StageList_SO.stageStarList[i].rewardGold.ToString();
                rewardtxtList[1].text = StageList_SO.stageStarList[i].rewardExp.ToString();
                if (stageStar > StageList_SO.stageStarList[i].count)
                    StageList_SO.stageStarList[i].count = stageStar;
            }
        }
    }

	IEnumerator WaitForClear() {
		yield return new WaitForSeconds(1.0f);

		if (GameManager.Instance.missionSO.missionList[2].IsTarget(SceneManager.GetActiveScene().name))
			Notify("StageClear", 1);

		Debug.Log("WinUI 호출 성공");

		Time.timeScale = 0f;
		Debug.Log("WinUI");
		CheckQuest();
		DrawWinUI();
		WinUI.gameObject.SetActive(true);


		int i = 0;
		foreach (var p in GameManager.Instance.playerStats)
		{
			previousLvl[i] = p.GetLevel();
			previousLevelText[i].text = previousLvl[i].ToString();
			int stageexp = StageList_SO.GetStageInfo().rewardExp;
			p.IncreaseExp(stageexp);
			presentlyLvl[i] = p.GetLevel();
			presentlyLevelText[i].text = presentlyLvl[i++].ToString();
		}

		if (!presentlyLvl.SequenceEqual(previousLvl))
		{
			LevelUpUI.gameObject.SetActive(true);
		}

		doOnce = false;
	}

    private void Update()
    {
      
    }
}
