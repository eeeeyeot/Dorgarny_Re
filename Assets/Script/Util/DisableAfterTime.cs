using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
	public float time = 3.0f;
	private float currentTime;

	private void OnEnable()
	{
		currentTime = time;
	}

	private void Update()
	{
		if(currentTime > 0)
		{
			currentTime -= Time.deltaTime;
		}
		else
		{
			currentTime = time;
			gameObject.SetActive(false);
		}



		if (Input.touchCount > 0)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Began)
				gameObject.SetActive(false);
		}

		if (Input.GetMouseButton(0))
		{
			gameObject.SetActive(false);
		}

	}


}
