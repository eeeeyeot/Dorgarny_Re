using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaLvUI : MonoBehaviour {
    
    private Vector3 targetposition;
    public UnityEngine.UI.Text txt;
    private Vector3 offset;
	void Start () {
        //chatransform = this.transform; 캐릭터 위치 읽어오기
        offset = new Vector3(0.0f, 200.0f, 0.0f);
        //name = this.gameObject.name;  캐릭터 Lv 읽어오기
        SetTextPosition();
    }
	
    public void SetTextPosition()
    {
       // targetposition = Camera.main.WorldToScreenPoint(chatransform.position);
        targetposition = targetposition + offset;
        txt.transform.position = targetposition;
        //txt.text = name;
        txt.resizeTextForBestFit = true;
    }
}
