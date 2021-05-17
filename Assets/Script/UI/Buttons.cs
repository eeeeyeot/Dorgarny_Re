using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public virtual void ClickButton() { }
    public void HideButton()
    {
        this.gameObject.SetActive(false);
    }

    public void UpBUtton()
    {
        this.gameObject.SetActive(true);
    }
}
