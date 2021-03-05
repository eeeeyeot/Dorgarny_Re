using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : Subject
{
    public PlayerStats_SO PlayerDefinition;
	[SerializeField]
	private Canvas noticeCanvas;
	
	public Text text;

    #region Initalizations
    private void Start()
    {
        if (!PlayerDefinition.setMenually)
        {
            PlayerDefinition.maxHealth = 100;
            PlayerDefinition.currentHealth = 50;

            PlayerDefinition.maxMana = 100;
            PlayerDefinition.currentMana = 50;

            PlayerDefinition.baseDamage = 20;
            PlayerDefinition.CurrentDamage = 20;
        }
		else
		{
			PlayerDefinition.currentHealth = PlayerDefinition.maxHealth;
			PlayerDefinition.currentMana = PlayerDefinition.maxMana;
		}

		if (PlayerDefinition.weapon != null)
			PlayerDefinition.activeSkill = PlayerDefinition.weapon.skill;
		else{
			PlayerDefinition.activeSkill[0] = null;
			PlayerDefinition.activeSkill[1] = null;
		}

		noticeCanvas = transform.Find("NoticeCanvas").GetComponent<Canvas>();
		PlayerDefinition.SetStats();
		AddObserver(GameManager.Instance.missionSO.missionList[3]);

		PlayerDefinition.InitSkills();
	}

	private void Update()
	{
		LookCamera();
	}

	void LookCamera()
	{
		noticeCanvas.transform.LookAt(noticeCanvas.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
	}
	#endregion

	#region Stat Increasers
	public void IncreaseHealth(float healthAmount)
    {
		text.text = healthAmount.ToString();
		text.color = Color.green;
	
		Instantiate(text, noticeCanvas.transform);

		PlayerDefinition.IncreaseHealth(healthAmount);
    }

    public void IncreaseMana(float manaAmount)
    {
        PlayerDefinition.IncreaseMana(manaAmount);
    }

	public void IncreaseExp(int expAmount)
	{
		PlayerDefinition.IncreaseExp(expAmount);
		if (PlayerDefinition.level >= GameManager.Instance.missionSO.missionList[3].needValue)
			Notify("LevelUp", PlayerDefinition.level);
	}
    #endregion

    #region Stat Decreasers
    public void TakeDamage(float damageAmount)
    {
		text.text = PlayerDefinition.TakeDamage(damageAmount).ToString();
		text.color = Color.red;

		Instantiate(text, noticeCanvas.transform);
    }

    public void TakeMana(float amount)
    {
        PlayerDefinition.TakeMana(amount);
    }
    #endregion

    #region Get 
    public float GetHealth()
    {
        return PlayerDefinition.currentHealth;
    }

	public float GetMaxHealth(){
		return PlayerDefinition.maxHealth;
	}

    public float GetMana()
    {
        return PlayerDefinition.currentMana;
    }

    public float GetDamage()
    {
        return PlayerDefinition.CurrentDamage;
    }

    public float GetViewRange()
    {
        return PlayerDefinition.viewRange;
    }

    public float GetAttackRange()
    {
        return PlayerDefinition.attackRange;
    }

	public int GetLevel()
	{
		return PlayerDefinition.level;
	}
    #endregion
}
