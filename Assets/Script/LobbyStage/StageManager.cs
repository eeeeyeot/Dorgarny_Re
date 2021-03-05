using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using Util;

public class StageManager : MonoBehaviour
{
    #region Singleton
    public static StageManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of StageManager");
        }
        instance = this;
    }
    #endregion

    public Sprite fillStar;

    public Transform chaTransform;
    public string colliderName { get; set; }
    private NavMeshAgent agent;
    private string destination;

    public Transform stageroot;
    public List<StageInfo> stageList;
    public StageList_SO stageSO;

    public GameObject stageSelectBtn;
    public GameObject stageStartBtn;
    public GameObject townBtn;
    
    void Start()
    {
        
        agent = chaTransform.GetComponent<NavMeshAgent>();
        destination = "Town";
        for (int i = 0; i < stageroot.childCount - 1; i++)
        {
            stageList.Add(stageroot.GetChild(i + 1).GetComponentInChildren<StageInfo>());
        }
        for (int i = 0; i < stageList.Count; i++)
        {
            stageList[i].starCount = stageSO.stageStarList[i].count;
        }
        StageStarUpdate();
    }

    public void StageStarUpdate()
    {
        foreach(StageInfo stage in stageList)
        {
			Debug.Log(stage.name);
            stage.UpdateStar(fillStar);
        }
    }

    public void StageMove(Vector3 position)
    {
        agent.SetDestination(position);
    }

    public void SetDestination(string desti)
    {
        destination = desti;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f, 1 << LayerMask.NameToLayer("MoveAgent")))
            {

                SetDestination(hit.transform.name);
                StageMove(hit.transform.position);
            }
        }

    }

    public void CheckStage(string name)
    {
        if (destination == name)
        {
            if (destination != "Town")
            {
                townBtn.SetActive(false);
                stageStartBtn.SetActive(true);
            }
            else
            {
                townBtn.SetActive(true);
            }
        }
        else
        {
            stageStartBtn.SetActive(false);
            townBtn.SetActive(false);
        }

        colliderName = name;
    }

}

