using System;
using System.Collections.Generic;

public class Subject : ISubject
{
    //1

    private List<IObserver> observers = new List<IObserver>();

    public void Notify()
    {
        observers.ForEach(obs => obs.Activate());
    }

    public void Subscribe(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Unsubscribe(IObserver observer)
    {
        observers.Remove(observer);
    }

    //2

    public Action OnAction;

    public void Activate()
    {
        OnAction?.Invoke();
    }
}
