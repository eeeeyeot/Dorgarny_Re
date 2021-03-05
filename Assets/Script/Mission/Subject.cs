using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

//관찰당하는자

public class Subject : MonoBehaviour
{
    private List<Observer> _observers = new List<Observer>();

    public void AddObserver(Observer observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(Observer observer)
    {
        _observers.Remove(observer);
    }

    public void Notify(string id, int value)
    {
        foreach (var observer in _observers)
            observer.OnNotify(id, value);
    }
}

