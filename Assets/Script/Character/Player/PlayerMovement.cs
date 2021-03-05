using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {

	#region Singleton

	public static PlayerMovement instance;

	void Awake()
	{
		instance = this;
	}

	#endregion

	[SerializeField]
	public float moveSpeed = 2f;
	public VirtualJoystick moveJoystick = null;

	Vector3 forward, right;
	Vector3 previous_pos;

	NavMeshAgent agent;

	Transform target;

	void Start () {
		//get the forward of camera except for the y element
		target     = GameObject.FindWithTag("MainPlayer").transform;
		agent      = target.GetComponent<NavMeshAgent>();

		forward    = Camera.main.transform.forward;
		forward.y  = 0;

		forward    = Vector3.Normalize(forward);

		//make a right vector
		right      = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
	}
	
	void FixedUpdate () {
		target = GameObject.FindWithTag("MainPlayer").transform;
		agent = target.GetComponent<NavMeshAgent>();
		if (Input.anyKey)
		{
			Move();
		}
	}

	void Move()
	{
		//calculate final value
		Vector3 RightMovement = 
			right * moveSpeed * Time.smoothDeltaTime * Input.GetAxis("Horizontal");
		Vector3 ForwardMovement =
			forward * moveSpeed * Time.smoothDeltaTime * Input.GetAxis("Vertical");
		//Vector3 FinalMovement = ForwardMovement + RightMovement;
		Vector3 direction = Vector3.Normalize(ForwardMovement + RightMovement);

		if (moveJoystick != null)
		{
			if (moveJoystick.InputDirection != Vector3.zero)
			{
				if (previous_pos != moveJoystick.InputDirection)
				{
					Quaternion v3Rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
					moveJoystick.InputDirection = v3Rotation * moveJoystick.InputDirection;
				}
				direction = Vector3.Normalize(moveJoystick.InputDirection);
				//FinalMovement = direction * moveSpeed * Time.smoothDeltaTime;
			}
		}

		//to prevent to try to call a method towards a vector zero (0,0,0)
		if (direction != Vector3.zero)
		{
			target.transform.forward = direction;
			//agent.Move(FinalMovement);
			agent.velocity = direction * moveSpeed;
		}

		previous_pos = moveJoystick.InputDirection;
	}

}
