using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
	public void StartStage()
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
        StageManager.instance.stageCharacterTransform.gameObject.SetActive(false);
    }

    public void StageSelectBtn()
    {
        StageManager.instance.stageSelectBtn.SetActive(false);
        Camera.main.transform.position = new Vector3(-170, 9, -3.4f);
        Camera.main.transform.rotation = Quaternion.Euler(new Vector3(45, -90, 0));
        StageManager.instance.stageCharacterTransform.gameObject.SetActive(true);
    }

}
