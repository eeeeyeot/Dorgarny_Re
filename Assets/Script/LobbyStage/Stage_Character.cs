using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stage_Character : MonoBehaviour
{
    private float offSetx;
    private float offSety;

    private void Start()
    {
        offSetx = 7;
        offSety = 7;
    }

    public void OnTriggerEnter(Collider other)
    {
        StageManager.instance.CheckStage(other.name);
    }

    private void OnTriggerExit(Collider other)
    {
        StageManager.instance.CheckStage(other.name);
    }

	void Update()
	{
        Vector3 thisPos = this.transform.position;
        Camera.main.transform.position = thisPos + new Vector3(offSetx, offSety);
		if(GetComponent<NavMeshAgent>().velocity != Vector3.zero)
		{
			GetComponent<Animator>().SetInteger("LoopState", 1);
		}
		else
		{
			GetComponent<Animator>().SetInteger("LoopState", 0);
		}
	}
}
