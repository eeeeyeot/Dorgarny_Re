using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

//[CreateAssetMenu(fileName = "NewStats", menuName = "Enemy/Stats", order =1)]
//public class EnemyStats_SO : ScriptableObject

[System.Serializable]
public class EnemyStats_SO
{

    #region Field
    public bool setMenually = false;
    //public bool saveDataOnClose = false;

    public float maxHealth = 0;
    public float currentHealth = 0;

    public float maxMana = 0;
    public float currentMana = 0;
    public float manaIncrement = 0;

    public float baseDamage = 0;

    public float viewRange = 0;
    public float attackRange = 0;

	public string type = "Enemy";
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
    #endregion

    #region Stat Decreasers
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
    }

    public void TakeMana(float amount)
    {
        currentMana -= amount;

        if(currentMana <= 0)
        {
            currentMana = 0;
        }
    }
    #endregion


    #region SaveEnemyData
    public void saveEnemyData()
    {
        //saveDataOnClose = true;
        //EditorUtility.SetDirty(this);
    }
    #endregion
}
