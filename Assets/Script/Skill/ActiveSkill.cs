using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System.Text;

[CreateAssetMenu(fileName = "New ActiveSkill", menuName = "Skills/ActiveSkill")]
public class ActiveSkill : Skill
{
	public float damageModifier;
	public SkillIndex skillIndex;
	public Object skillEffect;
	public string description;

	private GameObject skillObj;
	private StringBuilder sb = new StringBuilder();

	public override void Use()
	{
		if (SpellIsReady)
		{
			base.Use();
			sb.Clear();
			GameObject mainP = GameObject.FindWithTag("MainPlayer");

			sb.Append(mainP.name).Append("Skill").Append((int)skillIndex);

			GameObject skillPos = GameObject.Find(sb.ToString());
			Debug.Log(skillPos.name);
			if (skillPos.transform.childCount > 0)
			{
				skillPos.transform.GetChild(0).gameObject.SetActive(true);
				skillPos.GetComponentInChildren<ParticleSystem>().Play();
			}
			else
			{
				skillObj = Instantiate(skillEffect, skillPos.transform) as GameObject;
			}

			PlayerSkillManager.instance.Start_Coroutine(SkillCooldown());
			PlayerSkillManager.instance.Start_Coroutine(name, skillObj);

			SpellIsReady = false;
		}
	}
	IEnumerator SkillCooldown()
	{
		while (currentCoolTime > 0)
		{
			currentCoolTime -= Time.deltaTime;

			yield return null;
		}
		currentCoolTime = CoolDownTime;
		SpellIsReady = true;
	}

}

public enum SkillIndex
{
	First = 1,
	Second
}