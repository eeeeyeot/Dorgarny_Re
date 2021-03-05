using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MissionSO", menuName = "Mission")]
public class MissionSO : ScriptableObject
{
    public List<Mission> missionList = new List<Mission>();
}
