using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBtn : Buttons
{
    public override void ClickButton()
    {
        HideButton();
        Camera.main.transform.position = new Vector3(-170.0f, 9.0f, -3.4f);
        Camera.main.transform.rotation = Quaternion.Euler(new Vector3(45.0f, -90.0f, 0.0f));
        StageManager.instance.stageCharacterTransform.gameObject.SetActive(true);
    }
}
