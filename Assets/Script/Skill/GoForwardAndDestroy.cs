using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class GoForwardAndDestroy : MonoBehaviour
{
	public float time = 2.0f;

	private void Start()
	{
		Destroy(gameObject, time);
	}

	private void Update()
	{
		transform.position += transform.forward * Constants.fireballSpeed * Time.deltaTime;
	}
}