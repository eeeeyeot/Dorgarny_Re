using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestList", menuName = "QuestInfo")]
public class QuestInfo : ScriptableObject
{
    [System.Serializable]
    public class Quest
    {
        public string quest;
        public bool complete = false;
    }

    [System.Serializable]
    public class QuestCombine {
        public string stagename;
        public Quest[] quest;
    }

    public List<QuestCombine> questList;

}
