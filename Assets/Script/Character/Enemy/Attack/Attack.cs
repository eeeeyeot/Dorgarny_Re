using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    private readonly float damage;
    private readonly bool critical;

    #region Constructor
    public Attack(float damage, bool critical)
    {
        this.damage = damage;
        this.critical = critical;
    }
    #endregion

    #region Get
    public float Damage
    {
        get { return damage; }
    }

    public bool IsCritical
    {
        get { return critical; }
    }
    #endregion
}
