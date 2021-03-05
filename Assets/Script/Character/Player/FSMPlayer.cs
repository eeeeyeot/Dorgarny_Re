using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.AI;
using System.Text;

[RequireComponent(typeof(Rigidbody))]
public class FSMPlayer : FSMBase<CharacterState>
{
	//Animator 컴포넌트를 제어하는 변수
	public Animator anim;

	public GameObject hitCollider;
	public GameObject[] skills = new GameObject[2];
    public GameObject attackEffect;
    public Transform effectPos;
	public Transform effectParent;

	private PlayerAIControl aiControl;
	private GameObject effect;
	private Vector3 forward;
    private Ray ray;
	private StringBuilder sb = new StringBuilder();
	private bool lockAttack = false;

	#region MonoBehaviour Methods

	protected virtual void Awake()
	{
		anim = GetComponent<Animator>();
	}
	private void Start()
	{
		_name = "FSM Player";
		aiControl = GetComponent<PlayerAIControl>();
		if (hitCollider != null)
			hitCollider.GetComponent<HitCollider>().player = GetComponent<PlayerStats>();
	}

	private void LateUpdate()
    {
		if (CHState != CharacterState.Attack)
		{
			if (GetComponent<NavMeshAgent>().velocity != Vector3.zero)
			{
				SetState(CharacterState.Moving);
			}
			else
			{
				SetState(CharacterState.Idle);
			}
		}
    }

	#endregion

	#region States

	protected override IEnumerator Idle()
    {
        do
        {
            //SetState(CharacterState.Idle);
            yield return null;
        } while (!isNewState);
    }

    protected virtual IEnumerator Moving()
    {
        do
        {

			yield return null;
        } while (!isNewState);
    }

    protected virtual IEnumerator Dead()
    {
        do
        {

			yield return null;
        } while (!isNewState);
    }

	protected virtual IEnumerator Attack()
	{
		do
		{
			anim.SetTrigger(CharacterState.Attack.ToString());

			yield return null;
			
			SetState(CharacterState.Idle);
		} while (!isNewState);
	}

	protected virtual IEnumerator Skill()
	{
		do
		{
			anim.SetTrigger(CharacterState.Skill.ToString());

			yield return null;

			SetState(CharacterState.Idle);
		} while (!isNewState);
	}
	#endregion


	#region Set

	public override void SetState(CharacterState newState)
	{
		base.SetState(newState);

		//개체가 가진 Animator 컴포넌트의 state Parameters 에게 상태 변화 값을 전달한다.
		anim.SetInteger("LoopState", (int)CHState);
	}

	public void SetAnimTrigger(string trigger)
	{
		anim.SetTrigger(trigger);
	}

	public void Set(ActiveSkill newSkill)
	{
		skill = newSkill;
	}
	#endregion

	#region Skill

	ActiveSkill skill;
	IEnumerator enumerator;
	public void SkillAffect()
	{
		skill.Use();
	}

	#endregion

	#region AutoAttack
	//Attack
	public void OnAutoAttack()
    {
        if (lockAttack) return;

		transform.LookAt(aiControl.NearestEnemy.transform.position);

        GetComponent<NavMeshAgent>().speed = 1.0f;

        if (gameObject.tag == "MainPlayer")
        {
            PlayerMovement.instance.moveSpeed = 0.7f;
        }

		Debug.Log("On Auto Attack");
		SetState(CharacterState.Attack);
        lockAttack = true;
        if (hitCollider != null)
            hitCollider.SetActive(true);
    }

    public void ReleaseAttack()
    {
        GetComponent<NavMeshAgent>().speed = 2.0f;
        if (gameObject.tag == "MainPlayer")
        {
            PlayerMovement.instance.moveSpeed = 2.0f;
        }
        lockAttack = false;
        if (hitCollider != null)
            hitCollider.SetActive(false);
		SetState(CharacterState.Idle);
    }

    public void ActiveAttackEffect()
    {
        effect = Instantiate(attackEffect, effectPos.position, transform.rotation) as GameObject;

		sb.Clear();
		sb.Append(transform.name).Append("AutoAttack");

		StartCoroutine(sb.ToString());
    }

	IEnumerator WizardAutoAttack()
	{
		
		yield return null;
	}

	IEnumerator ArcherAutoAttack()
	{
		RaycastHit hit;
		var isHit = Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.forward, out hit, transform.rotation);

		if (isHit)
		{
			if (hit.transform.tag == "Enemy")
			{
				hit.transform.GetComponent<EnemyStats>().TakeDamage(GetComponent<PlayerStats>().GetDamage());
			}
		}
		yield return null;
	}

	IEnumerator WarriorAutoAttack()
	{
		
		yield return null;
	}
	#endregion
}
