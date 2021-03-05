using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour {
    public static IntroSceneManager intromanager;

	
    private void Awake()
    {
        if(intromanager == null)
        {
            intromanager = this;
        }
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Touchevent();
	}

    void Touchevent()
    {
        if(Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            //Touch touch = Input.GetTouch(0);
            if (/*touch.phase == TouchPhase.Began ||*/ Input.GetMouseButton(0))
                SceneManager.LoadScene("LobbyScene");
        }
    }
}
