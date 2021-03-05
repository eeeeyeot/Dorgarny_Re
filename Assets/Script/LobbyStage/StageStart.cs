using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageStart : Buttons
{
	public override void ClickButton()
	{
		Debug.Log("Scene : " + StageManager.instance.colliderName);
		SceneManager.LoadScene(StageManager.instance.colliderName);
	}

    public void ClickTownBtn()
    {
        StageManager.instance.townBtn.SetActive(false);
        StageManager.instance.stageSelectBtn.SetActive(true);
        Camera.main.transform.position = new Vector3(-0.137f, 1.66f, -3.103f);
        Camera.main.transform.rotation = Quaternion.Euler(new Vector3(20f, 0.0f, 0.0f));
        StageManager.instance.chaTransform.gameObject.SetActive(false);
        

    }

    public void StageBtn()
    {
        StageManager.instance.stageSelectBtn.SetActive(false);
        Camera.main.transform.position = new Vector3(-170.0f, 9.0f, -3.4f);
        Camera.main.transform.rotation = Quaternion.Euler(new Vector3(45.0f, -90.0f, 0.0f));
        StageManager.instance.chaTransform.gameObject.SetActive(true);
    }

}
