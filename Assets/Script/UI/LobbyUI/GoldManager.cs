using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class GoldManager : Subject
{
    #region Singleton
    public static GoldManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion

    public Text goldtext;
    private int gold = 9999;

    public int Gold { get { return gold; } set { gold += value; SetText(); } }

    public void Start()
    {
        SetText();
        AddObserver(MissionManager.instance.missionSO.missionList[0]);
    }

    public void CalcGold(int value)
    {
        Gold = value;
        Notify("Gold", -value);
    }

    public void SetText()
    {
        goldtext.text = gold.ToString();
    }
         
}
