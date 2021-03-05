using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedDebug : MonoBehaviour, iAttackable
{
    public void OnAttack(GameObject GO, Attack attack)
    {
        if (attack.IsCritical) Debug.Log("Critical");

        Debug.LogFormat("{0} => {1}: {2}", GO.name, name, attack.Damage);
    }
}
