using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts;

public class PlayerAIControl : MonoBehaviour
{
    public float attackRange = 1.0f;
    public float cognizanceRange = 1.5f;

    private NavMeshAgent agent;
    private float RemainingDistance;
    private float distance;
    GameObject target;
    GameObject mainPlayer;
    GameObject[] enemies;
    GameObject nearestEnemy;

	public GameObject NearestEnemy{	get { return nearestEnemy; } }

    public NavMeshAgent Agent
    {
        get
        {
            if (agent == null)
            {
                //agent = GetComponentInChildren<NavMeshAgent>();
                agent = GetComponent<NavMeshAgent>();
            }
            return agent;
        }
    }

    private void Start()
    {
        mainPlayer = target = GameObject.FindWithTag("MainPlayer");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void Update()
    {
		if(mainPlayer != null || mainPlayer.tag != "MainPlayer")
			mainPlayer = GameObject.FindWithTag("MainPlayer");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
			//가장 가까운 몬스터 탐색
            distance = Vector3.Distance(transform.position, enemies[0].transform.position);
            nearestEnemy = enemies[0];
            foreach (var e in enemies)
            {
                if (distance > Vector3.Distance(transform.position, e.transform.position))
                {
                    nearestEnemy = e;
                }
            }

			//인식범위보다 멀거나 메인 플레이어와 거리가 멀어질 경우
            if (Vector3.Distance(transform.position, nearestEnemy.transform.position) > cognizanceRange ||
			Vector3.Distance(transform.position, mainPlayer.transform.position) > 1.5f)
            {
                target = mainPlayer;
            }
			//인식범위내에 몹이 들어오고 메인 플레이어와 가까울 경우
            else
            {
                target = nearestEnemy;
            }
        }
        else
        {
            target = mainPlayer;
        }

		//target이 잘못되거나 내가 메인플레이어인 경우
        if (target == null || transform.tag == "MainPlayer")
        {
            ClearAgent();
            return;
        }
        
        UpdateAgent();
    }

    void ClearAgent()
    {
        Agent.ResetPath();
    }

    void UpdateAgent()
    {
        Vector3 Direction = Vector3.zero;

        if (target.tag == "MainPlayer")
        {
            Agent.SetDestination(target.transform.position);
			Agent.stoppingDistance = 1.2f;
		}
        else if (target.tag == "Enemy")
        {
			if(Vector3.Distance(transform.position, target.transform.position) > attackRange){
				agent.SetDestination(target.transform.position);
				Agent.stoppingDistance = attackRange;
			}
            if (Vector3.Distance(transform.position, target.transform.position) <= attackRange)
            {
                Agent.speed = 0f;
                transform.LookAt(target.transform.position);
                GetComponent<FSMPlayer>().OnAutoAttack();
            }
        }
    }
}
