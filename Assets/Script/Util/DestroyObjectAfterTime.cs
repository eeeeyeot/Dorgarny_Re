using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectAfterTime : MonoBehaviour
{
	public float time = 2.0f;
	private float speed = 0.3f;
	RectTransform rectTr = null;

	void Start()
	{
		Destroy(gameObject, time);
		rectTr = GetComponent<RectTransform>();
	}

	private void Update()
	{
		if(rectTr != null)
			rectTr.position += (Vector3.up * Time.deltaTime * speed);
	}
}
