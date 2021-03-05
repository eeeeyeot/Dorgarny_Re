using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
	public GameObject pauseUI;
	public void PauseClick()
	{
		Time.timeScale = 0.0f;
		pauseUI.SetActive(true);
	}
	public void RePlayClick()
	{
		pauseUI.SetActive(false);
		Time.timeScale = 1.0f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		
	}
	public void ContinueClick()
	{
		pauseUI.SetActive(false);
		Time.timeScale = 1.0f;
	}
	public void ExitClick()
	{
		pauseUI.SetActive(false);
		Time.timeScale = 1.0f;
		SceneManager.LoadScene("LobbyScene");
	}
}
