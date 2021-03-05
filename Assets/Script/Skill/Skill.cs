using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/Skill")]
public class Skill : ScriptableObject
{
	new public string name = "New Skill";
	public Sprite icon = null;
	public float CoolDownTime;
	public float currentCoolTime;
	public bool SpellIsReady = true;

	public virtual void Use()
	{
		//Debug.Log("Using Skill : " + name);
	}
}
