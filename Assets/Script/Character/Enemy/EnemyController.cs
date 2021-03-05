using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Assets.Scripts;

public class EnemyController : Subject
{
    public EnemyAttackDefinition demoAttack;
	public GameObject criticalParticle;

	public Canvas healthCanvas;
    public Image healthBar;
    Camera isoCamera;

    NavMeshAgent agent;
    Animator anim;
    EnemyStats stats;

	GameObject target;
	GameObject prev_target;
    bool lockAttack;
	bool isAlive;
	static bool bossDied = false;
	
    void Start()
    {
		Debug.Log("EnemyController Start");
		AddObserver(GameManager.Instance.missionSO.missionList[1]);
		agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        stats = GetComponent<EnemyStats>();
        isoCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
    }

    private void OnEnable()
    {
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		stats = GetComponent<EnemyStats>();
		isoCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;

		isAlive = true;
		lockAttack = false;
        ReleaseSpeed();
        prev_target = FindTarget();
        StartCoroutine("ManaRecovery");
		isoCamera = Camera.main;
        anim.SetBool("IsAlive", true);
	}

    void Update()
    {
		if (isAlive)
		{
			FindTarget();
			if (prev_target != target)
			{
				ReleaseSpeed();
			}
			if (target != null)
			{
				if (CanSeeTarget(target.transform.position))
				{
					SetDestination(target.transform.position);
					anim.SetInteger("LoopState", 1);
                    
					if (CanAttackTarget(agent.remainingDistance) && !lockAttack && agent.hasPath)
					{
						AttackTarget(target);
						agent.speed = 0f;
						agent.transform.LookAt(target.transform);

						lockAttack = true;
					}
				}
			}
			else
			{
				anim.SetInteger("LoopState", 0);
			}
			LookAtCamera();
			UpdateHealthBar();
			prev_target = target;
		}
	}
	private void LateUpdate()
	{
		if(stats.GetHealth() <= 0){
			Death();
			if(stats.Type == "boss" && !bossDied)
			{
				//GameManager.Instance.SetState(GameState.Win);
				bossDied = true;
			}
		}
	}

	IEnumerator ManaRecovery()
    {
        yield return new WaitForSeconds(2.0f);
        stats.IncreaseMana(stats.GetManaIncrement());
        //print(stats.GetMana());
        StartCoroutine("ManaRecovery");
    }

    #region Attack Function
    public void SetDestination(Vector3 destination)
    {
        agent.destination = destination;
    }

    public void AttackTarget(GameObject target)
    {
        var attack = demoAttack.CreateEnemyAttack(stats, target.GetComponent<PlayerStats>());
        var attackables = target.GetComponentsInChildren(typeof(iAttackable));

		Debug.Log("Critical : " + attack.IsCritical);
		if (!attack.IsCritical)
			anim.SetTrigger("Attack");
		else
		{
			anim.SetTrigger("Attack");
			ActiveParticle();
			Debug.Log("Critical");
			stats.SetCurrentMana(0);
		}

		if (attack.IsCritical)
        {
            Debug.Log("CriticalAttack");
        }

        foreach(iAttackable attackable in attackables)
        {
            attackable.OnAttack(gameObject, attack);
        }
    }

    GameObject FindTarget()
    {
        return target = GameObject.FindGameObjectWithTag("MainPlayer");
    }

    bool CanSeeTarget(Vector3 target)
    {
        float viewRange = stats.GetViewRange();
        float distance = Vector3.Distance(target, agent.transform.position);
        return (distance < viewRange) ? true : false;
    }
      
    bool CanAttackTarget(float distance)
    {
        float attackRange = stats.GetAttackRange();
        return (distance < attackRange) ? true : false;
    }
    #endregion

    public void ReleaseSpeed()
    {
		if(agent != null)
			agent.speed = 1.0f;
        lockAttack = false;
		if( criticalParticle != null ) criticalParticle.SetActive(false);
    }

	void ActiveParticle(){
		if(criticalParticle != null)
		{
			criticalParticle.SetActive(true);
			ParticleSystem[] particles = criticalParticle.GetComponentsInChildren<ParticleSystem>();

			foreach (var p in particles)
			{
				p.Play();
			}
		}
	}

	void Death()
	{
		anim.SetTrigger("Die");
		agent.speed = 0f;
		isAlive = false;
	}

    void BeginingOfDead()
    {
		anim.SetBool("IsAlive", false);
	}

    void WaitForDead()
    {
        gameObject.SetActive(false);
		Notify("MonsterSlayer", 1);
	}
	
    public Animator Anim
    {
        get
        {
            return anim;
        }
    }

	public static bool IsWin()
	{
		return bossDied;
	}
	
    #region healthBar
    void LookAtCamera(){
        healthCanvas.transform.LookAt(healthCanvas.transform.position + isoCamera.transform.rotation * Vector3.forward, isoCamera.transform.rotation * Vector3.up);
    }
    void UpdateHealthBar()
    {
        healthBar.fillAmount = stats.GetHealth() / stats.GetMaxHealth();

        if (healthBar.fillAmount < 0.3f)
        {
            healthBar.color = Color.red;
        }
        else if(healthBar.fillAmount < 0.6f)
        {
            healthBar.color = Color.yellow;
        }
		else
		{ healthBar.color = Color.green;
		}
    }
    #endregion
}
