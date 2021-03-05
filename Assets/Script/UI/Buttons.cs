using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public virtual void ClickButton() { }
    public void HideButton()
    {
        //for(int i = 0; i < 100; i++)
        //{
        //    RectTransform a;
        //    this.gameObject.transform.position
        //}
        this.gameObject.SetActive(false);
    }

    public void UpBUtton()
    {
        this.gameObject.SetActive(true);
    }
}
