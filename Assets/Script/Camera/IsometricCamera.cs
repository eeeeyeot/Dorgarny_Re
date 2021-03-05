using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsometricCamera : MonoBehaviour
{
	public Material transMt;
	public Material originalMt;

	float offsetY = 4f;
	float offsetX = 4.5f;
	float offsetZ = -4.5f;
	float Speed = 30f;

	GameObject player;
	GameObject prev_player;

	Renderer r = null;
	RaycastHit hit;
	GameObject previous_hitObj;
	
	private void Start()
	{
		player = GameObject.FindWithTag("MainPlayer");
		prev_player = player;
	}

	private void Update()
	{
		if(player.tag != "MainPlayer")
			player = GameObject.FindWithTag("MainPlayer");

		if (player.name == prev_player.name)
		{
			transform.position = new Vector3(
				player.transform.position.x + offsetX,
				player.transform.position.y + offsetY,
				player.transform.position.z + offsetZ);
		}
		else
		{
			StartCoroutine("ChangeTarget");

			return;
		}

		prev_player = player;
	}

	private void FixedUpdate()
	{
		Vector3 direction = (player.transform.position - transform.position);
		direction.y = direction.y + 0.2f;
		bool isHit = Physics.Raycast(transform.position, direction, out hit);
		Debug.DrawRay(transform.position, direction);

		if (!isHit)
			return;

		//1. 플레이어가 아닌 경우
		if (hit.transform.gameObject.tag != "MainPlayer" && hit.transform.gameObject.tag != "SubPlayer") 
		{
			r = hit.transform.GetComponent<Renderer>();

			//1.1 맵 오브젝트에서 맵 오브젝트로 넘어온 경우
			if(hit.transform.gameObject.layer != 13)
			{
				return;
			}
			else if (previous_hitObj.tag != "MainPlayer" && previous_hitObj.tag != "SubPlayer" && previous_hitObj.tag != "DeadPlayer")
			{
				Renderer p_r = previous_hitObj.GetComponent<Renderer>();
				p_r.material = originalMt;
			}

			//1.2 플레이어에서 맵 오브젝트로 넘어온 경우
			//무조건 반투명하게
			r.material = transMt;
		}
		//2. 플레이어인 경우
		else
		{
			//2.1 처음 시작한 경우
			if (previous_hitObj == null)
			{
				//do nothing..
			}
			else if(previous_hitObj.tag == "DeadPlayer"){
				return;
			}
			//2.2 맵 오브젝트에서 플레이어로 넘어온 경우
			else if (previous_hitObj.tag != "MainPlayer" && previous_hitObj.tag != "SubPlayer" && previous_hitObj.tag != "Enemy")
			{
				previous_hitObj.GetComponent<Renderer>().material = originalMt;
			}
		}
		previous_hitObj = hit.transform.gameObject;
	}

	private IEnumerator ChangeTarget()
	{
		yield return null;

		transform.position = Vector3.Lerp(transform.position,
			new Vector3(player.transform.position.x + offsetX,
				player.transform.position.y + offsetY,
				player.transform.position.z + offsetZ),
			Time.smoothDeltaTime * Speed);

		if (transform.position == new Vector3(
					player.transform.position.x + offsetX,
					player.transform.position.y + offsetY,
					player.transform.position.z + offsetZ))
		{
			prev_player = player;
		}
	}
}