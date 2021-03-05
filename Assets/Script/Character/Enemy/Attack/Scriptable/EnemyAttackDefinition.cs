using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttack", menuName = "Enemy/BaseAttack")]
public class EnemyAttackDefinition : ScriptableObject
{
    public float minDamage;
    public float maxDamage;
    public float criticalMultiplier;

    public Attack CreateEnemyAttack(EnemyStats enemy, PlayerStats player)
    {
		
        float coreDamage = enemy.GetDamage();
        coreDamage += Random.Range(minDamage, maxDamage);


		//bool isCritical = Random.value < criticalChace;
		bool isCritical = enemy.GetMana() >= enemy.GetMaxMana();
        if (isCritical) coreDamage *= criticalMultiplier;

        return new Attack((float)coreDamage, isCritical);
    }
}
