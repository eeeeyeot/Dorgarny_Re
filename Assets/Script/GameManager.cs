using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts;

public class GameManager : FSMBase<GameState>
{

	#region Singleton
	private static GameManager instance;

	public static GameManager Instance
	{
		get
		{
			if (instance == null)
			{
				GameObject obj;
				obj = GameObject.Find(typeof(GameManager).Name);
				if (obj == null)
				{
					obj = new GameObject(typeof(GameManager).Name);
					instance = obj.AddComponent<GameManager>();
				}
				else
				{
					instance = obj.GetComponent<GameManager>();
				}
			}
			return instance;
		}
	}
	#endregion

	public MissionSO missionSO;
	public PlayerStats[] playerStats;

	public Text timeText;
	public int potionUseCount = 0;
	public class TimerMS
	{
		public float m = 0;
		public float s = 0;
		public string GetTimeString()
		{
			if (s >= 60)
			{
				m++;
				s = 0;
			}
			if (s < 9.5f)
			{
				return m.ToString() + " : 0" + s.ToString("N0");
			}
			return m.ToString() + " : " + s.ToString("N0");
		}
		public float GetTimeSecond()
		{
			return (m * 60) + s;
		}
	}

	public TimerMS time = new TimerMS();

	private void Awake()
	{
		playerStats = GameObject.Find("Players").GetComponentsInChildren<PlayerStats>();
		missionSO = Resources.Load("ScriptableObject/Mission/MissionSO") as MissionSO;
	}

	private void Start()
	{
		_name = "Game Manager";
	}

	void Update()
	{
		time.s += Time.deltaTime;

		timeText.text = time.GetTimeString();
	}

	private void FixedUpdate()
	{
		if(EnemyController.IsWin())
		{
			SetState(GameState.Win);
		}
		else if(AttackedTakeDamage.IsLose())
		{
			SetState(GameState.Lose);
		}
	}
	
	protected override IEnumerator Idle()
	{
		do
		{
			
			yield return null;
		} while (!isNewState);
	}

	protected virtual IEnumerator Win()
	{
		do
		{
			Debug.Log("Call WinUI");
			EndUI.instance.WinUIOn();
			yield return null;
		} while (!isNewState);
	}

	protected virtual IEnumerator Lose()
	{
		do
		{
			EndUI.instance.IsPlayerDied = true;
			yield return null;
		} while (!isNewState);
	}
}
