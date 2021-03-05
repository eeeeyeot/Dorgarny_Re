using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;

public class PlayerSkillManager : MonoBehaviour
{
	#region Singleton

	public static PlayerSkillManager instance;

	void Awake()
	{
		instance = this;
	}

	#endregion

	GameObject[] players;
	GameObject mainPlayer;

	public Button[] BtnSkill = new Button[2];
	public Sprite transparent_Spr;

	private Text[] cooldownText = new Text[2];
	private float timer = 0.5f;

	delegate void Test(ActiveSkill skill);

	private void Start()
	{
		players = new GameObject[Constants.PlayerNum];
		for (int i = 0; i < Constants.PlayerNum; i++)
		{
			players[i] = GameObject.Find("Players").transform.GetChild(i).gameObject;
			Debug.Log(players[i].name);
		}

		for(int i = 0; i < BtnSkill.Length; i++)
		{
			cooldownText[i] = BtnSkill[i].transform.Find("TextCooldown").GetComponent<Text>();
		}
	}

	private void Update()
	{
		//if (mainPlayer == null || !mainPlayer.tag.Equals("MainPlayer"))
		//{
			mainPlayer = GameObject.FindWithTag("MainPlayer");
			SetSkills(mainPlayer.GetComponent<PlayerStats>().PlayerDefinition.activeSkill);
		//}
	}

	public void ClearSkills()
	{
		for (int i = 0; i < BtnSkill.Length; i++)
		{
			BtnSkill[i].image.sprite = transparent_Spr;
			BtnSkill[i].onClick.RemoveAllListeners();
		}
	}

	void SetSkills(ActiveSkill[] newSkill)
	{
		if (newSkill[0] == null)
		{
			ClearSkills();
			return;
		}

		for (int i = 0; i < cooldownText.Length; i++)
		{
			if (!newSkill[i].SpellIsReady)
			{
				cooldownText[i].text = newSkill[i].currentCoolTime.ToString();
			}
			else
			{
				cooldownText[i].text = "";
			}
		}


		for (int i = 0; i < BtnSkill.Length; i++)
		{
			BtnSkill[i].onClick.RemoveAllListeners();
			BtnSkill[i].image.sprite = newSkill[i].icon;
			ActiveSkill skill = newSkill[i];
			BtnSkill[i].onClick.AddListener(
				() =>
				{
					ActivateSkill(skill);
				});
		}
	}

	public void Start_Coroutine(string name, GameObject skillObj)
	{
		StartCoroutine(name, skillObj);
	}

	public void Start_Coroutine(IEnumerator coroutine)
	{
		StartCoroutine("LoopCoroutine", coroutine);
	}

	IEnumerator LoopCoroutine(IEnumerator coroutine)
	{
		while (true)
		{
			yield return StartCoroutine(coroutine);
		}
	}

	void ActivateSkill(ActiveSkill newSkill)
	{
		if (newSkill.SpellIsReady)
		{
			FSMPlayer player = GameObject.FindWithTag("MainPlayer").GetComponent<FSMPlayer>();

			player.SetState(CharacterState.Skill);

			player.Set(newSkill);
		}
	}

	//관통
	IEnumerator PowerShot(GameObject skillObj)
	{
		var hits = Physics.SphereCastAll(mainPlayer.transform.position, 0.5f, mainPlayer.transform.forward, 2f);

		for(int i = 0; i < hits.Length; i++)
		{
			if (hits[i].transform.tag == "Enemy")
			{
				hits[i].transform.GetComponent<EnemyStats>().TakeDamage(mainPlayer.GetComponent<PlayerStats>().GetDamage() * 1.3f);
			}
		}
		yield return null;
	}

	//단일
	IEnumerator ChargeShot(GameObject skillObj)
	{
		RaycastHit hit;
		var isHit = Physics.BoxCast(mainPlayer.transform.position, mainPlayer.transform.lossyScale / 2, mainPlayer.transform.forward, out hit, mainPlayer.transform.rotation);

		if(isHit)
		{
			if(hit.transform.tag == "Enemy")
			{
				hit.transform.GetComponent<EnemyStats>().TakeDamage(mainPlayer.GetComponent<PlayerStats>().GetDamage() * 2f);
			}
		}
		yield return null;
	}

	IEnumerator PowerHit(GameObject skillObj)
	{
		var hits = Physics.BoxCastAll(transform.position, transform.lossyScale / 2, transform.forward, transform.rotation, 0.2f);

		foreach (var hit in hits)
		{
			if (hit.transform.tag == "Enemy")
			{
				hit.transform.GetComponent<EnemyStats>().TakeDamage(mainPlayer.GetComponent<PlayerStats>().GetDamage() * 1.5f);
			}
		}

		yield return null;
	}

	IEnumerator SpinHit(GameObject skillObj)
	{
		Collider[] hitColliders = Physics.OverlapSphere(skillObj.transform.position, 1f);
		for (int i = 0; i < hitColliders.Length; i++)
		{
			if (hitColliders[i].transform.tag == "Enemy")
				hitColliders[i].transform.GetComponent<EnemyStats>().TakeDamage(mainPlayer.GetComponent<PlayerStats>().GetDamage() * 1.2f);
		}
		yield return null;
	}

	IEnumerator SingleHeal(GameObject skillObj)
	{
		GameObject healTarget = players[0];
		for(int i = 0; i < players.Length; i++)
		{
			if(players[i].GetComponent<PlayerStats>().GetHealth() < healTarget.GetComponent<PlayerStats>().GetHealth())
				healTarget = players[i];
		}

		healTarget.GetComponent<PlayerStats>().IncreaseHealth(20f);

		//파티클이 Wizard의 자식이나 위치는 HealTarget을 따라가기 위함
		while (true)
		{
			skillObj.transform.position = healTarget.transform.position;
			yield return null;
			if (skillObj.activeSelf) break;
		}
	}

	//광역
	IEnumerator AOEHeal(GameObject skillObj)
	{
		float t = timer;
		while (skillObj.activeSelf)
		{
			if (t <= 0f)
			{
				Collider[] hitColliders = Physics.OverlapSphere(skillObj.transform.position, 1.5f);
				for (int i = 0; i < hitColliders.Length; i++)
				{
					if (hitColliders[i].tag == "SubPlayer" || hitColliders[i].tag == "MainPlayer")
						hitColliders[i].transform.GetComponent<PlayerStats>().IncreaseHealth(10f);
				}
				t = timer;
			}
			else
			{
				t -= Time.deltaTime;
			}

			yield return null;
		}
	}
}
