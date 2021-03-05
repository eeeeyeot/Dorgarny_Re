using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public EnemyStats_SO EnemyDefinition = new EnemyStats_SO();
	public Text text;

	[SerializeField]
	public Canvas noticeCanvas;

    #region Initalizations
    void OnEnable()
    {
		
        if (!EnemyDefinition.setMenually)
        {
            EnemyDefinition.maxHealth = 100;
            EnemyDefinition.currentHealth = 50;

            EnemyDefinition.maxMana = 100;
            EnemyDefinition.currentMana = 50;

            EnemyDefinition.baseDamage = 20;
        }
        else
        {
            EnemyDefinition.currentHealth = EnemyDefinition.maxHealth;
            EnemyDefinition.currentMana = 0;
        }
    }
    #endregion

    #region Stat Increasers
    public void IncreaseHealth(float healthAmount)
    {
		print(gameObject.name + " Get Heals");

		text.text = healthAmount.ToString();
		text.color = Color.green;

		Instantiate(text, noticeCanvas.transform);

		EnemyDefinition.IncreaseHealth(healthAmount);
    }

    public void IncreaseMana(float manaAmount)
    {
        EnemyDefinition.IncreaseMana(manaAmount);
    }
    #endregion

    #region Stat Decreasers
    public void TakeDamage(float damageAmount)
    {
		text.text = damageAmount.ToString();
		text.color = Color.red;

		Instantiate(text, noticeCanvas.transform.position, noticeCanvas.transform.rotation , noticeCanvas.transform);

		EnemyDefinition.TakeDamage(damageAmount);
        GetComponent<EnemyController>().Anim.SetTrigger("Damaged");
    }

    public void TakeMana(float amount)
    {
        EnemyDefinition.TakeMana(amount);
    }
    #endregion
    
    #region SaveEnemyData
    public void saveEnemyData()
    {
        //'$'면 상태 저장
        if(Input.GetKeyDown(KeyCode.Dollar))
        {
            EnemyDefinition.saveEnemyData();
        }
    }
    #endregion

    #region Get 
    public float GetHealth()
    {
        return EnemyDefinition.currentHealth;
    }

    public float GetMaxHealth()
    {
        return EnemyDefinition.maxHealth;
    }

    public float GetMana()
    {
        return EnemyDefinition.currentMana;
    }

    public float GetMaxMana()
    {
        return EnemyDefinition.maxMana;
    }

    public float GetDamage()
    {
        return EnemyDefinition.baseDamage;
    }

    public float GetManaIncrement()
    {
        return EnemyDefinition.manaIncrement;
    }

    public float GetViewRange()
    {
        return EnemyDefinition.viewRange;
    }

    public float GetAttackRange()
    {
        return EnemyDefinition.attackRange;
    }

	public string Type {
		get
		{
			return EnemyDefinition.type;
		}
		set 
		{
			EnemyDefinition.type = value;
		}
	}
    #endregion

    public void SetCurrentMana(float amount)
    {
        EnemyDefinition.currentMana = amount;
    }
}
