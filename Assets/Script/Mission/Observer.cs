using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//관찰자
public abstract class Observer
{
    public abstract void OnNotify(string id, int value);
}
