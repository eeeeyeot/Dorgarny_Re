using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class PlayerSwapManager : MonoBehaviour
{
	NavMeshAgent mainPlayer;
	public void ChangeToArcher()
	{
		mainPlayer = GameObject.Find("Archer").GetComponent<NavMeshAgent>();
		mainPlayer.gameObject.tag = "MainPlayer";

		mainPlayer.avoidancePriority = 10;
		//sub = 20
		GameObject w;
		if ((w = GameObject.Find("Warrior")) != null){
			w.tag = "SubPlayer";
		}
		if ((w = GameObject.Find("Wizard")) != null)
		{
			w.tag = "SubPlayer";
		}


	}
	public void ChangeToWarrior()
	{
		mainPlayer = GameObject.Find("Warrior").GetComponent<NavMeshAgent>();
		mainPlayer.gameObject.tag = "MainPlayer";
		
		mainPlayer.avoidancePriority = 10;

		GameObject w;
		if ((w = GameObject.Find("Archer")) != null)
		{
			w.tag = "SubPlayer";
		}
		if ((w = GameObject.Find("Wizard")) != null)
		{
			w.tag = "SubPlayer";
		}
	}
	public void ChangeToWizard()
	{
		mainPlayer = GameObject.Find("Wizard").GetComponent<NavMeshAgent>();
		mainPlayer.gameObject.tag = "MainPlayer";

		mainPlayer.avoidancePriority = 10;

		GameObject w;
		if ((w = GameObject.Find("Archer")) != null)
		{
			w.tag = "SubPlayer";
		}
		if ((w = GameObject.Find("Warrior")) != null)
		{
			w.tag = "SubPlayer";
		}
	}
}
