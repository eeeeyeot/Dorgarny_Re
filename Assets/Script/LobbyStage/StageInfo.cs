using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageInfo : MonoBehaviour
{
	public string stageText;
	public bool unLocked = false;
	public int starCount = 0;
	public Transform starroot;
	public SpriteRenderer[] stars;

	void Awake()
	{
		stars = new SpriteRenderer[3];
		starroot = this.transform.GetChild(0).GetComponent<Transform>();
		stageText = this.gameObject.name;

		for (int i = 0; i < starroot.childCount; i++)
		{
			stars[i] = starroot.GetChild(i).GetComponentInChildren<SpriteRenderer>();
		}
	}

	public void UpdateStar(Sprite star_sprite)
	{
		for (int i = 0; i < starCount; i++)
		{
			stars[i].sprite = star_sprite;
		}
	}
}
