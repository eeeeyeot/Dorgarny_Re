using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Scripts;

[CreateAssetMenu(fileName = "NewStats", menuName = "Player/Stats", order = 1)]
public class PlayerStats_SO : ScriptableObject
{

	#region Field

	public Sprite icon;

	public RPS rps = RPS.None;

    public bool setMenually = false;
    public bool saveDataOnClose = false;
	
    public float maxHealth = 0;
    public float currentHealth = 0;

    public float maxMana = 0;
    public float currentMana = 0;

    public float baseDamage = 0;

	public float baseArmor = 0;
	public float CurrentArmor
	{
		get
		{
			return (armor != null) ? baseArmor + armor.armorModifier : baseArmor;
		}
	}

	public float CurrentDamage
	{
		get
		{
			return (weapon != null) ? baseDamage + weapon.damageModifier : baseDamage;
		}
		set
		{
			baseDamage = value;
		}
	}

	public string job;
	public int level = 1;
	public int currentExp = 0;
	public int maxExp = 100;

    public float viewRange = 0;
    public float attackRange = 0;

	public EquipmentWeapon weapon;
	public EquipmentArmor armor;

	public ActiveSkill[] activeSkill = new ActiveSkill[2];
	#endregion

	#region Initialize
	public void InitSkills()
	{
		foreach(var skill in activeSkill)
		{
			if (skill == null)
				return;
			skill.currentCoolTime = skill.CoolDownTime;
			skill.SpellIsReady = true;
		}
	}
	#endregion

	#region Stat Increasers
	public void IncreaseHealth(float healthAmount)
    {
        if ((currentHealth + healthAmount) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += healthAmount;
        }
    }

    public void IncreaseMana(float manaAmount)
    {
        if ((currentMana + manaAmount) > maxMana)
        {
            currentMana = maxMana;
        }
        else
        {
            currentMana += manaAmount;
        }
    }

	public void IncreaseExp(int expAmount)
	{
		currentExp += expAmount;
		while (currentExp >= maxExp)
		{
			level++;
			currentExp -= maxExp;
			SetStats();
		}
	}

	public void SetStats()
	{
		baseDamage = level * 10f;
		baseArmor = level * 1.05f;
		maxHealth = (level * 20f) + 100f;
		maxExp = (level * 20) + 100;
	}
    #endregion

    #region Stat Decreasers
    public float TakeDamage(float amount)
    {
		currentHealth -= (amount - CurrentArmor >= 1) ? amount - CurrentArmor : 1f;

        if (currentHealth <= 0)
        {
            Death();
        }

		return (amount - CurrentArmor >= 0) ? amount - CurrentArmor : 1f;
    }

    public void TakeMana(float amount)
    {
        currentMana -= amount;

        if (currentMana <= 0)
        {
            currentMana = 0;
        }
    }
    #endregion

    #region Death
    private void Death()
    {
        Debug.Log("<ENEMY DIE>");

    }
    #endregion

}
