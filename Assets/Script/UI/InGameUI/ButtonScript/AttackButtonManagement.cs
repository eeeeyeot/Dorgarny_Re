using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButtonManagement : MonoBehaviour
{
	Button thisBtn;
	GameObject mainPlayer;
	private void Start()
	{
		mainPlayer = GameObject.FindWithTag("MainPlayer");
		thisBtn = GetComponent<Button>();
		thisBtn.onClick.AddListener(
			() =>
			{
				mainPlayer.GetComponent<FSMPlayer>().OnAutoAttack();
			}
		);
	}

	private void Update()
	{
		if(mainPlayer == null || mainPlayer.tag !="MainPlayer")
		{
			mainPlayer = GameObject.FindWithTag("MainPlayer");
		}
	}
}
