using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerStats))]
public class AttackedTakeDamage : MonoBehaviour, iAttackable
{
	public Slider slider;
	private static List<Transform> players;
    private PlayerStats stats;

    Animator anim;

	static bool playerLose = false;
	
	private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        anim = GetComponent<Animator>();
	}

	private void Start()
	{
		slider = GameObject.Find("Swap" + transform.name + "Btn").GetComponentInChildren<Slider>();
		players = new List<Transform>();
		for(int i =0;i<Constants.PlayerNum;i++){
			players.Add(GameObject.Find("Players").transform.GetChild(i));
		}
	}

	void Update(){
		slider.value = stats.GetHealth() / stats.GetMaxHealth();
	}

	public void OnAttack(GameObject attacker, Attack attack)
    {
        stats.TakeDamage(attack.Damage);


        anim.SetTrigger("Damage");

		//print(stats.GetHealth());
		CheckDied();
    }

	void CheckDied(){
		if (stats.GetHealth() <= 0)
		{
			anim.SetTrigger("Die");
			Debug.Log("1. Player Count " + players.Count);
			for (int i = 0; i < players.Count; i++)
			{
				if(players[i].tag == gameObject.tag){
					players.Remove(transform);
					if (players.Count == 0) 
					{
						DisplayFail();
						return;
					}
					players[0].tag = "MainPlayer";
					transform.tag = "DeadPlayer";
				}
			}
			GetComponent<PlayerAIControl>().enabled = false;
			GetComponent<NavMeshAgent>().speed = 0f;

			Debug.Log("2. Player Count " + players.Count);

			playerLose = true;
		}
	}

	public static bool IsLose()
	{
		return playerLose;
	}

	void WaitForDie(){
		
		gameObject.SetActive(false);
	}

	void DisplayFail(){
		Time.timeScale = 0f;
		EndUI.instance.LoseUIOn();
	}
}
