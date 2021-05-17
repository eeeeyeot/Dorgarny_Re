using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Util;

public class CharacterManager : MonoBehaviour
{
    #region Singleton
    public static CharacterManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public List<PlayerStats_SO> stats_List = new List<PlayerStats_SO>();

    public CharacterInfoUI characterInfo;
    public int playerIndex = 0;

	public void UpdateUI()
    {
        characterInfo.SetInfo();
    }
}
